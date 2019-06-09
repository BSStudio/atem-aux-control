using BMDSwitcherAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midi_aux_control
{
    public class AuxMonitor : IBMDSwitcherInputAuxCallback
    {

        private Switcher parent;

        private IBMDSwitcherInputAux m_aux;

        public IBMDSwitcherInputAux Aux
        {
            get => m_aux;
        }

        public AuxMonitor(Switcher parent, IBMDSwitcherInputAux aux)
        {
            this.parent = parent;
            m_aux = aux;
            updateInputSource();
        }

        void IBMDSwitcherInputAuxCallback.Notify(_BMDSwitcherInputAuxEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherInputAuxEventType.bmdSwitcherInputAuxEventTypeInputSourceChanged:
                    updateInputSource();
                    break;
            }
        }

        private void updateInputSource()
        {
            InvokeHelper.Invoke(() =>
            {
                m_aux.GetInputSource(out long inputId);
                Source = parent.GetSignalById(inputId);
            });
        }

        private Signal source;

        public Signal Source
        {
            get => source;
            private set
            {
                if (source != null)
                {
                    source.Monitor.IsPreviewTalliedChanged -= sourceIsPreviewTalliedHandler;
                    source.Monitor.IsProgramTalliedChanged -= sourceIsProgramTalliedHandler;
                }
                source = value;
                if (value != null)
                {
                    source.Monitor.IsPreviewTalliedChanged += sourceIsPreviewTalliedHandler;
                    source.Monitor.IsProgramTalliedChanged += sourceIsProgramTalliedHandler;
                    IsPreviewTallied = source.Monitor.IsPreviewTallied;
                    IsProgramTallied = source.Monitor.IsProgramTallied;
                }
                else
                {
                    IsPreviewTallied = false;
                    IsProgramTallied = false;
                }
                SourceNameChanged?.Invoke(SourceName);
            }
        }

        private void sourceIsPreviewTalliedHandler(bool isPreviewTallied)
        {
            IsPreviewTallied = isPreviewTallied;
        }

        private void sourceIsProgramTalliedHandler(bool isProgramTallied)
        {
            IsProgramTallied = isProgramTallied;
        }

        public delegate void IsProgramTalliedChangedDelegate(bool isProgramTallied);
        public event IsProgramTalliedChangedDelegate IsProgramTalliedChanged;

        private bool isProgramTallied = false;

        public bool IsProgramTallied
        {
            get => isProgramTallied;
            private set
            {
                isProgramTallied = value;
                IsProgramTalliedChanged?.Invoke(value);
            }
        }

        public delegate void IsPreviewTalliedChangedDelegate(bool isPreviewTallied);
        public event IsPreviewTalliedChangedDelegate IsPreviewTalliedChanged;

        private bool isPreviewTallied = false;

        public bool IsPreviewTallied
        {
            get => isPreviewTallied;
            private set
            {
                isPreviewTallied = value;
                IsPreviewTalliedChanged?.Invoke(value);
            }
        }

        public delegate void SourceNameChangedDelegate(string newName);
        public event SourceNameChangedDelegate SourceNameChanged;

        public string SourceName
        {
            get => (source != null) ? source.LongName : "-";
        }

    }
}