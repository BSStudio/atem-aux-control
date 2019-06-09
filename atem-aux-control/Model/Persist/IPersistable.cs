using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midi_aux_control.Model.Persist
{
    public interface IPersistable
    {
        string PersistenceKey { get; }
        Dictionary<string, string> GetPersistableData();
        void SetPersistableData(Dictionary<string, string> data);
    }
}
