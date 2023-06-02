using System;
using System.Collections.Generic;
using LittleBigTraveler.Models.TravelClasses;
using Microsoft.AspNetCore.Http;

public class AllInclusiveTravelViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int TravelId { get; set; }
    public List<int> SelectedServiceId { get; set; }
    public List<Service> Services { get; set; }
    public List<Service> AvailableServices { get; set; }
    public Travel Travel { get; set; }

    public AllInclusiveTravelViewModel()
    {
        Services = new List<Service>();
        AvailableServices = new List<Service>();
    }

    public AllInclusiveTravelViewModel(IHttpContextAccessor httpContextAccessor)
    {
        Services = new List<Service>();
        AvailableServices = new List<Service>();
    }

}
