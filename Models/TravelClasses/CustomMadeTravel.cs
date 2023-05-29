using System;
using System.ComponentModel.DataAnnotations;
using LittleBigTraveler.Models.UserClasses;

namespace LittleBigTraveler.Models.TravelClasses
{
	public class CustomMadeTravel
	{
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        [Required]
        public int TravelId { get; set; }
        public virtual Travel Travel { get; set; }
        [Required]
        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }
    }
}

