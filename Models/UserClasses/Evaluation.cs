using System;
using System.ComponentModel.DataAnnotations;
using LittleBigTraveler.Models.TravelClasses;

namespace LittleBigTraveler.Models.UserClasses
{
	public class Evaluation
	{
        public int Id { get; set; }

        [Range(1, 5)]
        public int Note { get; set; }

        [StringLength(500)]
        public string Comment { get; set; }

        [Required]
        public int BookingId { get; set; }
        public virtual Booking Booking { get; set; }
    }
}

