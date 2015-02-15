using lyncx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace beakn
{
    public class Controller
    {
        private IProtocol protocol;
        private LyncxClient lyncx;

        public void Setup()
        {
            protocol = ProtocolFactory.Get(Config.Protocol);
            protocol.SendSuccess += protocol_SendSuccess;
            protocol.SendFailure += protocol_SendFailure;
            protocol.MessageReceived += protocol_Receive;
            protocol.EventReceived += protocol_EventReceived;

            protocol.Setup();
            
            lyncx = new LyncxClient();
            lyncx.AvailabilityChanged += lyncx_AvailabilityChanged;
            lyncx.Setup();
        }

        void protocol_EventReceived(object sender, EventSource4Net.ServerSentEventReceivedEventArgs e)
        {
            sendAvailability(sender, new AvailabilityEventArgs(lyncx.Availability));
        }

        void protocol_Receive(object sender, MessageEventArgs e)
        {
            OnLog(new MessageEventArgs(e.Message));
        }

        void lyncx_AvailabilityChanged(object sender, AvailabilityEventArgs e)
        {
            sendAvailability(sender, e);
        }

        void sendAvailability(object sender, AvailabilityEventArgs e)
        {
            try
            {
                protocol.Send(e.Availability.AvailabilityName);
            }
            catch (Exception ex)
            {
                OnLog(new MessageEventArgs("Exception while trying to send command. " + ex.ToString()));
            }
        }

        void protocol_SendFailure(object sender, MessageEventArgs e)
        {
            OnLog(new MessageEventArgs("Send Failure - Exception: " + e.Message));
        }

        void protocol_SendSuccess(object sender, MessageEventArgs e)
        {
            OnLog(new MessageEventArgs("Send Success - Message: " + e.Message));
        }

        public event EventHandler<MessageEventArgs> Log;

        protected virtual void OnLog(MessageEventArgs e)
        {
            EventHandler<MessageEventArgs> handler = Log;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
