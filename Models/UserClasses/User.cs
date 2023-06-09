﻿using System;
using System.Collections.Generic;
using System.Security.Principal;
using LittleBigTraveler.Models.TravelClasses;

namespace LittleBigTraveler.Models.UserClasses
{
    public class User
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string ProfilePicture { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Partner Partner { get; set; }
        public virtual Administrator Administrator { get; set; }
        public virtual Role Role { get; set; }
        public virtual List<Booking> Bookings { get; set; }
        public virtual UserPreference UserPreference { get; set; }



        public string GetUserType()
        {
            if (Customer != null)
            {
                return "Customer";
            }
            else if (Partner != null)
            {
                return "Partner";
            }
            else if (Administrator != null)
            {
                return "Administrator";
            }

            return "Role non défini";
        }
    }
}
