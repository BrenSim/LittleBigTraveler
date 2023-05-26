using System;
using LittleBigTraveler.Models.TravelClasses;

namespace LittleBigTraveler.Models.UserClasses
{
	public class Bookmark
	{
        public int Id { get; set; }

        public int DestinationId { get; set; }
        public virtual Destination Destination { get; set; }

        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }

        public int PartnerId { get; set; }
        public virtual Partner Partner { get; set; }
    }
}

