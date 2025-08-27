using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client_scheduler.Models
{
    public class UserScheduleReport
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string AppointmentTitle { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string CustomerName { get; set; }
        public string Type { get; set; }
    }
}
