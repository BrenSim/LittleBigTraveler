using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LittleBigTraveler.Models.UserClasses
{
	public class Role
	{
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }

        public virtual List<Partner> Partners { get; set; }
    }
}

