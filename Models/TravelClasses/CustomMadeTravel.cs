using System;
using LittleBigTraveler.Models.UserClasses;

namespace LittleBigTraveler.Models.TravelClasses
{
	public class CustomMadeTravel
	{
        public int Id { get; set; }
        public string Name { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int TravelId { get; set; }
        public virtual Travel Travel { get; set; }

        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }
    }
}

