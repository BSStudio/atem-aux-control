using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midi_aux_control
{
    public class Signal
    {

        public long ID { get; set; }
        public string LongName { get; set; }
        public InputMonitor Monitor { get; set; }

        public override bool Equals(object obj)
        {
            Signal signal = obj as Signal;
            if (signal == null)
                return false;
            return (signal.ID == ID);
        }

    }
}
