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
            protocol.Setup();
            protocol.SendSuccess += protocol_SendSuccess;
            protocol.SendFailure += protocol_SendFailure;

            lyncx = new LyncxClient();
            lyncx.AvailabilityChanged += lyncx_AvailabilityChanged;
            lyncx.Setup();

        }

        void lyncx_AvailabilityChanged(object sender, AvailabilityChangedEventArgs e)
        {
            try
            {
                protocol.Send(e.AvailabilityName);
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

        public event LogEventHandler Log;
        public delegate void LogEventHandler(object sender, MessageEventArgs e);

        protected virtual void OnLog(MessageEventArgs e)
        {
            LogEventHandler handler = Log;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
