using System.Collections.Generic;
using LittleBigTraveler.Models.TravelClasses;
using LittleBigTraveler.Models.UserClasses;
using Microsoft.AspNetCore.Http;

namespace LittleBigTraveler.ViewModels
{
    public class PackageViewModel
    {
        public int Id { get; set; }
        public int TravelId { get; set; }
        public Travel Travel { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int QuantityAvailable { get; set; }
        public List<Service> Services { get; set; }
        public List<int> SelectedServiceId { get; set; }
        public List<Service> AvailableServices { get; set; }
        public List<Package> Packages { get; set; }
        public Package Package { get; set; }


        public PackageViewModel()
        {
            Services = new List<Service>();
            AvailableServices = new List<Service>();
        }

        public PackageViewModel(IHttpContextAccessor httpContextAccessor)
        {
            Services = new List<Service>();
            AvailableServices = new List<Service>();
        }
    }
}
