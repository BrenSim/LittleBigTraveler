using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LittleBigTraveler.Models.TravelClasses;

namespace LittleBigTraveler.ViewModels
{
    public class ServiceViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime Schedule { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public int MaxCapacity { get; set; }
        public List<string> Images { get; set; }
        public string ExternalLinks { get; set; }

        public List<Service> Services { get; set; }
    }
}
