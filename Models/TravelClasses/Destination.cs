using System;
using System.Collections.Generic;

namespace LittleBigTraveler.Models.TravelClasses
{
    public class Destination
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }

        public string Style { get; set; }
        public List<string> Images { get; set; } // Liste d'images
        public string ExternalLinks { get; set; }


        public virtual List<Service> Services { get; set; } // Propriété de navigation
        public virtual List<Travel> Travels { get; set; }
    }

}

