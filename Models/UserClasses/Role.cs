using System;
using System.Collections.Generic;

namespace LittleBigTraveler.Models.UserClasses
{
	public class Role
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public virtual List<Partner> Partners { get; set; }
    }
}

