using lyncx;
using System;
using System.Net;
using System.Text;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using config = System.Configuration.ConfigurationManager;

namespace beakn.desktop.mqtt
{
    public class Controller
    {
        private  LyncxClient lyncxClient;
        private  MqttClient mqttClient;

        public void Setup() {
            mqttClient = new MqttClient(IPAddress.Parse(config.AppSettings["MqttHost"]), int.Parse(config.AppSettings["MqttPort"]), false, null);
            mqttClient.MqttMsgPublished += client_MqttMsgPublished;
            mqttClient.Connect(config.AppSettings["MqttClientName"] + "-" + config.AppSettings["MqttPairingCode"], config.AppSettings["MqttUsername"], config.AppSettings["MqttPassword"]);

            lyncxClient = new LyncxClient();
            lyncxClient.AvailabilityChanged += lyncxClient_AvailabilityChanged;

            lyncxClient.Setup();
        }

        void lyncxClient_AvailabilityChanged(object sender, AvailabilityChangedEventArgs e)
        {
            try
            {
                mqttClient.Publish(config.AppSettings["MqttTopic"] + config.AppSettings["MqttPairingCode"], Encoding.UTF8.GetBytes(e.AvailabilityName), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        void client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            Console.WriteLine("Message Published - MessageId: " + e.MessageId);
        }
    }
}
