using System;
using LittleBigTraveler.Models.TravelClasses;

namespace LittleBigTraveler.Models.UserClasses
{
    public class Payment
    {
        public int Id { get; set; }
        public double TotalAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public int NumCB { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int BookingId { get; set; }
        public virtual Booking Booking { get; set; }
    }
}
