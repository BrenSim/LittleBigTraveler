using System;
using System.Collections.Generic;
using System.Linq;
using LittleBigTraveler.Models.DataBase;
using LittleBigTraveler.Models.UserClasses;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.Security.Cryptography;
using System.Text;

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

        // Encodage
        public static string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "UnUser" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }

        //Authentification
        public User Authentification (string email, string password)
        {
            string passwordLoggIn = EncodeMD5(password);
            User user = _bddContext.Users.FirstOrDefault(u => u.Email == email && u.Password == passwordLoggIn);
            return user;
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
            string passwordEncode = EncodeMD5(password);
            User user = new User()
            {
                LastName = lastName,
                FirstName = firstName,
                Email = email,
                Password = passwordEncode,
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

        public void ModifyCustomer(int id, string lastName, string firstName, string email, string password, string address, string phoneNumber, DateTime birthDate, int loyaltyPoint, int commentPoint)
        {
            var customer = _bddContext.Customers.FirstOrDefault(c => c.Id == id);
            if (customer != null)
            {
                var user = customer.User;
                ModifyUser(user.Id, lastName, firstName, email, password, address, phoneNumber, birthDate, user.ProfilePicture);
                customer.LoyaltyPoint = loyaltyPoint;
                customer.CommentPoint = commentPoint;
                _bddContext.SaveChanges();
            }
        }

        public void ModifyPartner(int id, string lastName, string firstName, string email, string password, string address, string phoneNumber, DateTime birthDate, string roleName, string roleType)
        {
            var partner = _bddContext.Partners.FirstOrDefault(p => p.Id == id);
            if (partner != null)
            {
                var user = partner.User;
                ModifyUser(user.Id, lastName, firstName, email, password, address, phoneNumber, birthDate, user.ProfilePicture);
                partner.Role.Name = roleName;
                partner.Role.Type = roleType;
                _bddContext.SaveChanges();
            }
        }

        public void ModifyAdministrator(int id, string lastName, string firstName, string email, string password, string address, string phoneNumber, DateTime birthDate)
        {
            var administrator = _bddContext.Administrators.FirstOrDefault(a => a.Id == id);
            if (administrator != null)
            {
                var user = administrator.User;
                ModifyUser(user.Id, lastName, firstName, email, password, address, phoneNumber, birthDate, user.ProfilePicture);
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
                string userType = user.GetUserType();                                               
            }

            return users;
        }


        // Récupération des données "User" par ID avec le type
        public User GetAllUsersWithTypeWithId(int id)
        {
            User user = _bddContext.Users
                .Include(u => u.Customer)
                .Include(u => u.Partner)
                .Include(u => u.Administrator)
                .FirstOrDefault(u => u.Id == id);

            if (user != null)
            {
                string userType = user.GetUserType();
            }

            return user;
        }

        public User GetUserById (int id)
        {
            return _bddContext.Users.Find(id);
        }

        public User GetUserById(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                return this.GetUserById(id);
            }
            return null;
        }
    }
}