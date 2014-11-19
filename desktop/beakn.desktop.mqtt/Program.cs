using Microsoft.Lync.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using config = System.Configuration.ConfigurationManager;

namespace beakn.desktop.mqtt
{
    static class Program
    {
        private static LyncClient lyncClient;
        private static MqttClient mqttClient;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            mqttClient = new MqttClient(IPAddress.Parse(config.AppSettings["MqttHost"]), int.Parse(config.AppSettings["MqttPort"]), false, null);
            mqttClient.Connect(config.AppSettings["MqttClientName"] + "-" + config.AppSettings["MqttPairingCode"], config.AppSettings["MqttUsername"], config.AppSettings["MqttPassword"]);
            mqttClient.MqttMsgPublished += client_MqttMsgPublished;

            LyncSetup();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        static void client_MqttMsgPublished(object sender, MqttMsgPublishedEventArgs e)
        {
            Console.WriteLine("Message Published - MessageId: " + e.MessageId);
        }

        static void LyncSetup()
        {
            //Listen for events of changes in the state of the client
            try
            {
                lyncClient = LyncClient.GetClient();
            }
            catch (ClientNotFoundException clientNotFoundException)
            {
                Console.WriteLine(clientNotFoundException);
                return;
            }
            catch (NotStartedByUserException notStartedByUserException)
            {
                Console.Out.WriteLine(notStartedByUserException);
                return;
            }
            catch (LyncClientException lyncClientException)
            {
                Console.Out.WriteLine(lyncClientException);
                return;
            }
            catch (SystemException systemException)
            {
                if (IsLyncException(systemException))
                {
                    // Log the exception thrown by the Lync Model API.
                    Console.WriteLine("Error: " + systemException);
                    return;
                }
                else
                {
                    // Rethrow the SystemException which did not come from the Lync Model API.
                    throw;
                }
            }

            lyncClient.StateChanged +=
                new EventHandler<ClientStateChangedEventArgs>(Client_StateChanged);

            lyncClient.Self.Contact.ContactInformationChanged +=
                   new EventHandler<ContactInformationChangedEventArgs>(SelfContact_ContactInformationChanged);

            SetAvailability();

        }

        private static void SelfContact_ContactInformationChanged(object sender, ContactInformationChangedEventArgs e)
        {
            //Only update the contact information in the user interface if the client is signed in.
            //Ignore other states including transitions (e.g. signing in or out).
            if (lyncClient.State == ClientState.SignedIn)
            {
                //Get from Lync only the contact information that changed.
                if (e.ChangedContactInformation.Contains(ContactInformationType.Availability))
                {
                    //Use the current dispatcher to update the contact's availability in the user interface.
                    SetAvailability();
                }
            }
        }

        private static void SetAvailability()
        {
            //Get the current availability value from Lync
            ContactAvailability currentAvailability = 0;
            try
            {
                currentAvailability = (ContactAvailability)lyncClient.Self.Contact.GetContactInformation(ContactInformationType.Availability);
                string currentAvailabilityName = Enum.GetName(typeof(ContactAvailability), currentAvailability);
                mqttClient.Publish(config.AppSettings["MqttTopic"] + config.AppSettings["MqttPairingCode"], Encoding.UTF8.GetBytes(currentAvailabilityName), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
            }
            catch (LyncClientException e)
            {
                Console.WriteLine(e);
            }
            catch (SystemException systemException)
            {
                if (IsLyncException(systemException))
                {
                    // Log the exception thrown by the Lync Model API.
                    Console.WriteLine("Error: " + systemException);
                }
                else
                {
                    // Rethrow the SystemException which did not come from the Lync Model API.
                    throw;
                }
            }
        }

        private static void Client_StateChanged(object sender, ClientStateChangedEventArgs e)
        {
        }

        /// <summary>
        /// Identify if a particular SystemException is one of the exceptions which may be thrown
        /// by the Lync Model API.
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        static private bool IsLyncException(SystemException ex)
        {
            return
                ex is NotImplementedException ||
                ex is ArgumentException ||
                ex is NullReferenceException ||
                ex is NotSupportedException ||
                ex is ArgumentOutOfRangeException ||
                ex is IndexOutOfRangeException ||
                ex is InvalidOperationException ||
                ex is TypeLoadException ||
                ex is TypeInitializationException ||
                ex is InvalidComObjectException ||
                ex is InvalidCastException;
        }
    }
}
