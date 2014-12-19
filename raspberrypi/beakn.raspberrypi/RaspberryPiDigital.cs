using Raspberry.IO.GeneralPurpose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beakn.raspberrypi
{
    public class RaspberryPiDigital : IRaspberryPi, IDisposable 
    {
        OutputPinConfiguration redLed;
        OutputPinConfiguration greenLed;
        OutputPinConfiguration yellowLed;
        OutputPinConfiguration[] leds;
        GpioConnection conn;

        public void Setup()
        {
            redLed = ConnectorPin.P1Pin12.Output();
            yellowLed = ConnectorPin.P1Pin16.Output();
            greenLed = ConnectorPin.P1Pin18.Output();
            leds = new OutputPinConfiguration[] { redLed, greenLed, yellowLed };
            conn = new GpioConnection(leds);
        }

        public void Set(string presence)
        {
            conn[redLed] = false;
            conn[yellowLed] = false;
            conn[greenLed] = false;

            switch (presence)
            {
                case "DoNotDisturb":
                case "Busy":
                    conn[redLed] = true;
                    return;
                case "TemporarilyAway":
                case "Away":
                    conn[yellowLed] = true;
                    return;
                case "Free":
                    conn[greenLed] = true;
                    return;
                default:
                    return;
            }
        }

        public void Dispose()
        {
            conn.Close();
        }
    }
}
