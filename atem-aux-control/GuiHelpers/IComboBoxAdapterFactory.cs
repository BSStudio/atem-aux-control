﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midi_aux_control.GuiHelpers
{
    public interface IComboBoxAdapterFactory
    {
        IComboBoxAdapter GetOne();
    }
}
