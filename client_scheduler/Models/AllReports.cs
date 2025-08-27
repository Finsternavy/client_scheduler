using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client_scheduler.Models
{
    public class AllReports
    {
        public Dictionary<string, List<AppointmentTypeReport>> AppointmentTypes { get; set; }
        public Dictionary<int, List<UserScheduleReport>> UserSchedules { get; set; }
        public Dictionary<string, LocationReport> Locations { get; set; }
        public ReportDisplayData AppointmentTypesDisplay { get; set; }
        public ReportDisplayData UserSchedulesDisplay { get; set; }
        public ReportDisplayData LocationsDisplay { get; set; }
    }
}
