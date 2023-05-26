using System;
using LittleBigTraveler.Models.TravelClasses;

namespace LittleBigTraveler.Models.UserClasses
{
	public class Payment
	{
        public int Id { get; set; }
        public string PaymentMethod { get; set; }
        public double TotalAmount { get; set; }
        public DateTime PaymentDate { get; set; }

        public int BookingId { get; set; }
        public virtual Booking Booking { get; set; }
    }
}

