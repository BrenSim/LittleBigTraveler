﻿using System;
using System.Collections.Generic;
using LittleBigTraveler.Models.UserClasses;

namespace LittleBigTraveler.Models.TravelClasses
{
	public class CustomMadeTravel
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int TravelId { get; set; }
        public Travel Travel { get; set; }


        public List<Service> ServiceForPackage { get; set; }
        //public virtual Service Service { get; set; }
    }
}

