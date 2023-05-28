using System;
using System.ComponentModel.DataAnnotations;
using LittleBigTraveler.Models.TravelClasses;

namespace LittleBigTraveler.Models.UserClasses
{
	public class UserPreference
	{
        public int Id { get; set; }

        [Range(0, double.MaxValue)]
        public double PreferredBudget { get; set; }

        [Range(1, int.MaxValue)]
        public int TravelFrequency { get; set; }

        [Required]
        public int DestinationId { get; set; }
        public virtual Destination Destination { get; set; }

        [Required]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }

        public int BookmarkId { get; set; }
        public virtual Bookmark Bookmark { get; set; }
    }
}

