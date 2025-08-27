using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client_scheduler.Models
{
    public class LocationReport
    {
        public string Location { get; set; }
        public int AppointmentCount { get; set; }
        public HashSet<string> AppointmentTypes { get; set; } = new HashSet<string>();
    }
}
