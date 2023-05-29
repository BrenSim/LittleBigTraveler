using System;
using System.ComponentModel.DataAnnotations;
using LittleBigTraveler.Models.UserClasses;

namespace LittleBigTraveler.Models.TravelClasses
{
	public class AllInclusiveTravel
	{
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int TravelId { get; set; }
        public virtual Travel Travel { get; set; }

        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }

        //public int TravelPackageId { get; set; }
        //public virtual TravelPackage TravelPackage { get; set; }
    }
}

