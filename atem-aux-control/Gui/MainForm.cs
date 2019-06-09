using midi_aux_control.GuiHelpers;
using midi_aux_control.Model;
using midi_aux_control.Model.Persist;
using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Windows.Forms;

namespace midi_aux_control
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            initMidi();
            initSwitcher();
            initAuxTally();
            initSerial();
            initButtons(8);
            Persister.Instance.Load();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        #region Binding

        private ShortcutButton currentlyBinding;

        public ShortcutButton CurrentlyBinding
        {
            get => currentlyBinding;
            set
            {
                currentlyBinding = value;
                foreach (var scb in buttons)
                {
                    if (scb != value)
                        scb.CancelBinding();
                }
            }
        }

        private void Scb_BindingStopped(ShortcutButton sender)
        {
            CurrentlyBinding = null;
        }

        private void Scb_BindingStarted(ShortcutButton sender)
        {
            CurrentlyBinding = sender;
        }
        #endregion

        #region MIDI
        private InputDevice inDevice = null;

        private void initMidi()
        {

            if (InputDevice.DeviceCount == 0)
            {
                MessageBox.Show("No MIDI input devices available.", "MIDI error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                inDevice = new InputDevice(0);
                inDevice.MessageReceived += handleMidiMessage;
                inDevice.StartRecording();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "MIDI error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void handleMidiMessage(IMidiMessage message)
        {

            byte[] b = message.GetBytes();

            if (b[0] != 144)
                return;

            foreach (var scb in buttons)
                scb.ReceiveMidi(b[1]);

        }
        #endregion

        #region Serial
        private SerialPort serialPort;

        private void initSerial()
        {
            try
            {
                serialPort = new SerialPort("COM10");
                serialPort.DataReceived += SerialPort_DataReceived;
                serialPort.Open();
            }
            catch
            {
                MessageBox.Show("Couldn't open COM10.", "Serial error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                while(serialPort.BytesToRead > 0)
                {
                    int chr = serialPort.ReadChar();
                    if ((chr >= '1') && (chr <= '8'))
                        buttons[chr - '1'].HandleClick();
                }
            }
            catch
            { }
        }
        #endregion

        #region Switcher
        private Switcher switcher;

        private void initSwitcher()
        {
            switcher = new Switcher();
            switcher.ConnectedChanged += Switcher_ConnectedChanged;
            switcher.IpAddressChanged += Switcher_IpAddressChanged;
        }

        private void Switcher_IpAddressChanged(string newIp)
        {
            mixerIP.Text = newIp;
        }

        private void Switcher_ConnectedChanged(Switcher switcher, bool connected)
        {
            updateDropDowns();
            Persister.Instance.Distribute(switcher);
        }
        private void MixerConnectButton_Click(object sender, EventArgs e)
        {
            switcher.IpAddress = mixerIP.Text;
            switcher.Connect();
        }
        #endregion

        #region Buttons
        private List<ShortcutButton> buttons = new List<ShortcutButton>();

        private void initButtons(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var scb = new ShortcutButton(i, switcher);
                flowLayoutPanel1.Controls.Add(scb);
                buttons.Add(scb);
                scb.BindingStarted += Scb_BindingStarted;
                scb.BindingStopped += Scb_BindingStopped;
            }
        }
        #endregion

        private void updateDropDowns()
        {
            foreach (var scb in buttons)
            {
                scb.AuxPortsChanged(switcher.Auxes);
                scb.SignalsChanged(switcher.Signals);
            }
            auxTallySelector.CreateAdapterAsDataSource<AuxPort>(switcher.Auxes, p => p.LongName, true, "-");
        }

        #region Aux tally
        private AuxTally auxTally = null;

        private void AuxTallySelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            auxTally.Port = auxTallySelector.SelectedValue as AuxPort;
        }

        private void initAuxTally()
        {
            auxTally = new AuxTally("0", switcher);
            auxTally.PortChanged += AuxTally_PortChanged;
            auxTally.TallyStateChanged += AuxTally_TallyStateChanged;
            auxTally.SourceNameChanged += AuxTally_SourceNameChanged;
        }

        private void AuxTally_SourceNameChanged(string newName)
        {
            auxTallySourceName.Text = newName;
        }

        private void AuxTally_TallyStateChanged(AuxTally.TallyStateEnum newState)
        {
            System.Drawing.Color backColor = System.Drawing.SystemColors.ControlLightLight;
            System.Drawing.Color textColor = System.Drawing.Color.Black;
            switch (newState)
            {
                case AuxTally.TallyStateEnum.Program:
                    backColor = System.Drawing.Color.DarkRed;
                    textColor = System.Drawing.Color.White;
                    break;
                case AuxTally.TallyStateEnum.Preview:
                    backColor = System.Drawing.Color.DarkGreen;
                    textColor = System.Drawing.Color.White;
                    break;
            }
            auxTallyPanel.BackColor = backColor;
            auxTallyPanel.ForeColor = textColor;
        }

        private void AuxTally_PortChanged(AuxPort newPort)
        {
            auxTallySelector.SelectByValue(newPort);
        }

        #endregion

        

    }

}
