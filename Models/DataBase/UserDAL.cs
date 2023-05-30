using System;
using System.Collections.Generic;
using System.Linq;
using LittleBigTraveler.Models.DataBase;
using LittleBigTraveler.Models.UserClasses;
using Microsoft.EntityFrameworkCore;

namespace LittleBigTraveler.Models.DataBase
{

    public class UserDAL : IUserDAL
    {
        private BddContext _bddContext;

        public UserDAL()
        {
            _bddContext = new BddContext();
        }

        // Supression/Création de la database (méthode appelé dans BddContext)
        public void DeleteCreateDatabase()
        {
            _bddContext.Database.EnsureDeleted();
            _bddContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }
        // Méthode de création d'un administrateur
        public int CreateAdministrator(string lastName, string firstName, string email, string password, string address, string phoneNumber, DateTime birthDate)
        {
            User user = new User()
            {
                LastName = lastName,
                FirstName = firstName,
                Email = email,
                Password = password,
                Address = address,
                PhoneNumber = phoneNumber,
                BirthDate = birthDate
            };

            Administrator administrator = new Administrator()
            {
                User = user
            };

            _bddContext.Administrators.Add(administrator);
            _bddContext.SaveChanges();

            return administrator.Id;
        }

        // Méthode de création d'un partenaire
        public int CreatePartner(string lastName, string firstName, string email, string password, string address, string phoneNumber, DateTime birthDate, string roleName, string roleType)
        {
            User user = new User()
            {
                LastName = lastName,
                FirstName = firstName,
                Email = email,
                Password = password,
                Address = address,
                PhoneNumber = phoneNumber,
                BirthDate = birthDate
            };

            Role role = new Role()
            {
                Name = roleName,
                Type = roleType
            };

            Partner partner = new Partner()
            {
                User = user,
                Role = role
            };

            _bddContext.Partners.Add(partner);
            _bddContext.SaveChanges();

            return partner.Id;
        }

        // Méthode de création d'un client
        public int CreateCustomer(string lastName, string firstName, string email, string password, string address, string phoneNumber, DateTime birthDate, int loyaltyPoint, int commentPoint)
        {
            User user = new User()
            {
                LastName = lastName,
                FirstName = firstName,
                Email = email,
                Password = password,
                Address = address,
                PhoneNumber = phoneNumber,
                BirthDate = birthDate
            };

            Customer customer = new Customer()
            {
                User = user,
                LoyaltyPoint = loyaltyPoint,
                CommentPoint = commentPoint
            };

            _bddContext.Customers.Add(customer);
            _bddContext.SaveChanges();

            return customer.Id;
        }



        // Suppression des données "User"
        public void DeleteUser(int id)
        {
            var user = _bddContext.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _bddContext.Users.Remove(user);
                _bddContext.SaveChanges();
            }
        }



        // Modification des données "User"
        public void ModifyUser(int id, string lastName, string firstName, string email, string password, string address, string phoneNumber, DateTime birthDate, string profilePicture)
        {
            var user = _bddContext.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.LastName = lastName;
                user.FirstName = firstName;
                user.Email = email;
                user.Password = password;
                user.Address = address;
                user.PhoneNumber = phoneNumber;
                user.BirthDate = birthDate;
                user.ProfilePicture = profilePicture;

                _bddContext.SaveChanges();
            }
        }

        // Recherche dans les données "User" d'après le nom, prénom ou email
        public List<User> SearchUser(string query)
        {
            IQueryable<User> recherche = _bddContext.Users;

            if (!string.IsNullOrEmpty(query))
            {
                recherche = recherche.Where(u => u.LastName.Contains(query) || u.FirstName.Contains(query) || u.Email.Contains(query));
            }

            return recherche.ToList();
        }

        // Récupération de tous les utilisateurs avec le type
        public List<User> GetAllUsersWithType()
        {
            List<User> users = _bddContext.Users
                .Include(u => u.Customer)
                .Include(u => u.Partner)
                .Include(u => u.Administrator)
                .ToList();

            foreach (var user in users)
            {
                string userType = user.GetUserType(); // Utilisez la méthode GetUserType()
                                                      // Faites ce que vous devez faire avec le userType, par exemple, l'afficher ou le stocker dans une autre liste
            }

            return users;
        }


        // Récupération des données "User" par ID avec le type
        public User GetAllUsersWithTypeWithId(int id)
        {
            User user = _bddContext.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                string userType = user.GetUserType(); // Utilisez la méthode GetUserType()
                                                      // Faites ce que vous devez faire avec le userType, par exemple, l'afficher ou le stocker dans une variable
            }

            return user;
        }
    }
}