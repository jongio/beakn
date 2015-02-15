using Microsoft.Lync.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lyncx
{
    public class AvailabilityEventArgs : EventArgs
    {
        public AvailabilityEventArgs(Availability availability)
        {
            this.Availability = availability;
        }

        public Availability Availability { get; set; }
    }
}
