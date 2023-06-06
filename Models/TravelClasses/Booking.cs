using System;
using LittleBigTraveler.Models.UserClasses;
using System.Collections.Generic;

namespace LittleBigTraveler.Models.TravelClasses
{
	public class Booking
	{
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        //public int CustomerId { get; set; }
        //public virtual Customer Customer { get; set; }

        public int PackageId { get; set; }
        public Package Package { get; set; }

        //public int TravelId { get; set; }
        //public virtual Travel Travel { get; set; }

        //public int ServiceId { get; set; }
        //public virtual Service Service { get; set; }


        public virtual List<Payment> Payments { get; set; }
        public virtual List<Evaluation> Evaluations { get; set; }
      
    }
}

