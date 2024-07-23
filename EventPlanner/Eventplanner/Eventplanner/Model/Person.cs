using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventplanner.Model
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        public string TelephoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public Address Address { get; set; }
        public bool IsEmploee { get; set; } 
    }  
}
