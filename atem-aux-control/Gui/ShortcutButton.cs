using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using midi_aux_control.GuiHelpers;
using midi_aux_control.Model.Persist;

namespace midi_aux_control
{
    public partial class ShortcutButton : UserControl, IPersistable
    {

        int index;

        private Switcher switcher;

        public ShortcutButton(int index, Switcher switcher)
        {
            InitializeComponent();
            Binding = false;
            midiKeyCode = -1;
            selectedAuxPort = null;
            selectedSignal = null;
            this.index = index;
            this.switcher = switcher;
            Persister.Instance.RegisterPersistable(this);
        }

        public bool ReceiveMidi(int code)
        {

            if (Binding)
            {
                MidiKeyCode = code;
                return false;
            }

            if(MidiKeyCode != code)
                return false;

            return click();

        }

        private bool click()
        {
            if ((SelectedAuxPort == null) || (SelectedSignal == null))
                return false;
            InvokeHelper.Invoke(() =>
            {
                SelectedAuxPort.Obj.SetInputSource(SelectedSignal.ID);
            });
            return true;
        }

        private void Button_Click(object sender, EventArgs e)
            => click();

        public void HandleClick()
            => click();

        private void AuxDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedAuxPort = auxDropDown.SelectedValue as AuxPort;
        }

