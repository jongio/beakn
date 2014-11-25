using lyncx;
using System;
using System.Net;
using System.Text;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using config = System.Configuration.ConfigurationManager;
using Microsoft.Win32;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace beakn.desktop.mqtt
{
    static class Program
    {
        private static LyncxClient lyncxClient;
        private static MqttClient mqttClient;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            mqttClient = new MqttClient(IPAddress.Parse(config.AppSettings["MqttHost"]), int.Parse(config.AppSettings["MqttPort"]), false, null);
            mqttClient.MqttMsgPublished += client_MqttMsgPublished;
            mqttClient.Connect(config.AppSettings["MqttClientName"] + "-" + config.AppSettings["MqttPairingCode"], config.AppSettings["MqttUsername"], config.AppSettings["MqttPassword"]);

            lyncxClient = new LyncxClient();
            lyncxClient.AvailabilityChanged += lyncxClient_AvailabilityChanged;

            lyncxClient.Setup();

            #region Windows Forms Init
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            #endregion
        }

        static void lyncxClient_AvailabilityChanged(object sender, AvailabilityChangedEventArgs e)
        {
            mqttClient.Publish(config.AppSettings["MqttTopic"] + config.AppSettings["MqttPairingCode"], Encoding.UTF8.GetBytes(e.AvailabilityName), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);

        }

        static void client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            Console.WriteLine("Message Published - MessageId: " + e.MessageId);
        }
    }
}
