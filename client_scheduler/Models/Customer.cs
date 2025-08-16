using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client_scheduler.Models
{
    internal class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public int AddressId { get; set; }
        public string Address {  get; set; }
        public string Address2 { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public int CityId { get; set; }
        public string City { get; set; }
        public int CountryId { get; set; }
        public string Country { get; set; }

    }
}
