﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using LittleBigTraveler.Models.TravelClasses;

namespace LittleBigTraveler.ViewModels
{
	public class DestinationViewModel
	{
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public List<string> Images { get; set; }
        public string ExternalLinks { get; set; }
        public List<Destination> Destinations { get; set; }

        public Package Package { get; set; }
        public Destination Destination { get; set; }

    }
}

