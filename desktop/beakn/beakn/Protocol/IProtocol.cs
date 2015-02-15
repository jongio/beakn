using EventSource4Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beakn
{
    public interface IProtocol
    {
        void Setup();
        void Send(string message);
        event EventHandler<MessageEventArgs> SendSuccess;
        event EventHandler<MessageEventArgs> SendFailure;
        event EventHandler<MessageEventArgs> MessageReceived;
        event EventHandler<ServerSentEventReceivedEventArgs> EventReceived;
    }
}
