using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using LittleBigTraveler.Models.UserClasses;

namespace LittleBigTraveler.Models.TravelClasses
{
    public class Travel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "La date de départ est obligatoire.")]
        public DateTime DepartureDate { get; set; }

        [Required(ErrorMessage = "La date de retour est obligatoire.")]
        public DateTime ReturnDate { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Le budget doit être un nombre positif.")]
        public double Budget { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Le nombre de participants doit être un entier positif.")]
        public int NumParticipants { get; set; }

        
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

       
        public int DestinationId { get; set; }
        public virtual Destination Destination { get; set; }

        
        public virtual List<Booking> Bookings { get; set; }

      
    }
}