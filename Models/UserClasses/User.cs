using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LittleBigTraveler.Models.UserClasses
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 8)]
        public string Password { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public virtual List<Customer> Customer { get; set; }
        public virtual List<Partner> Partner { get; set; }
        public virtual List<Administrator> Administrator { get; set; }
    }
}

