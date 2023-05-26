using System;
using System.Collections.Generic;

namespace LittleBigTraveler.Models.TravelClasses
{
	public class Service
	{
        public int Id { get; set; }
        public double Price { get; set; }
        public DateTime Schedule { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public int MaxCapacity { get; set; }

        public virtual List<ServiceCatalog> ServiceCatalogs { get; set; }
        //public virtual List<Booking> Bookings { get; set; }
    }
}

