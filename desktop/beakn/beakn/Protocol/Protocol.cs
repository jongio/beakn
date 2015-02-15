using EventSource4Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beakn
{
    public class Protocol : IProtocol
    {
        public event EventHandler<MessageEventArgs> SendSuccess;
        public event EventHandler<MessageEventArgs> SendFailure;
        public event EventHandler<MessageEventArgs> MessageReceived;
        public event EventHandler<ServerSentEventReceivedEventArgs> EventReceived;

        public virtual void Send(string message){}
        public virtual void Setup() { }

        protected virtual void OnSendSuccess(MessageEventArgs e)
        {
            if (SendSuccess != null)
            {
                SendSuccess(this, e);
            }
        }

        protected virtual void OnSendFailure(MessageEventArgs e)
        {
            if (SendFailure != null)
            {
                SendFailure(this, e);
            }
        }

        protected virtual void OnMessageReceived(MessageEventArgs e)
        {
            if (MessageReceived != null)
            {
                MessageReceived(this, e);
            }
        }

        protected virtual void OnEventReceived(ServerSentEventReceivedEventArgs e)
        {
            if (EventReceived != null)
            {
                EventReceived(this, e);
            }
        }
    }
}
