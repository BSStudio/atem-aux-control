namespace midi_aux_control
{
    partial class ShortcutButton
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.signalDropDown = new System.Windows.Forms.ComboBox();
            this.auxDropDown = new System.Windows.Forms.ComboBox();
            this.button = new System.Windows.Forms.Button();
            this.bindButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // signalDropDown
            // 
            this.signalDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.signalDropDown.FormattingEnabled = true;
            this.signalDropDown.Location = new System.Drawing.Point(5, 106);
            this.signalDropDown.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.signalDropDown.Name = "signalDropDown";
            this.signalDropDown.Size = new System.Drawing.Size(165, 24);
            this.signalDropDown.TabIndex = 22;
            this.signalDropDown.SelectedIndexChanged += new System.EventHandler(this.InputDropDown_SelectedIndexChanged);
            // 
            // auxDropDown
            // 
            this.auxDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.auxDropDown.FormattingEnabled = true;
            this.auxDropDown.Location = new System.Drawing.Point(5, 73);
            this.auxDropDown.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.auxDropDown.Name = "auxDropDown";
            this.auxDropDown.Size = new System.Drawing.Size(165, 24);
            this.auxDropDown.TabIndex = 21;
            this.auxDropDown.SelectedIndexChanged += new System.EventHandler(this.AuxDropDown_SelectedIndexChanged);
            // 
            // button
            // 
            this.button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button.ForeColor = System.Drawing.Color.DarkBlue;
            this.button.Location = new System.Drawing.Point(4, 4);
            this.button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(166, 62);
            this.button.TabIndex = 19;
            this.button.Text = "CAM1\r\n[AUX1]";
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.Button_Click);
            // 
            // bindButton
            // 
            this.bindButton.Location = new System.Drawing.Point(29, 139);
            this.bindButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bindButton.Name = "bindButton";
            this.bindButton.Size = new System.Drawing.Size(116, 28);
            this.bindButton.TabIndex = 23;
            this.bindButton.Text = "Bind";
            this.bindButton.UseVisualStyleBackColor = true;
            this.bindButton.Click += new System.EventHandler(this.BindButton_Click);
            // 
            // ShortcutButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.bindButton);
            this.Controls.Add(this.signalDropDown);
            this.Controls.Add(this.auxDropDown);
            this.Controls.Add(this.button);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "ShortcutButton";
            this.Size = new System.Drawing.Size(174, 171);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox signalDropDown;
        private System.Windows.Forms.ComboBox auxDropDown;
        private System.Windows.Forms.Button button;
        private System.Windows.Forms.Button bindButton;
    }
}
