using BMDSwitcherAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midi_aux_control
{
    public class AuxPort
    {
        public long ID { get; set; }
        public string LongName { get; set; }
        public IBMDSwitcherInputAux Obj { get; set; }
        public AuxMonitor Monitor { get; set; }

        public override bool Equals(object obj)
        {
            AuxPort aux = obj as AuxPort;
            if (aux == null)
                return false;
            return (aux.ID == ID);
        }

    }
}
