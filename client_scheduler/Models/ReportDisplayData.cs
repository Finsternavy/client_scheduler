using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client_scheduler.Models
{
    public class ReportDisplayData
    {
        public string Title { get; set; } = "";
        public List<string> Headers { get; set; } = new List<string>();
        public List<List<string>> Rows { get; set; } = new List<List<string>>();
        public Dictionary<string, object> Summary { get; set; } = new Dictionary<string, object>();
    }
}
