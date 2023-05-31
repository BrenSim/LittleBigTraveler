using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using LittleBigTraveler.Models.TravelClasses;
using LittleBigTraveler.Models.UserClasses;
using Microsoft.AspNetCore.Http;

namespace LittleBigTraveler.ViewModels
{
    public class TravelViewModel
    {
        public int Id { get; set; }
        public int DestinationId { get; set; }
        public Customer Customer { get; set; }
        public Destination Destination { get; set; }
        public string DepartureLocation { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public double Price { get; set; }
        public int NumParticipants { get; set; }

        public TravelViewModel()
        {
            // Ajoutez des valeurs par défaut si nécessaire
        }

        public TravelViewModel(IHttpContextAccessor httpContextAccessor)
        {
            // Initialisez les propriétés si nécessaire en utilisant les informations de l'HttpContextAccessor
        }
    }

}
