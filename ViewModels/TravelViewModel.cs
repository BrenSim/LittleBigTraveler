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
        public string DestinationCity { get; set; }
        public Customer Customer { get; set; }
        public Destination Destination { get; set; }
        public Travel Travel { get; set; }
        public string DepartureLocation { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int Price { get; set; }
        public int NumParticipants { get; set; }

        public List<Travel> Travels { get; set; }

        public TravelViewModel()
        {
        }
        public TravelViewModel(IHttpContextAccessor httpContextAccessor)
        {
        }
    }
}


