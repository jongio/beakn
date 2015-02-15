namespace beakn
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.deviceIdLabel = new System.Windows.Forms.Label();
            this.accessTokenLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.accessTokenTextbox = new System.Windows.Forms.TextBox();
            this.deviceIdTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sparkLinkLabel = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // deviceIdLabel
            // 
            this.deviceIdLabel.AutoSize = true;
            this.deviceIdLabel.Location = new System.Drawing.Point(41, 48);
            this.deviceIdLabel.Name = "deviceIdLabel";
            this.deviceIdLabel.Size = new System.Drawing.Size(56, 13);
            this.deviceIdLabel.TabIndex = 0;
            this.deviceIdLabel.Text = "Device Id:";
            // 
            // accessTokenLabel
            // 
            this.accessTokenLabel.AutoSize = true;
            this.accessTokenLabel.Location = new System.Drawing.Point(18, 77);
            this.accessTokenLabel.Name = "accessTokenLabel";
            this.accessTokenLabel.Size = new System.Drawing.Size(79, 13);
            this.accessTokenLabel.TabIndex = 1;
            this.accessTokenLabel.Text = "Access Token:";
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(286, 121);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 4;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(367, 121);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // accessTokenTextbox
            // 
            this.accessTokenTextbox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::beakn.Properties.Settings.Default, "AccessToken", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.accessTokenTextbox.Location = new System.Drawing.Point(103, 74);
            this.accessTokenTextbox.Name = "accessTokenTextbox";
            this.accessTokenTextbox.Size = new System.Drawing.Size(339, 20);
            this.accessTokenTextbox.TabIndex = 3;
            this.accessTokenTextbox.Text = global::beakn.Properties.Settings.Default.AccessToken;
            // 
            // deviceIdTextbox
            // 
            this.deviceIdTextbox.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::beakn.Properties.Settings.Default, "DeviceId", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.deviceIdTextbox.Location = new System.Drawing.Point(103, 45);
            this.deviceIdTextbox.Name = "deviceIdTextbox";
            this.deviceIdTextbox.Size = new System.Drawing.Size(339, 20);
            this.deviceIdTextbox.TabIndex = 2;
            this.deviceIdTextbox.Text = global::beakn.Properties.Settings.Default.DeviceId;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Spark Settings";
            // 
            // sparkLinkLabel
            // 
            this.sparkLinkLabel.AutoSize = true;
            this.sparkLinkLabel.Location = new System.Drawing.Point(103, 101);
            this.sparkLinkLabel.Name = "sparkLinkLabel";
            this.sparkLinkLabel.Size = new System.Drawing.Size(102, 13);
            this.sparkLinkLabel.TabIndex = 7;
            this.sparkLinkLabel.TabStop = true;
            this.sparkLinkLabel.Text = "http://spark.io/build";
            this.sparkLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.sparkLinkLabel_LinkClicked);
            // 
            // Settings
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(455, 158);
            this.Controls.Add(this.sparkLinkLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.accessTokenTextbox);
            this.Controls.Add(this.deviceIdTextbox);
            this.Controls.Add(this.accessTokenLabel);
            this.Controls.Add(this.deviceIdLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "beakn Settings";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label deviceIdLabel;
        private System.Windows.Forms.Label accessTokenLabel;
        private System.Windows.Forms.TextBox deviceIdTextbox;
        private System.Windows.Forms.TextBox accessTokenTextbox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel sparkLinkLabel;
    }
}