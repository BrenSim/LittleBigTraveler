using System;
using System.ComponentModel.DataAnnotations;
using LittleBigTraveler.Models.TravelClasses;

namespace LittleBigTraveler.Models.UserClasses
{
    public class Payment
    {
        public int Id { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        [Range(0, double.MaxValue)]
        public double TotalAmount { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime PaymentDate { get; set; }

        [Required]
        public int BookingId { get; set; }
        public virtual Booking Booking { get; set; }
    }
}

