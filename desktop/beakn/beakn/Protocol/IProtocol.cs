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
        event MessageEventHandler SendSuccess;
        event MessageEventHandler SendFailure;
        event MessageEventHandler Receive;

    }
}
