using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LittleBigTraveler.ViewModels
{
	public class DestinationViewModel
	{
        public int Id { get; set; }

        [Required(ErrorMessage = "Le pays est obligatoire.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "La ville est obligatoire.")]
        public string City { get; set; }

        public string Description { get; set; }
        public string Style { get; set; }
        public List<string> Images { get; set; }
        public string ExternalLinks { get; set; }
    }
}

