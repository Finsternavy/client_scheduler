using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client_scheduler.Models
{
    internal class Response
    {
        public DataTable data {  get; set; }
        public bool success { get; set; }
        public string message { get; set; }


        public Response() { }
        public Response(bool success, string message, DataTable data) { 
            this.success = success;
            this.message = message;
            this.data = data;
        }

        public string ToString()
        {
            return "Success: " + this.success + " | Message: " + this.message + " | Data: " + this.data.ToString();
        }


    }
}
