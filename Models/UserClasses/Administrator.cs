using System;
namespace LittleBigTraveler.Models.UserClasses
{
	public class Administrator
	{
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        //public bool Admin { get; set; }
    }
}

