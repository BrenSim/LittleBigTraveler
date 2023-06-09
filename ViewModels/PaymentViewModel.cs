using System;
using LittleBigTraveler.Models.TravelClasses;
using LittleBigTraveler.Models.UserClasses;

namespace LittleBigTraveler.ViewModels
    
{
    public class PaymentViewModel
    {
        public int BookingId { get; set; }
        public double TotalAmount { get; set; }
        public int NumCB { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PackageName { get; set; }
        public string Description { get; set; }

        public Booking Booking { get; set; }


        public Package Package { get; set; }
        public User User { get; set; }

    }
}


