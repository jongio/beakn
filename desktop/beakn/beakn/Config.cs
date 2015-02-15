using CommandMessenger;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beakn
{
    public static class Config
    {
        public static string Protocol = ConfigurationManager.AppSettings["Protocol"];
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

        public static string SerialCOMPort = ConfigurationManager.AppSettings["SerialCOMPort"];

        public static int SerialBaudRate
        {
            get
            {

                int baud = 0;
                int.TryParse(ConfigurationManager.AppSettings["SerialBaudRate"], out baud);
                return baud;
            }
        }

        public static bool SerialDtrEnable
        {
            get
            {

                bool dtr = false;
                bool.TryParse(ConfigurationManager.AppSettings["SerialDtrEnable"], out dtr);
                return dtr;
            }
        }

        public static BoardType SerialBoardType
        {
            get
            {

                BoardType boardType = BoardType.Bit16;
                Enum.TryParse(ConfigurationManager.AppSettings["SerialBoardtype"], true, out boardType);
                return boardType;
            }
        }
    }
}
