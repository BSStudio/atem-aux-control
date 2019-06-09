using midi_aux_control.Model.Persist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midi_aux_control.Model
{

    public class AuxTally : IPersistable
    {

        private string id;

        private Switcher switcher;

        public AuxTally(string id, Switcher switcher)
        {
            this.id = id;
            this.switcher = switcher;
            Persister.Instance.RegisterPersistable(this);
        }

        #region Port
        public delegate void PortChangedDelegate(AuxPort newPort);
        public event PortChangedDelegate PortChanged;

        private AuxPort port;

        public AuxPort Port
        {
            get => port;
            set
            {
                if (port == value)
                    return;
                if (port != null)
                {
                    port.Monitor.IsPreviewTalliedChanged -= Monitor_IsPreviewTalliedChanged;
                    port.Monitor.IsProgramTalliedChanged -= Monitor_IsProgramTalliedChanged;
                    port.Monitor.SourceNameChanged -= Monitor_SourceNameChanged;
                    isPreviewTallied = false;
                    isProgramTallied = false;
                }
                port = value;
                PortChanged?.Invoke(value);
                if (!restoringPersisted)
                    Persister.Instance.RequestPersist(this);
                if (port != null)
                {
                    port.Monitor.IsPreviewTalliedChanged += Monitor_IsPreviewTalliedChanged;
                    port.Monitor.IsProgramTalliedChanged += Monitor_IsProgramTalliedChanged;
                    port.Monitor.SourceNameChanged += Monitor_SourceNameChanged;
                    isPreviewTallied = port.Monitor.IsPreviewTallied;
                    isProgramTallied = port.Monitor.IsProgramTallied;
                }
                updateTallyState();
                SourceNameChanged?.Invoke(SourceName);
            }
        }
        #endregion

        #region Tally state
        private void Monitor_IsPreviewTalliedChanged(bool isPreviewTallied)
        {
            this.isPreviewTallied = isPreviewTallied;
            updateTallyState();
        }

        private void Monitor_IsProgramTalliedChanged(bool isProgramTallied)
        {
            this.isProgramTallied = isProgramTallied;
            updateTallyState();
        }

        private bool isPreviewTallied = false;
        private bool isProgramTallied = false;

        public enum TallyStateEnum
        {
            Program,
            Preview,
            NoTally
        }

        public delegate void TallyStateChangedDelegate(TallyStateEnum newState);
        public event TallyStateChangedDelegate TallyStateChanged;

        private TallyStateEnum tallyState;

        public TallyStateEnum TallyState
        {
            get => tallyState;
            set
            {
                if (tallyState == value)
                    return;
                tallyState = value;
                TallyStateChanged?.Invoke(value);
            }
        }

        private void updateTallyState()
        {
            TallyState = (isProgramTallied ? TallyStateEnum.Program : (isPreviewTallied ? TallyStateEnum.Preview : TallyStateEnum.NoTally));
        }
        #endregion

        #region Source name
        public delegate void SourceNameChangedDelegate(string newName);
        public event SourceNameChangedDelegate SourceNameChanged;

        public string SourceName
        {
            get => ((port != null) ? port.Monitor.SourceName : "-");
        }

        private void Monitor_SourceNameChanged(string newName)
        {
            SourceNameChanged?.Invoke(SourceName);
        }
        #endregion

        #region Persistence
        public string PersistenceKey => string.Format("auxtally-{0}", id);

        private string PERSISTENCE_KEY_PORT = "port";

        public Dictionary<string, string> GetPersistableData()
        {
            return new Dictionary<string, string>()
                {
                    { PERSISTENCE_KEY_PORT, ((port != null) ? port.ID.ToString() : "-1") }
                };
        }

        public void SetPersistableData(Dictionary<string, string> data)
        {

            restoringPersisted = true;

            if (!data.TryGetValue(PERSISTENCE_KEY_PORT, out string auxPortIdStr))
                auxPortIdStr = "-1";
            if (!long.TryParse(auxPortIdStr, out long auxPortId))
                auxPortId = -1;
            Port = switcher.GetAuxById(auxPortId);

            restoringPersisted = false;

        }

        bool restoringPersisted = false;
        #endregion


    }

}
