using System;
using System.Collections.Generic;
using LittleBigTraveler.Models.TravelClasses;
using LittleBigTraveler.Models.UserClasses;

namespace LittleBigTraveler.Models.DataBase
{
    public interface IUserDAL : IDisposable
    {
        // Méthodes relatives aux utilisateurs
        int CreateAdministrator(string lastName, string firstName, string email, string password, string address, string phoneNumber, DateTime birthDate);
        int CreatePartner(string lastName, string firstName, string email, string password, string address, string phoneNumber, DateTime birthDate, string roleName, string roleType);
        int CreateCustomer(string lastName, string firstName, string email, string password, string address, string phoneNumber, DateTime birthDate, int loyaltyPoint, int commentPoint);
        void DeleteUser(int id);
        void ModifyUser(int id, string lastName, string firstName, string email, string password, string address, string phoneNumber, DateTime birthDate, string profilePicture);
        List<User> SearchUser(string searchText);
        List<User> GetAllUsersWithType();
        User GetAllUsersWithTypeWithId(int id);
    }
}
