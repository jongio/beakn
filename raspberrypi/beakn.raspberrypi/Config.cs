using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beakn.raspberrypi
{
    public static class Config
    {
        public static string MqttHost = ConfigurationManager.AppSettings["MqttHost"];
        public static int MqttPort
        {
            get
            {

                int port = 0;
                int.TryParse(ConfigurationManager.AppSettings["MqttPort"], out port);
                return port;
            }
        }

        public static string MqttClientName = ConfigurationManager.AppSettings["MqttClientName"];
        public static string MqttPairingCode = ConfigurationManager.AppSettings["MqttPairingCode"];
        public static string MqttClientId = MqttClientName + "-" + Guid.NewGuid().ToString();  // This just needs to be unique per participant in the MQTT pipeline
        public static string MqttUsername = ConfigurationManager.AppSettings["MqttUsername"];
        public static string MqttPassword = ConfigurationManager.AppSettings["MqttPassword"];
        public static string MqttTopicRoot = ConfigurationManager.AppSettings["MqttTopicRoot"];
        public static string MqttTopic = MqttTopicRoot + MqttPairingCode;
        public static string LedPinType = ConfigurationManager.AppSettings["LedPinType"];
    }
}
