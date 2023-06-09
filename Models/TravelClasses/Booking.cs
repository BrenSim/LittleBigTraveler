using System;
using LittleBigTraveler.Models.UserClasses;
using System.Collections.Generic;

namespace LittleBigTraveler.Models.TravelClasses
{
    public class Booking
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int PackageId { get; set; }
        public Package Package { get; set; }
        public bool IsConfirmed { get; set; } // Statut de confirmation de la réservation

        public virtual List<Payment> Payments { get; set; }
        public virtual List<Evaluation> Evaluations { get; set; }
    }
}
