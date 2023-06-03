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

        // Suppression/Création de la base de données (méthode appelée dans BddContext)
        public void DeleteCreateDatabase()
        {
            _bddContext.Database.EnsureDeleted();
            _bddContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

        // Encodage MD5 d'un mot de passe
        public static string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "UnUser" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }

        // Authentification d'un utilisateur
        public User Authentification(string email, string password)
        {
            string passwordLoggIn = EncodeMD5(password);
            User user = _bddContext.Users
                .Include(u => u.Customer)
                .Include(u => u.Partner)
                .Include(u => u.Administrator)
                .FirstOrDefault(u => u.Email == email && u.Password == passwordLoggIn);
            return user;
        }

        // Méthode de création d'un administrateur
        public int CreateAdministrator(string lastName, string firstName, string email, string password, string address, string phoneNumber, DateTime birthDate)
        {
            // Encodage du mot de passe en utilisant MDD (pour stockage sécurisé)
            string passwordEncode = EncodeMD5(password);
            // Création d'un nouvel utilisateur avec les données fournies
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

            // Création d'un nouvel administrateur lié à l'utilisateur
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
            // Encodage du mot de passe en utilisant MDD (pour stockage sécurisé)
            string passwordEncode = EncodeMD5(password);
            // Création d'un nouvel utilisateur avec les données fournies
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

            // Création d'un nouveau rôle
            Role role = new Role()
            {
                Name = roleName,
                Type = roleType
            };

            // Création d'un nouveau partenaire lié à l'utilisateur et au rôle
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
            // Encodage du mot de passe en utilisant MDD (pour stockage sécurisé)
            string passwordEncode = EncodeMD5(password);
            // Création d'un nouvel utilisateur avec les données fournies
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
            // Création d'un nouveau client lié à l'utilisateur
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

        // Suppression des données d'un utilisateur
        public void DeleteUser(int id)
        {
            var user = _bddContext.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _bddContext.Users.Remove(user);
                _bddContext.SaveChanges();
            }
        }

        // Modification des données d'un utilisateur
        public void ModifyUser(int id, string lastName, string firstName, string email, string password, string address, string phoneNumber, DateTime birthDate, string profilePicture)
        {
            var user = _bddContext.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                // Modification des propriétés de l'utilisateur avec les nouvelles données fournies
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

        // Modification des données d'un client
        public void ModifyCustomer(int id, string lastName, string firstName, string email, string password, string address, string phoneNumber, DateTime birthDate, int loyaltyPoint, int commentPoint)
        {
            var customer = _bddContext.Customers.FirstOrDefault(c => c.Id == id);
            if (customer != null)
            {
                var user = customer.User;
                // Appel de la méthode ModifyUser pour mettre à jour les données de l'utilisateur
                ModifyUser(user.Id, lastName, firstName, email, password, address, phoneNumber, birthDate, user.ProfilePicture);
                // Modification des propriétés spécifiques du client
                customer.LoyaltyPoint = loyaltyPoint;
                customer.CommentPoint = commentPoint;
                _bddContext.SaveChanges();
            }
        }

        // Modification des données d'un partenaire
        public void ModifyPartner(int id, string lastName, string firstName, string email, string password, string address, string phoneNumber, DateTime birthDate, string roleName, string roleType)
        {
            var partner = _bddContext.Partners.FirstOrDefault(p => p.Id == id);
            if (partner != null)
            {
                var user = partner.User;
                // Appel de la méthode ModifyUser pour mettre à jour les données de l'utilisateur
                ModifyUser(user.Id, lastName, firstName, email, password, address, phoneNumber, birthDate, user.ProfilePicture);
                // Modification des propriétés spécifiques du partenaire
                partner.Role.Name = roleName;
                partner.Role.Type = roleType;
                _bddContext.SaveChanges();
            }
        }

        // Modification des données d'un administrateur
        public void ModifyAdministrator(int id, string lastName, string firstName, string email, string password, string address, string phoneNumber, DateTime birthDate)
        {
            var administrator = _bddContext.Administrators.FirstOrDefault(a => a.Id == id);
            if (administrator != null)
            {
                var user = administrator.User;
                // Appel de la méthode ModifyUser pour mettre à jour les données de l'utilisateur
                ModifyUser(user.Id, lastName, firstName, email, password, address, phoneNumber, birthDate, user.ProfilePicture);
                _bddContext.SaveChanges();
            }
        }
        // Recherche dans les données "User" en fonction du nom, prénom ou email
        public List<User> SearchUser(string query)
        {
            IQueryable<User> recherche = _bddContext.Users;

            if (!string.IsNullOrEmpty(query))
            {
                recherche = recherche.Where(u => u.LastName.Contains(query) || u.FirstName.Contains(query) || u.Email.Contains(query));
            }

            return recherche.ToList();
        }

        // Récupération de tous les utilisateurs avec leur type
        public List<User> GetAllUsersWithType()
        {
            List<User> users = _bddContext.Users
                .Include(u => u.Customer)
                .Include(u => u.Partner)
                .Include(u => u.Administrator)
                .ToList();

            foreach (var user in users)
            {
                // Obtention du type d'utilisateur en appelant la méthode GetUserType()
                string userType = user.GetUserType();
                // Faites quelque chose avec le type d'utilisateur...
            }

            return users;
        }

        // Récupération des données d'un utilisateur par ID avec son type
        public User GetAllUsersWithTypeWithId(int id)
        {
            User user = _bddContext.Users
                .Include(u => u.Customer)
                .Include(u => u.Partner)
                .Include(u => u.Administrator)
                .FirstOrDefault(u => u.Id == id);

            if (user != null)
            {
                // Obtention du type d'utilisateur en appelant la méthode GetUserType()
                string userType = user.GetUserType();
            }

            return user;
        }

        public User GetAllUsersWithTypeWithId(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                return this.GetAllUsersWithTypeWithId(id);
            }
            return null;
        }

        // Récupération d'un utilisateur par ID
        public User GetUserById(int id)
        {
            return _bddContext.Users.Find(id);
        }

        // Récupération d'un utilisateur par ID (version surchargée acceptant une chaîne)
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