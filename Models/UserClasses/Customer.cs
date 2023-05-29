using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LittleBigTraveler.Models.TravelClasses;

namespace LittleBigTraveler.Models.UserClasses
{
	public class Customer
	{
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int LoyaltyPoint { get; set; }
        public int CommentPoint { get; set; }

        public int BookingId { get; set; }
        public virtual Booking Booking { get; set; }


        public virtual List<Evaluation> Evaluations { get; set; }
    }
}

