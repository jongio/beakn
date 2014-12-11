using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Text;

namespace beakn.netduino.app
{
    public class Program
    {
        static INetduino netduino;
        static MqttClient mqttClient;

        public static void Main()
        {
            netduino = NetduinoFactory.Get(ConfigurationManager.LedPinType);
            netduino.Setup();

            mqttClient = new MqttClient(ConfigurationManager.MqttHost, ConfigurationManager.MqttPort, false, null);
            mqttClient.Connect(ConfigurationManager.MqttClientId, ConfigurationManager.MqttUsername, ConfigurationManager.MqttPassword);


            mqttClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            mqttClient.MqttMsgSubscribed += client_MqttMsgSubscribed;

            mqttClient.Subscribe(new string[] { ConfigurationManager.MqttTopic }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
        }

        static void client_MqttMsgSubscribed(object sender, MqttMsgSubscribedEventArgs e)
        {
            var sb = new StringBuilder();
            foreach (byte qosLevel in e.GrantedQoSLevels)
            {
                sb.Append(qosLevel);
                sb.Append(" ");
            }

            Debug.Print("Message Subscribed - MessageId: " + e.MessageId + ", QOS Levels: " + sb.ToString());

        }

        static void client_MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {
            string message = new string(Encoding.UTF8.GetChars(e.Message));
            Debug.Print("Message Received - Topic: " + e.Topic + ", Message: " + message + ", QosLevel: " + e.QosLevel + ", Retain: " + e.Retain + ", DupFlag: " + e.DupFlag);
            netduino.Set(message);
        }
    }
}
