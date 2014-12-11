using System;
using Microsoft.SPOT;
using System.Collections;
using System.IO;
using System.Xml;

namespace beakn.netduino.app
{
    public static class ConfigurationManager
    {
        static ConfigurationManager()
        {
            
        }

        public static AppSettings AppSettings = new AppSettings();

        public static string MqttHost = ConfigurationManager.AppSettings["MqttHost"];
        public static int MqttPort = Convert.ToInt32(ConfigurationManager.AppSettings["MqttPort"].ToString());
        public static string MqttClientName = ConfigurationManager.AppSettings["MqttClientName"];
        public static string MqttPairingCode = ConfigurationManager.AppSettings["MqttPairingCode"];
        public static string MqttClientId = MqttClientName + "-" + Guid.NewGuid().ToString();  // This just needs to be unique per participant in the MQTT pipeline
        public static string MqttUsername = ConfigurationManager.AppSettings["MqttUsername"];
        public static string MqttPassword = ConfigurationManager.AppSettings["MqttPassword"];
        public static string MqttTopicRoot = ConfigurationManager.AppSettings["MqttTopicRoot"];
        public static string MqttTopic = MqttTopicRoot + MqttPairingCode;
        public static string LedPinType = ConfigurationManager.AppSettings["LedPinType"];
        
        public static bool InvertedPins
        {
            get
            {
                return string.Compare(ConfigurationManager.AppSettings["InvertedPins"].ToLower(), "true") == 0;
            }
        }
    }
}
