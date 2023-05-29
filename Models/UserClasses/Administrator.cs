using System;
using System.ComponentModel.DataAnnotations;

namespace LittleBigTraveler.Models.UserClasses
{
	public class Administrator
	{
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        //public bool Admin { get; set; }
    }
}

