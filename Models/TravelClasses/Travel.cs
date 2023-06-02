using System;
using System.Collections.Generic;
using LittleBigTraveler.Models.UserClasses;

namespace LittleBigTraveler.Models.TravelClasses
{
	public class Travel
	{
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int DestinationId { get; set; }
        public virtual Destination Destination { get; set; }

        public string DepartureLocation { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        
        public double Price { get; set; }
        public int NumParticipants { get; set; }

        public virtual List<Booking> Bookings { get; set; }

        public virtual List<AllInclusiveTravel> AllInclusiveTravels { get; set; }


        //public virtual List<TravelPackage> TravelPackages { get; set; }
    }
}

