using Microsoft.Lync.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lyncx
{
    public class LyncxClient
    {
        protected LyncClient lyncClient = null;

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

            while (lyncClient == null)
            {
                try
                {
                    lyncClient = LyncClient.GetClient();
                    lyncClient.StateChanged -= new EventHandler<ClientStateChangedEventArgs>(Client_StateChanged);
                    lyncClient.StateChanged += new EventHandler<ClientStateChangedEventArgs>(Client_StateChanged);
                    
                }
                catch (ClientNotFoundException e)
                {
                    // Eat this for now.  It just means that the Lync client isn't running on the desktop.  
                    // TODO figure out a better way to do this.
                    Thread.Sleep(1000);
                }
            }

            if (lyncClient.Self != null && lyncClient.Self.Contact != null)
            {
                lyncClient.Self.Contact.ContactInformationChanged -= new EventHandler<ContactInformationChangedEventArgs>(SelfContact_ContactInformationChanged);
                lyncClient.Self.Contact.ContactInformationChanged += new EventHandler<ContactInformationChangedEventArgs>(SelfContact_ContactInformationChanged);

                SetAvailability();
            }
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
            if (lyncClient.State == ClientState.SignedIn)
            {
                //Get the current availability value from Lync
                ContactAvailability currentAvailability = 0;

                currentAvailability = (ContactAvailability)lyncClient.Self.Contact.GetContactInformation(ContactInformationType.Availability);
                string currentAvailabilityName = Enum.GetName(typeof(ContactAvailability), currentAvailability);

                OnAvailabilityChanged(new AvailabilityChangedEventArgs(currentAvailability, currentAvailabilityName));
            }
        }

        private void Client_StateChanged(object sender, ClientStateChangedEventArgs e)
        {
            switch (e.NewState)
            {
                case ClientState.SignedIn:
                    Setup();
                    break;
                case ClientState.SigningOut:
                    OnAvailabilityChanged(new AvailabilityChangedEventArgs(ContactAvailability.Offline, "Offline"));
                    Setup();
                    break;
                default:
                    break;
            }
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
