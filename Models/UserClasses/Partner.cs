using System;
using System.Collections.Generic;
using System.Data;
using LittleBigTraveler.Models.TravelClasses;

namespace LittleBigTraveler.Models.UserClasses
{
	public class Partner
	{
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int? RoleId { get; set; }
        public virtual Role Role { get; set; }

        //public int BookingId { get; set; }
        //public virtual List<Booking> Bookings { get; set; }

        //public virtual List<ServiceCatalog> ServiceCatalogs { get; set; }
    }
}

