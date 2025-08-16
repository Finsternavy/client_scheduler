using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client_scheduler.Models
{
    internal class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //constructor
        public User() { }

        public User(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
