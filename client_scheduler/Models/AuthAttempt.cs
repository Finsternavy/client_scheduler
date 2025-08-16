using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client_scheduler.Models
{
    internal class AuthAttempt
    {
        public string name {  get; set; }
        public string password { get; set; }

        public AuthAttempt() { }

        public AuthAttempt(string name, string password)
        {
            this.name = name;
            this.password = password;
        }
    }
}
