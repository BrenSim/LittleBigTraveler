using System;
using LittleBigTraveler.Models.TravelClasses;

namespace LittleBigTraveler.Models.UserClasses
{
	public class UserPreference
	{
        public int Id { get; set; }
        public double PreferredBudget { get; set; }
        public int TravelFrequency { get; set; }

        public int DestinationId { get; set; }
        public virtual Destination Destination { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }

        public int BookmarkId { get; set; }
        public virtual Bookmark Bookmark { get; set; }
    }
}

