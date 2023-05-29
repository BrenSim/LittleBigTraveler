using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using LittleBigTraveler.Models.TravelClasses;

namespace LittleBigTraveler.Models.UserClasses
{
	public class Partner
	{
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }


        public virtual List<ServiceCatalog> ServiceCatalogs { get; set; }
    }
}

