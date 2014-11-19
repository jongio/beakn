using Microsoft.Lync.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lyncx
{
    public class AvailabilityChangedEventArgs : EventArgs
    {
        public AvailabilityChangedEventArgs(ContactAvailability availability, string availabilityName)
        {
            this.Availability = availability;
            this.AvailabilityName = availabilityName;
        }

        public ContactAvailability Availability { get; set; }
        public string AvailabilityName { get; set; }
    }
}
