using System.ComponentModel.DataAnnotations;

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
        public bool IsEmployee { get; set; }

        public Person()
        {
        }
    }  
}
