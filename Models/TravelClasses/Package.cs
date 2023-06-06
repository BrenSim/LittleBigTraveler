using System;
using LittleBigTraveler.Models.UserClasses;
using System.Collections.Generic;

namespace LittleBigTraveler.Models.TravelClasses
{
	public class Package
	{

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int QuantityAvailable { get; set; }

        public int TravelId { get; set; }
        public Travel Travel { get; set; }

        public List<Service> ServiceForPackage { get; set; }
        //public virtual Service Service { get; set; }
    }
}

