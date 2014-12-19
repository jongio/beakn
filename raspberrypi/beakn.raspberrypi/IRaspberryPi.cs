using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beakn.raspberrypi
{
    public interface IRaspberryPi
    {
        void Setup();
        void Set(string presence);
    }
}
