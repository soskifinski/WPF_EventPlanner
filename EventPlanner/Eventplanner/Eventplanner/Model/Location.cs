using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventplanner.Model
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? AddressId { get; set; }
        public Address Address { get; set; }
    }
}
