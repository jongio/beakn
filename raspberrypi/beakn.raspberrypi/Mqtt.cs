using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace beakn.raspberrypi
{
    public class Mqtt
    {
        private IRaspberryPi rpi;
        private MqttClient mqttClient;

        public Mqtt(IRaspberryPi rpi)
        {
            this.rpi = rpi;
        }

        public void Setup()
        {
            mqttClient = new MqttClient(Config.MqttHost, Config.MqttPort, false, null);
            mqttClient.Connect(Config.MqttClientId, Config.MqttUsername, Config.MqttPassword);

            Console.WriteLine("Connected: " + mqttClient.IsConnected);

            mqttClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            mqttClient.MqttMsgSubscribed += client_MqttMsgSubscribed;

            mqttClient.Subscribe(new string[] { Config.MqttTopic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }

        void client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            var sb = new StringBuilder();
            foreach (byte qosLevel in e.GrantedQoSLevels)
            {
                sb.Append(qosLevel);
                sb.Append(" ");
            }

            Console.WriteLine("Message Subscribed - MessageId: " + e.MessageId + ", QOS Levels: " + sb.ToString());
        }

        void client_MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {
            string message = new string(Encoding.UTF8.GetChars(e.Message));
            Console.WriteLine("Message Received - Topic: " + e.Topic + ", Message: " + message + ", QosLevel: " + e.QosLevel + ", Retain: " + e.Retain + ", DupFlag: " + e.DupFlag);
            rpi.Set(message);
        }
    }
}
