using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace beakn
{
    public partial class Beakn : Form
    {
        private static Controller controller = new Controller();
        private int maxLogLength = 1000;

        public Beakn()
        {
            InitializeComponent();
            controller.Log += controller_Log;
            setupController();
        }

        void setupController()
        {
            try
            {
                controller.Setup();
            }
            catch (ArgumentNullException e)
            {
                MessageBox.Show(e.Message, this.Text);
                showSettingsDialog();
            }
        }

        void showSettingsDialog()
        {
            using (var settings = new Settings())
            {
                if (settings.ShowDialog(this) == DialogResult.OK)
                {
                    setupController();
                }
            }
        }

        void controller_Log(object sender, MessageEventArgs e)
        {
            if (ControlInvokeRequired(logBox, () => controller_Log(sender, e))) return;

            // Do some trimming so we don't bloat the beakn.exe process
            var log = string.Concat(DateTime.Now, Environment.NewLine, e.Message, Environment.NewLine, Environment.NewLine, logBox.Text);
            if (log.Length > maxLogLength)
            {
                log = log.Substring(0, maxLogLength - 1);
            }

            logBox.Text = log;
        }

        /// <summary>
        /// Helper method to determin if invoke required, if so will rerun method on correct thread.
        /// if not do nothing. From: http://stackoverflow.com/a/26506378/457880
        /// </summary>
        /// <param name="c">Control that might require invoking</param>
        /// <param name="a">action to preform on control thread if so.</param>
        /// <returns>true if invoke required</returns>
        public bool ControlInvokeRequired(Control c, Action a)
        {
            if (c.InvokeRequired) c.Invoke(new MethodInvoker(delegate { a(); }));
            else return false;

            return true;
        }

        private void settingsLabel_Click(object sender, EventArgs e)
        {
            showSettingsDialog();
        }

        private void Beakn_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Closing this form will shutdown beakn and your status light will not reflect your current status. Are you sure you want to quit beakn?", this.Text, MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }
    }
}
