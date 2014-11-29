using lyncx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace beakn.desktop.serial
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                var c = new Controller();
                c.Setup();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            #region Windows Forms Init
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BeaknForm());
            #endregion
        }
    }
}
