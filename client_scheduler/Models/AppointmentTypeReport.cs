using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client_scheduler.Models
{
    public class AppointmentTypeReport
    {
        public string Month { get; set; }
        public string Type { get; set; }
        public int Count { get; set; }
    }
}
