using System;
using System.Collections.Generic;
using LittleBigTraveler.Models.TravelClasses;

namespace LittleBigTraveler.Models.UserClasses
{
	public class UserPreference
	{
        public int Id { get; set; }
        public int PreferredBudget { get; set; }
        public int TravelFrequency { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        //public int DestinationId { get; set; }
        public virtual List <Destination> Destination { get; set; }

        public virtual List <Service> Services { get; set; }

        //public virtual List<Bookmark> Bookmarks { get; set; }
    }
}

