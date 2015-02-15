using Microsoft.Lync.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lyncx
{
    public class Availability
    {
        public Availability(ContactAvailability contactAvailability, string availabilityName)
        {
            this.ContactAvailability = contactAvailability;
            this.AvailabilityName = availabilityName;
        }

        public ContactAvailability ContactAvailability { get; set; }
        public string AvailabilityName { get; set; }
    }
}
