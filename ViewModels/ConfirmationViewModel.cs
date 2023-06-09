using System;
using LittleBigTraveler.Models.TravelClasses;
using LittleBigTraveler.Models.UserClasses;
namespace LittleBigTraveler.ViewModels

{
    public class ConfirmationViewModel
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public string PackageName { get; set; }
        public string PackageDescription { get; set; }
        public decimal PackagePrice { get; set; }
    }
}

