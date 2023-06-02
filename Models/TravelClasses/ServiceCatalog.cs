using System;
using LittleBigTraveler.Models.UserClasses;

namespace LittleBigTraveler.Models.TravelClasses
{
	public class ServiceCatalog
	{
        public int Id { get; set; }

        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }

        public int PartnerId { get; set; }
        public virtual Partner Partner { get; set; }
    }
}
