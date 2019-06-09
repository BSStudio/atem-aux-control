using BMDSwitcherAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midi_aux_control
{
    public class SwitcherMonitor : IBMDSwitcherCallback
    {

        private IBMDSwitcher m_switcher;

        public delegate void SwitcherDisconnectedDelegate(IBMDSwitcher switcher);
        public event SwitcherDisconnectedDelegate SwitcherDisconnected;

        public SwitcherMonitor(IBMDSwitcher m_switcher)
        {
            this.m_switcher = m_switcher;
        }

        void IBMDSwitcherCallback.Notify(_BMDSwitcherEventType eventType, _BMDSwitcherVideoMode coreVideoMode)
        {
            if (eventType == _BMDSwitcherEventType.bmdSwitcherEventTypeDisconnected)
            {
                SwitcherDisconnected?.Invoke(m_switcher);
            }
        }

    }

}
