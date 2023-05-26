using System;
using LittleBigTraveler.Models.TravelClasses;

namespace LittleBigTraveler.Models.UserClasses
{
	public class Evaluation
	{
        public int Id { get; set; }
        public int Note { get; set; }
        public string Comment { get; set; }

        public int BookingId { get; set; }
        public virtual Booking Booking { get; set; }
    }
}

