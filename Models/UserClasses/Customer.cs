using System;
using System.Collections.Generic;
using LittleBigTraveler.Models.TravelClasses;

namespace LittleBigTraveler.Models.UserClasses
{
	public class Customer
	{
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int LoyaltyPoint { get; set; }
        public int CommentPoint { get; set; }

        public int BookingId { get; set; }
        public virtual Booking Booking { get; set; }


        public virtual List<Evaluation> Evaluations { get; set; }
    }
}

