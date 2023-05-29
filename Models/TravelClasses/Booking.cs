using System;
using LittleBigTraveler.Models.UserClasses;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LittleBigTraveler.Models.TravelClasses
{
	public class Booking
	{
        [Key]
        public int Id { get; set; }
        [Required]
        public int TravelId { get; set; }
        public virtual Travel Travel { get; set; }
        [Required]
        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }


        public virtual List<Payment> Payments { get; set; }
        public virtual List<Evaluation> Evaluations { get; set; }
      
    }
}

