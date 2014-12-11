using System;
using Microsoft.SPOT;

namespace beakn.netduino.app
{
    static class NetduinoFactory
    {
        public static INetduino Get(string type) {
            switch (type)
            {
                case "Digital": return new NetduinoDigital();
                case "Analog": return new NetduinoAnalog();
                default: throw new ArgumentNullException("Set Netduino type in ConfigurationManager.");
            }
        }
    }
}
