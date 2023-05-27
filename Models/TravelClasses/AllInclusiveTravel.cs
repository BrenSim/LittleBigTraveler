using System;
using LittleBigTraveler.Models.UserClasses;
using System.Collections.Generic;

namespace LittleBigTraveler.Models.TravelClasses
{
	public class AllInclusiveTravel
	{
        public int Id { get; set; }
        public string Name { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int TravelId { get; set; }
        public virtual Travel Travel { get; set; }

        
        public List<int> ServiceId { get; set; }
        public virtual Service Service { get; set; }

        //public int TravelPackageId { get; set; }
        //public virtual TravelPackage TravelPackage { get; set; }
    }
}