        private void InputDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedSignal = signalDropDown.SelectedValue as Signal;
        }

        public delegate void SelectedAuxPortChangedDelegate(AuxPort auxPort);
        public event SelectedAuxPortChangedDelegate SelectedAuxPortChanged;

        private AuxPort selectedAuxPort;

        public AuxPort SelectedAuxPort
        {
            get => selectedAuxPort;
            set
            {
                selectedAuxPort = value;
                if (auxDropDown.SelectedValue != value)
                    auxDropDown.SelectByValue(value);
                updateButton();
                SelectedAuxPortChanged?.Invoke(value);
                repersist();
            }
        }

        public delegate void SelectedSignalChangedDelegate(Signal signal);
        public event SelectedSignalChangedDelegate SelectedSignalChanged;

        private Signal selectedSignal;

        public Signal SelectedSignal
        {
            get => selectedSignal;
            set
            {

                if (selectedSignal != null)
                {
                    selectedSignal.Monitor.IsProgramTalliedChanged -= SignalIsProgramTalliedHandler;
                    selectedSignal.Monitor.IsPreviewTalliedChanged -= SignalIsPreviewTalliedHandler;
                }
                SignalIsProgramTallied = false;
                SignalIsPreviewTallied = false;

                selectedSignal = value;
                if (signalDropDown.SelectedValue != value)
                    signalDropDown.SelectByValue(value);
                updateButton();

                if (value != null) {
                    selectedSignal.Monitor.IsProgramTalliedChanged += SignalIsProgramTalliedHandler;
                    selectedSignal.Monitor.IsPreviewTalliedChanged += SignalIsPreviewTalliedHandler;
                    SignalIsProgramTallied = value.Monitor.IsProgramTallied;
                    SignalIsPreviewTallied = value.Monitor.IsPreviewTallied;
                }

                SelectedSignalChanged?.Invoke(value);

                repersist();

            }
        }

        private bool signalIsProgramTallied;

        private bool SignalIsProgramTallied
        {
            get => signalIsProgramTallied;
            set
            {
                signalIsProgramTallied = value;
                updateButtonColor();
            }
        }

        private void SignalIsProgramTalliedHandler(bool isProgramTallied)
        {
            SignalIsProgramTallied = isProgramTallied;
        }

        private bool signalIsPreviewTallied;

        private bool SignalIsPreviewTallied
        {
            get => signalIsPreviewTallied;
            set
            {
                signalIsPreviewTallied = value;
                updateButtonColor();
            }
        }

        private void SignalIsPreviewTalliedHandler(bool isPreviewTallied)
        {
            SignalIsPreviewTallied = isPreviewTallied;
        }

        private void updateButtonColor()
        {
            if (signalIsProgramTallied)
            {
                button.BackColor = Color.DarkRed;
                button.ForeColor = Color.White;
            }
            else if (signalIsPreviewTallied)
            {
                button.BackColor = Color.DarkGreen;
                button.ForeColor = Color.White;
            }
            else
            {
                button.BackColor = SystemColors.ControlLight;
                button.ForeColor = Color.DarkBlue;
            }
        }

        private void updateButton()
        {
            if ((SelectedAuxPort == null) || (SelectedSignal == null))
            {
                button.Text = "";
                button.Enabled = false;
                return;
            }

            button.Text = string.Format("{0}\r\n[{1}]", SelectedSignal.LongName, SelectedAuxPort.LongName);
            button.Enabled = true;
        }

        public void AuxPortsChanged(IEnumerable<AuxPort> ports)
        {
            updatingDropDowns = true;
            auxDropDown.CreateAdapterAsDataSource<AuxPort>(ports, p => p.LongName, true, "-");
            updatingDropDowns = false;
        }

        public void SignalsChanged(IEnumerable<Signal> signals)
        {
            updatingDropDowns = true;
            signalDropDown.CreateAdapterAsDataSource<Signal>(signals, s => s.LongName, true, "-");
            updatingDropDowns = false;
        }

        private int midiKeyCode;

        private int MidiKeyCode
        {
            get => midiKeyCode;
            set
            {
                Binding = false;
                midiKeyCode = value;
                bindButton.Text = (value > -1) ? "Clear binding" : "Bind";
                if (value > -1)
                    BindingStopped?.Invoke(this);
                repersist();
            }
        }

        private bool binding = false;

        private bool Binding
        {
            get => binding;
            set
            {
                binding = value;
                bindButton.BackColor = value ? Color.Yellow : SystemColors.Control;
                if (value)
                    BindingStarted?.Invoke(this);
            }
        }

        public string PersistenceKey => string.Format("scb-{0}", index);

        public void CancelBinding()
        {
            Binding = false;
        }

        public delegate void BindingStartedDelegate(ShortcutButton sender);
        public event BindingStartedDelegate BindingStarted;

        public delegate void BindingStoppedDelegate(ShortcutButton sender);
        public event BindingStoppedDelegate BindingStopped;

        private void BindButton_Click(object sender, EventArgs e)
        {
            if (MidiKeyCode > -1)
            {
                MidiKeyCode = -1;
                return;
            }

            if (!Binding)
            {
                Binding = true;
            }
            else
            {
                Binding = false;
                BindingStopped?.Invoke(this);
            }
        }

        bool restoringPersisted = false;

        private string PERSISTENCE_KEY_AUX = "aux";
        private string PERSISTENCE_KEY_SIGNAL = "signal";
        private string PERSISTENCE_KEY_MIDI = "midi";

        public Dictionary<string, string> GetPersistableData()
        {
            return new Dictionary<string, string>()
            {
                { PERSISTENCE_KEY_AUX, (SelectedAuxPort != null) ? SelectedAuxPort.ID.ToString() : "-1" },
                { PERSISTENCE_KEY_SIGNAL, (SelectedSignal != null) ? SelectedSignal.ID.ToString() : "-1" },
                { PERSISTENCE_KEY_MIDI, MidiKeyCode.ToString() },
            };
        }

        public void SetPersistableData(Dictionary<string, string> data)
        {

            restoringPersisted = true;

            if (!data.TryGetValue(PERSISTENCE_KEY_AUX, out string auxPortIdStr))
                auxPortIdStr = "-1";
            if (!long.TryParse(auxPortIdStr, out long auxPortId))
                auxPortId = -1;
            SelectedAuxPort = switcher.GetAuxById(auxPortId);

            if (!data.TryGetValue(PERSISTENCE_KEY_SIGNAL, out string signalIdStr))
                signalIdStr = "-1";
            if (!long.TryParse(signalIdStr, out long signalId))
                signalId = -1;
            SelectedSignal = switcher.GetSignalById(signalId);

            if (!data.TryGetValue(PERSISTENCE_KEY_MIDI, out string midiCodeStr))
                midiCodeStr = "-1";
            if (!int.TryParse(midiCodeStr, out int midiCode))
                midiCode = -1;
            MidiKeyCode = midiCode;

            restoringPersisted = false;

        }

        bool updatingDropDowns = false;

        private void repersist()
        {
            if (!restoringPersisted && !updatingDropDowns)
                Persister.Instance.RequestPersist(this);
        }

    }
}
