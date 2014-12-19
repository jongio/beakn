using Raspberry.IO.GeneralPurpose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace beakn.raspberrypi
{
    class Program
    {
        static void Main(string[] args)
        {
            var rpi = RaspberryPiFactory.Get(Config.LedPinType);
            rpi.Setup();

            var mqtt = new Mqtt(rpi);
            mqtt.Setup();
        }
    }
}
