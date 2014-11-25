using System;
using Microsoft.SPOT;

namespace beakn.netduino.app
{
    interface INetduino
    {
        void Setup();
        void Set(string presence);
    }
}
