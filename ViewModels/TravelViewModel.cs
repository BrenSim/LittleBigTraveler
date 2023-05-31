using System;
using System.ComponentModel.DataAnnotations;
using LittleBigTraveler.Models.TravelClasses;
using LittleBigTraveler.Models.UserClasses;

namespace LittleBigTraveler.ViewModels
{
    public class TravelViewModel
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }


        public int DestinationId { get; set; }
        public virtual Destination Destination { get; set; }


        public string DepartureLocation { get; set; }


        [DataType(DataType.Date)]
        public DateTime DepartureDate { get; set; }


        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }


        public double Price { get; set; }


        public int NumParticipants { get; set; }
    }
}
