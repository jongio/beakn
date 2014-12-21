using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beakn
{
    public static class ProtocolFactory
    {
        public static IProtocol Get(string type)
        {
            switch (type.ToLower())
            {
                case "sparkcore": return new SparkCore();
                case "mqtt": return new Mqtt();
                case "serial": return new Serial();
                default: throw new ArgumentNullException("Set Protocol in config");
            }
        }
    }
}
