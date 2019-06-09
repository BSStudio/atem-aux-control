namespace midi_aux_control
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mixerConnectButton = new System.Windows.Forms.Button();
            this.mixerIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.auxTallyPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.auxTallySelector = new System.Windows.Forms.ComboBox();
            this.auxTallySourceName = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.auxTallyPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mixerConnectButton);
            this.groupBox1.Controls.Add(this.mixerIP);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(13, 724);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(787, 58);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mixer";
            // 
            // mixerConnectButton
            // 
            this.mixerConnectButton.Location = new System.Drawing.Point(205, 18);
            this.mixerConnectButton.Margin = new System.Windows.Forms.Padding(4);
            this.mixerConnectButton.Name = "mixerConnectButton";
            this.mixerConnectButton.Size = new System.Drawing.Size(185, 28);
            this.mixerConnectButton.TabIndex = 18;
            this.mixerConnectButton.Text = "Connect";
            this.mixerConnectButton.UseVisualStyleBackColor = true;
            this.mixerConnectButton.Click += new System.EventHandler(this.MixerConnectButton_Click);
            // 
            // mixerIP
            // 
            this.mixerIP.Location = new System.Drawing.Point(43, 21);
            this.mixerIP.Margin = new System.Windows.Forms.Padding(4);
            this.mixerIP.Name = "mixerIP";
            this.mixerIP.Size = new System.Drawing.Size(153, 22);
            this.mixerIP.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP:";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(4, 19);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(779, 441);
            this.flowLayoutPanel1.TabIndex = 17;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.flowLayoutPanel1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(13, 223);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(787, 464);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Shortcuts";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.auxTallyPanel);
            this.groupBox3.Controls.Add(this.panel1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(13, 12);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(10);
            this.groupBox3.Size = new System.Drawing.Size(787, 211);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "UMD";
            // 
            // auxTallyPanel
            // 
            this.auxTallyPanel.BackColor = System.Drawing.Color.White;
            this.auxTallyPanel.Controls.Add(this.auxTallySourceName);
            this.auxTallyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.auxTallyPanel.Location = new System.Drawing.Point(10, 25);
            this.auxTallyPanel.Name = "auxTallyPanel";
            this.auxTallyPanel.Padding = new System.Windows.Forms.Padding(8);
            this.auxTallyPanel.Size = new System.Drawing.Size(767, 142);
            this.auxTallyPanel.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.auxTallySelector);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(10, 167);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(4);
            this.panel1.Size = new System.Drawing.Size(767, 34);
            this.panel1.TabIndex = 1;
            // 
            // auxTallySelector
            // 
            this.auxTallySelector.Dock = System.Windows.Forms.DockStyle.Right;
            this.auxTallySelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.auxTallySelector.FormattingEnabled = true;
            this.auxTallySelector.Location = new System.Drawing.Point(601, 4);
            this.auxTallySelector.Name = "auxTallySelector";
            this.auxTallySelector.Size = new System.Drawing.Size(162, 24);
            this.auxTallySelector.TabIndex = 0;
            this.auxTallySelector.SelectedIndexChanged += new System.EventHandler(this.AuxTallySelector_SelectedIndexChanged);
            // 
            // auxTallySourceName
            // 
            this.auxTallySourceName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.auxTallySourceName.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.auxTallySourceName.Location = new System.Drawing.Point(8, 8);
            this.auxTallySourceName.Name = "auxTallySourceName";
            this.auxTallySourceName.Size = new System.Drawing.Size(751, 126);
            this.auxTallySourceName.TabIndex = 0;
            this.auxTallySourceName.Text = "-";
            this.auxTallySourceName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 794);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.Text = "ATEM Aux Control";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.auxTallyPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button mixerConnectButton;
        private System.Windows.Forms.TextBox mixerIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel auxTallyPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox auxTallySelector;
        private System.Windows.Forms.Label auxTallySourceName;
    }
}

