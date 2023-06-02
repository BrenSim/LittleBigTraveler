using LittleBigTraveler.Models.TravelClasses;
using System;
using System.Collections.Generic;

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
        public string Style { get; set; }
        public int MaxCapacity { get; set; }
        public List<string> Images { get; set; }
        public string ExternalLinks { get; set; }

        // Ajout de DestinationId pour le référencer
        public int DestinationId { get; set; }

        public List<Service> Services { get; set; }
    }
}
