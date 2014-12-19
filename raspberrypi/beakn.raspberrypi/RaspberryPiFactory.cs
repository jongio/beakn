using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beakn.raspberrypi
{
    public static class RaspberryPiFactory
    {
        public static IRaspberryPi Get(string type)
        {
            switch (type)
            {
                case "Digital": return new RaspberryPiDigital();
                default: throw new ArgumentNullException("Set RaspberryPi type in ConfigurationManager.");
            }
        }
    }
}
