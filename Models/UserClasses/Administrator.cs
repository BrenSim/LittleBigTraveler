using System;
using LittleBigTraveler.Models.TravelClasses;
using System.Collections.Generic;

namespace LittleBigTraveler.Models.UserClasses
{
	public class Administrator
	{
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int BookingId { get; set; }
        public virtual List<Booking> Bookings { get; set; }

    }
}

