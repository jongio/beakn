using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beakn
{
    public delegate void MessageEventHandler(object sender, MessageEventArgs e);

    public class Protocol : IProtocol
    {
        public event MessageEventHandler SendSuccess;
        public event MessageEventHandler SendFailure;
        public event MessageEventHandler Receive;

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

        protected virtual void OnReceive(MessageEventArgs e)
        {
            if (Receive != null)
            {
                Receive(this, e);
            }
        }
    }
}
