using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace beakn
{
    public class Mqtt : Protocol
    {
        private MqttClient mqttClient;

        public override void Setup()
        {
            mqttClient = new MqttClient(Config.MqttHost, Config.MqttPort, false, null);
            mqttClient.Connect(Config.MqttClientId, Config.MqttUsername, Config.MqttPassword);
        }

        public override void Send(string message)
        {
            try
            {
                ushort id = mqttClient.Publish(Config.MqttTopic, Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
                OnSendSuccess(new MessageEventArgs(string.Format("Message Publish Success: Id={0}, Message={1}", id, message)));
            }
            catch (Exception e)
            {
                OnSendFailure(new MessageEventArgs("Message Publish Failure: " + e.ToString()));
            }
        }
    }
}
