using System;
using System.Collections.Generic;
using System.Security.Claims;
using LittleBigTraveler.Models.UserClasses;

namespace LittleBigTraveler.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string ProfilePicture { get; set; }

        public int CustomerId { get; set; }
        public int LoyaltyPoint { get; set; }
        public int CommentPoint { get; set; }

        public int AdministratorId { get; set; }

        public int PartnerId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleType { get; set; }

        public string UserType { get; set; }

        public List<UserViewModel> Users { get; set; }

        public bool LoggedIn { get; set; }

        //public ClaimsPrincipal UserPrincipal { get; set; }

        //public string ReturnUrl { get; set; }
    }
}
