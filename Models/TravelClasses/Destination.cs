using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LittleBigTraveler.Models.TravelClasses
{
	public class Destination
	{
        [Key]
        public int Id { get; set; }
        
        public string City { get; set; }
        
        public string Country { get; set; }
        public string Description { get; set; }
        
        public string Style { get; set; }
        public List<string> Images { get; set; }
        public string ExternalLinks { get; set; }

        public virtual List<Travel> Travels { get; set; }
    }
}

