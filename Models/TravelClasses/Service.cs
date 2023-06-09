﻿using System;
using System.Collections.Generic;

namespace LittleBigTraveler.Models.TravelClasses
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime Schedule { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public string Style { get; set; }
        public int MaxCapacity { get; set; }
        public List<string> Images { get; set; } // Liste d'images
        public string ExternalLinks { get; set; }

        public int DestinationId { get; set; }
        public virtual Destination Destination { get; set; }

        //public List<Destination> Destinations { get; set; }

        public int? PackageId { get; set; }
        public Package Package { get; set; }
    }
}
