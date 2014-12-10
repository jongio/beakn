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
        public Beakn(Controller controller)
        {
            InitializeComponent();
            controller.Log += controller_Log;

            
        }

        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        void controller_Log(object sender, MessageEventArgs e)
        {
            if (ControlInvokeRequired(textBox1, () => controller_Log(sender, e))) return;
            textBox1.Text = e.Message + Environment.NewLine + textBox1.Text;
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
    }
}
