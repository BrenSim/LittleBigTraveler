using System;
using LittleBigTraveler.Models.TravelClasses;
using LittleBigTraveler.Models.UserClasses;
namespace LittleBigTraveler.ViewModels
{
    public class BookingViewModel
    {
        public int Id { get; set; }
        public string PackageName { get; set; }
        public string PackageDescription { get; set; }
        public decimal PackagePrice { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime BookingDate { get; set; }
        public Travel Travel { get; set; }

        public int PackageId { get; set; }
    }

}

