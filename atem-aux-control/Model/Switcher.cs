using BMDSwitcherAPI;
using midi_aux_control.Model.Persist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace midi_aux_control
{
    public class Switcher : IPersistable
    {

        private IBMDSwitcher m_switcher;
        private IBMDSwitcherDiscovery m_switcherDiscovery;
        private SwitcherMonitor m_switcherMonitor;

        public delegate void IpAddressChangedDelegate(string newIp);
        public event IpAddressChangedDelegate IpAddressChanged;

        private string ipAddress;

        public string IpAddress
        {
            get => ipAddress;
            set
            {
                if (value == ipAddress)
                    return;
                ipAddress = value;
                IpAddressChanged?.Invoke(value);
                if (!restoringPersisted)
                    Persister.Instance.RequestPersist(this);
            }
        }

        public Switcher()
        {
            Persister.Instance.RegisterPersistable(this);
        }

        public void Connect()
        {

            m_switcherDiscovery = new CBMDSwitcherDiscovery();
            _BMDSwitcherConnectToFailure failReason = 0;

            try
            {
                m_switcherDiscovery.ConnectTo(ipAddress, out m_switcher, out failReason);
            }
            catch (COMException)
            {
                switch (failReason)
                {
                    case _BMDSwitcherConnectToFailure.bmdSwitcherConnectToFailureNoResponse:
                        throw new Exception("No response from Switcher");
                    case _BMDSwitcherConnectToFailure.bmdSwitcherConnectToFailureIncompatibleFirmware:
                        throw new Exception("Switcher has incompatible firmware");
                    default:
                        throw new Exception("Connection failed for unknown reason");
                }
            }

            switcherConnected();

        }

        private void switcherConnected()
        {

            m_switcherMonitor = new SwitcherMonitor(m_switcher);
            m_switcher.AddCallback(m_switcherMonitor);
            m_switcherMonitor.SwitcherDisconnected += switcherDisconnectedHandler;

            IBMDSwitcherInputIterator inputIterator = null;
            IntPtr inputIteratorPtr;
            Guid inputIteratorIID = typeof(IBMDSwitcherInputIterator).GUID;
            m_switcher.CreateIterator(ref inputIteratorIID, out inputIteratorPtr);
            if (inputIteratorPtr != null)
            {
                inputIterator = (IBMDSwitcherInputIterator)Marshal.GetObjectForIUnknown(inputIteratorPtr);
            }

            if (inputIterator != null)
            {
                IBMDSwitcherInput input;
                inputIterator.Next(out input);
                while (input != null)
                {
                    input.GetPortType(out _BMDSwitcherPortType t);
                    input.GetLongName(out string longName);
                    input.GetInputId(out long inputId);
                    if (t == _BMDSwitcherPortType.bmdSwitcherPortTypeAuxOutput)
                    {
                        IBMDSwitcherInputAux aux = input as IBMDSwitcherInputAux;
                        AuxMonitor monitor = new AuxMonitor(this, aux);
                        aux.AddCallback(monitor);
                        auxes.Add(new AuxPort() {
                            ID = inputId,
                            LongName = longName,
                            Obj = aux,
                            Monitor = monitor
                        });
                    }
                    else
                    {
                        InputMonitor monitor = new InputMonitor(input);
                        input.AddCallback(monitor);
                        signals.Add(new Signal() {
                            ID = inputId,
                            LongName = longName,
                            Monitor = monitor
                        });
                    }
                    inputIterator.Next(out input);
                }
            }

            Connected = true;

        }

        private void switcherDisconnectedHandler(IBMDSwitcher switcher)
        {
            auxes.Clear();
            signals.Clear();
            Connected = false;
        }

        #region Auxes and signals
        private List<AuxPort> auxes = new List<AuxPort>();
        private List<Signal> signals = new List<Signal>();

        public List<Signal> Signals
        {
            get => signals;
        }

        public List<AuxPort> Auxes
        {
            get => auxes;
        }

        public Signal GetSignalById(long id)
        {
            return signals.FirstOrDefault(s => (s.ID == id));
        }

        public AuxPort GetAuxById(long id)
        {
            return auxes.FirstOrDefault(a => (a.ID == id));
        }
        #endregion

        #region Connection state
        public delegate void ConnectedChangedDelegate(Switcher switcher, bool connected);
        public event ConnectedChangedDelegate ConnectedChanged;

        private bool connected = false;

        public bool Connected
        {
            get => connected;
            set
            {
                connected = value;
                ConnectedChanged?.Invoke(this, value);
            }
        }
        #endregion

        #region Persistence
        public string PersistenceKey => "mixer";

        private const string PERSISTENCE_KEY_IP = "ip";

        public Dictionary<string, string> GetPersistableData()
        {
            return new Dictionary<string, string>()
            {
                { PERSISTENCE_KEY_IP,  IpAddress }
            };
        }

        public void SetPersistableData(Dictionary<string, string> data)
        {

            restoringPersisted = true;

            if (!data.TryGetValue(PERSISTENCE_KEY_IP, out string ip))
                ip = "";
            IpAddress = ip;

            restoringPersisted = false;

        }

        bool restoringPersisted = false;
        #endregion

    }
}
