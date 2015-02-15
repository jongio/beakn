using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace beakn
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.AccessToken = accessTokenTextbox.Text;
            Properties.Settings.Default.DeviceId = deviceIdTextbox.Text;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void sparkLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(sparkLinkLabel.Text);
        }
    }
}
