using Microsoft.Lync.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace lyncx
{
    public class LyncxClient
    {
        protected LyncClient lyncClient;

        public event AvailabilityChangedEventHandler AvailabilityChanged;
        public delegate void AvailabilityChangedEventHandler(object sender, AvailabilityChangedEventArgs e);

        protected virtual void OnAvailabilityChanged(AvailabilityChangedEventArgs e)
        {
            AvailabilityChangedEventHandler handler = AvailabilityChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public LyncxClient() { }

        public void Setup()
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

        private void SelfContact_ContactInformationChanged(object sender, ContactInformationChangedEventArgs e)
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

        private void SetAvailability()
        {
            //Get the current availability value from Lync
            ContactAvailability currentAvailability = 0;
            try
            {
                currentAvailability = (ContactAvailability)lyncClient.Self.Contact.GetContactInformation(ContactInformationType.Availability);
                string currentAvailabilityName = Enum.GetName(typeof(ContactAvailability), currentAvailability);

                OnAvailabilityChanged(new AvailabilityChangedEventArgs(currentAvailability, currentAvailabilityName));
               
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

        private void Client_StateChanged(object sender, ClientStateChangedEventArgs e)
        {
        }

        /// <summary>
        /// Identify if a particular SystemException is one of the exceptions which may be thrown
        /// by the Lync Model API.
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private bool IsLyncException(SystemException ex)
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
