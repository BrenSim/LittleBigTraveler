using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LittleBigTraveler.Models.TravelClasses
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Schedule { get; set; }

        [StringLength(100)]
        public string Location { get; set; }

        [Required]
        public string Type { get; set; }

        [Range(0, int.MaxValue)]
        public int MaxCapacity { get; set; }

        public virtual List<ServiceCatalog> ServiceCatalogs { get; set; }
        //public virtual List<Booking> Bookings { get; set; }
    }
}

