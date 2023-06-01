using System.Collections.Generic;
using LittleBigTraveler.Models.TravelClasses;
using Microsoft.AspNetCore.Http;

namespace LittleBigTraveler.ViewModels
{
    public class AllInclusiveTravelViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TravelId { get; set; }
        public List<int> SelectedServiceId { get; set; }
        public List<Service> Services { get; set; }

        public AllInclusiveTravelViewModel()
        {
            Services = new List<Service>();
        }

        public AllInclusiveTravelViewModel(IHttpContextAccessor httpContextAccessor)
        {
            // Initialisez les propriétés si nécessaire en utilisant les informations de l'HttpContextAccessor
        }
    }
}


