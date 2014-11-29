using lyncx;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace beakn.desktop.serial
{
    public class Controller
    {
        private LyncxClient lyncxClient;
        private Serial arduinoSerial;

        public void Setup()
        {
            arduinoSerial = new Serial { RunLoop = true };
            arduinoSerial.Setup();

            lyncxClient = new LyncxClient();
            lyncxClient.AvailabilityChanged += lyncxClient_AvailabilityChanged;
            lyncxClient.Setup();
        }

        void lyncxClient_AvailabilityChanged(object sender, AvailabilityChangedEventArgs e)
        {
            try
            {
                arduinoSerial.Set(e.Availability);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
