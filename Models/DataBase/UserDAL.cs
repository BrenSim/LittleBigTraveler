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
    /// <summary>
    /// Fournit des méthodes d'accès aux données pour l'entité User.
    /// </summary>
    public class UserDAL : IUserDAL
    {
        private BddContext _bddContext;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="UserDAL"/>.
        /// </summary>
        public UserDAL()
        {
            _bddContext = new BddContext();
        }

        /// <summary>
        /// Libère les ressources utilisées par l'instance <see cref="UserDAL"/>.
        /// </summary>
        public void Dispose()
        {
            _bddContext.Dispose();
        }

        /// <summary>
        /// Supprime et recrée la base de données.
        /// </summary>
        public void DeleteCreateDatabase()
        {
            _bddContext.Database.EnsureDeleted();
            _bddContext.Database.EnsureCreated();
        }

        /// <summary>
        /// Encodage MD5 d'une chaîne.
        /// </summary>
        /// <param name="motDePasse">Le mot de passe à encoder.</param>
        /// <returns>Le mot de passe encodé en MD5.</returns>
        public static string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "UnUser" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }

        /// <summary>
        /// Authentifie un utilisateur.
        /// </summary>
        /// <param name="email">L'email de l'utilisateur.</param>
        /// <param name="password">Le mot de passe de l'utilisateur.</param>
        /// <returns>L'utilisateur authentifié, ou null s'il n'est pas trouvé.</returns>
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

        /// <summary>
        /// Crée un nouvel administrateur.
        /// </summary>
        /// <param name="lastName">Le nom de famille de l'administrateur.</param>
        /// <param name="firstName">Le prénom de l'administrateur.</param>
        /// <param name="email">L'email de l'administrateur.</param>
        /// <param name="password">Le mot de passe de l'administrateur.</param>
        /// <param name="address">L'adresse de l'administrateur.</param>
        /// <param name="phoneNumber">Le numéro de téléphone de l'administrateur.</param>
        /// <param name="birthDate">La date de naissance de l'administrateur.</param>
        /// <returns>L'ID de l'administrateur créé.</returns>
        public int CreateAdministrator(string lastName, string firstName, string email, string password, string address, string phoneNumber, DateTime birthDate)
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

            Administrator administrator = new Administrator()
            {
                User = user
            };

            _bddContext.Administrators.Add(administrator);
            _bddContext.SaveChanges();

            return administrator.Id;
        }

        /// <summary>
        /// Crée un nouveau partenaire.
        /// </summary>
        /// <param name="lastName">Le nom de famille du partenaire.</param>
        /// <param name="firstName">Le prénom du partenaire.</param>
        /// <param name="email">L'email du partenaire.</param>
        /// <param name="password">Le mot de passe du partenaire.</param>
        /// <param name="address">L'adresse du partenaire.</param>
        /// <param name="phoneNumber">Le numéro de téléphone du partenaire.</param>
        /// <param name="birthDate">La date de naissance du partenaire.</param>
        /// <param name="roleName">Le nom du rôle du partenaire.</param>
        /// <param name="roleType">Le type du rôle du partenaire.</param>
        /// <returns>L'ID du partenaire créé.</returns>
        public int CreatePartner(string lastName, string firstName, string email, string password, string address, string phoneNumber, DateTime birthDate, string roleName, string roleType)
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

        /// <summary>
        /// Crée un nouveau client.
        /// </summary>
        /// <param name="lastName">Le nom de famille du client.</param>
        /// <param name="firstName">Le prénom du client.</param>
        /// <param name="email">L'email du client.</param>
        /// <param name="password">Le mot de passe du client.</param>
        /// <param name="address">L'adresse du client.</param>
        /// <param name="phoneNumber">Le numéro de téléphone du client.</param>
        /// <param name="birthDate">La date de naissance du client.</param>
        /// <param name="loyaltyPoint">Le nombre de points de fidélité du client.</param>
        /// <param name="commentPoint">Le nombre de points de commentaire du client.</param>
        /// <returns>L'ID du client créé.</returns>
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

        /// <summary>
        /// Supprime un utilisateur par son ID.
        /// </summary>
        /// <param name="id">L'ID de l'utilisateur à supprimer.</param>
        public void DeleteUser(int id)
        {
            var user = _bddContext.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _bddContext.Users.Remove(user);
                _bddContext.SaveChanges();
            }
        }

        /// <summary>
        /// Modifie les données d'un utilisateur.
        /// </summary>
        /// <param name="id">L'ID de l'utilisateur à modifier.</param>
        /// <param name="lastName">Le nouveau nom de famille.</param>
        /// <param name="firstName">Le nouveau prénom.</param>
        /// <param name="email">Le nouvel email.</param>
        /// <param name="password">Le nouveau mot de passe.</param>
        /// <param name="address">La nouvelle adresse.</param>
        /// <param name="phoneNumber">Le nouveau numéro de téléphone.</param>
        /// <param name="birthDate">La nouvelle date de naissance.</param>
        /// <param name="profilePicture">La nouvelle photo de profil.</param>
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
            }
        }

        /// <summary>
        /// Modifie les données d'un client.
        /// </summary>
        /// <param name="id">L'ID du client à modifier.</param>
        /// <param name="lastName">Le nouveau nom de famille.</param>
        /// <param name="firstName">Le nouveau prénom.</param>
        /// <param name="email">Le nouvel email.</param>
        /// <param name="password">Le nouveau mot de passe.</param>
        /// <param name="address">La nouvelle adresse.</param>
        /// <param name="phoneNumber">Le nouveau numéro de téléphone.</param>
        /// <param name="birthDate">La nouvelle date de naissance.</param>
        /// <param name="loyaltyPoint">Le nouveau nombre de points de fidélité.</param>
        /// <param name="commentPoint">Le nouveau nombre de points de commentaire.</param>
        public void ModifyCustomer(int id, string lastName, string firstName, string email, string password, string address, string phoneNumber, DateTime birthDate, int loyaltyPoint, int commentPoint)
        {
            var customer = _bddContext.Customers
                .Include(c => c.User)
                .FirstOrDefault(c => c.User.Id == id);

            if (customer != null)
            {
                var user = customer.User;
                ModifyUser(user.Id, lastName, firstName, email, password, address, phoneNumber, birthDate, user.ProfilePicture);
                customer.LoyaltyPoint = loyaltyPoint;
                customer.CommentPoint = commentPoint;
            }
        }

        /// <summary>
        /// Modifie les données d'un partenaire.
        /// </summary>
        /// <param name="id">L'ID du partenaire à modifier.</param>
        /// <param name="lastName">Le nouveau nom de famille.</param>
        /// <param name="firstName">Le nouveau prénom.</param>
        /// <param name="email">Le nouvel email.</param>
        /// <param name="password">Le nouveau mot de passe.</param>
        /// <param name="address">La nouvelle adresse.</param>
        /// <param name="phoneNumber">Le nouveau numéro de téléphone.</param>
        /// <param name="birthDate">La nouvelle date de naissance.</param>
        /// <param name="roleName">Le nouveau nom du rôle.</param>
        /// <param name="roleType">Le nouveau type du rôle.</param>
        public void ModifyPartner(int id, string lastName, string firstName, string email, string password, string address, string phoneNumber, DateTime birthDate, string roleName, string roleType)
        {
            var partner = _bddContext.Partners
                .Include(p => p.User)
                .FirstOrDefault(p => p.User.Id == id);

            if (partner != null)
            {
                var user = partner.User;
                ModifyUser(user.Id, lastName, firstName, email, password, address, phoneNumber, birthDate, user.ProfilePicture);
                partner.Role.Name = roleName;
                partner.Role.Type = roleType;
            }
        }

        /// <summary>
        /// Modifie les données d'un administrateur.
        /// </summary>
        /// <param name="id">L'ID de l'administrateur à modifier.</param>
        /// <param name="lastName">Le nouveau nom de famille.</param>
        /// <param name="firstName">Le nouveau prénom.</param>
        /// <param name="email">Le nouvel email.</param>
        /// <param name="password">Le nouveau mot de passe.</param>
        /// <param name="address">La nouvelle adresse.</param>
        /// <param name="phoneNumber">Le nouveau numéro de téléphone.</param>
        /// <param name="birthDate">La nouvelle date de naissance.</param>
        public void ModifyAdministrator(int id, string lastName, string firstName, string email, string password, string address, string phoneNumber, DateTime birthDate)
        {
            var administrator = _bddContext.Administrators
                .Include(a => a.User)
                .FirstOrDefault(a => a.User.Id == id);

            if (administrator != null)
            {
                var user = administrator.User;
                ModifyUser(user.Id, lastName, firstName, email, password, address, phoneNumber, birthDate, user.ProfilePicture);
            }
        }

        /// <summary>
        /// Recherche les utilisateurs en fonction d'une requête.
        /// </summary>
        /// <param name="query">La requête de recherche.</param>
        /// <returns>La liste des utilisateurs correspondant à la requête.</returns>
        public List<User> SearchUser(string query)
        {
            IQueryable<User> recherche = _bddContext.Users;

            if (!string.IsNullOrEmpty(query))
            {
                recherche = recherche.Where(u => u.LastName.Contains(query) || u.FirstName.Contains(query) || u.Email.Contains(query));
            }

            return recherche.ToList();
        }

        /// <summary>
        /// Récupère tous les utilisateurs avec leur type.
        /// </summary>
        /// <returns>La liste des utilisateurs avec leur type.</returns>
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
                // Faites quelque chose avec le type d'utilisateur...
            }

            return users;
        }

        /// <summary>
        /// Récupère un utilisateur par son ID avec son type.
        /// </summary>
        /// <param name="id">L'ID de l'utilisateur.</param>
        /// <returns>L'utilisateur avec son type, ou null s'il n'est pas trouvé.</returns>
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

        /// <summary>
        /// Récupère un utilisateur par son ID (surchargé pour accepter une chaîne).
        /// </summary>
        /// <param name="idStr">L'ID de l'utilisateur sous forme de chaîne.</param>
        /// <returns>L'utilisateur correspondant à l'ID, ou null s'il n'est pas trouvé.</returns>
        public User GetAllUsersWithTypeWithId(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                return this.GetAllUsersWithTypeWithId(id);
            }
            return null;
        }

        /// <summary>
        /// Récupère un utilisateur par son ID.
        /// </summary>
        /// <param name="id">L'ID de l'utilisateur.</param>
        /// <returns>L'utilisateur correspondant à l'ID, ou null s'il n'est pas trouvé.</returns>
        public User GetUserById(int id)
        {
            return _bddContext.Users.Find(id);
        }

        /// <summary>
        /// Récupère un utilisateur par son ID (surchargé pour accepter une chaîne).
        /// </summary>
        /// <param name="idStr">L'ID de l'utilisateur sous forme de chaîne.</param>
        /// <returns>L'utilisateur correspondant à l'ID, ou null s'il n'est pas trouvé.</returns>
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
