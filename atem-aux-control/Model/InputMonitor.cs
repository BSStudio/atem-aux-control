using BMDSwitcherAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midi_aux_control
{
    public class InputMonitor : IBMDSwitcherInputCallback
    {

        private IBMDSwitcherInput m_input;

        public IBMDSwitcherInput Input {
            get => m_input;
        }

        public InputMonitor(IBMDSwitcherInput input)
        {
            m_input = input;
            InvokeHelper.Invoke(() =>
            {
                m_input.IsProgramTallied(out int isProgramTallied);
                IsProgramTallied = (isProgramTallied != 0);
                m_input.IsPreviewTallied(out int isPreviewTallied);
                IsPreviewTallied = (isPreviewTallied != 0);
            });
        }

        void IBMDSwitcherInputCallback.Notify(_BMDSwitcherInputEventType eventType)
        {
            switch (eventType)
            {
                case _BMDSwitcherInputEventType.bmdSwitcherInputEventTypeIsProgramTalliedChanged:
                    InvokeHelper.Invoke(() =>
                    {
                        Input.IsProgramTallied(out int isProgramTallied);
                        IsProgramTallied = (isProgramTallied != 0);
                    });
                    break;
                case _BMDSwitcherInputEventType.bmdSwitcherInputEventTypeIsPreviewTalliedChanged:
                    InvokeHelper.Invoke(() =>
                    {
                        Input.IsPreviewTallied(out int isPreviewTallied);
                        IsPreviewTallied = (isPreviewTallied != 0);
                    });
                    break;
            }
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

    }

}
