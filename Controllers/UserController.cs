using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LittleBigTraveler.Models.DataBase;
using LittleBigTraveler.Models.UserClasses;
using LittleBigTraveler.ViewModels;
using System.Security.Claims;
using System.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace LittleBigTraveler.Controllers
{
    /// <summary>
    /// Contrôleur de l'utilisateur.
    /// </summary>
    public class UserController : Controller
    {
        /// <summary>
        /// Affiche la liste des utilisateurs.
        /// </summary>
        /// <returns>Vue avec les modèles d'affichage des utilisateurs.</returns>
        // Action pour afficher la liste des utilisateurs
        [Authorize]
        public IActionResult List()
        {
            using (var userDAL = new UserDAL())
            {
                var users = userDAL.GetAllUsersWithType();
                var userViewModels = MapUsersToViewModels(users);
                return View(userViewModels);
            }
        }

        /// <summary>
        /// Authentification (connexion) de l'utilisateur.
        /// </summary>
        /// <returns>Vue pour la connexion.</returns>
        // Connexion
        [AllowAnonymous]
        // Action pour l'authentification (connexion) de l'utilisateur
        public IActionResult LogIn()
        {
            var model = new UserViewModel { LoggedIn = HttpContext.User.Identity.IsAuthenticated };
            if (model.LoggedIn)
            {
                using (var userDAL = new UserDAL())
                {
                    User user = userDAL.GetAllUsersWithTypeWithId(HttpContext.User.Identity.Name);
                    model.FirstName = user.FirstName;
                }
                return View(model);
            }

            //model.UserPrincipal = User;
            return View(model);
        }

        /// <summary>
        /// Traite le formulaire de connexion (authentification).
        /// </summary>
        /// <param name="model">Modèle d'affichage de l'utilisateur.</param>
        /// <param name="returnUrl">URL de redirection après la connexion.</param>
        /// <returns>Vue pour la connexion ou redirection.</returns>
        // Action pour le traitement du formulaire de connexion (authentification)
        [AllowAnonymous]
        [HttpPost]
        public IActionResult LogIn(UserViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                using (var userDAL = new UserDAL())
                {
                    User user = userDAL.Authentification(model.Email, model.Password);
                    if (user != null)
                    {
                        // Création des revendications (claims) pour l'utilisateur authentifié
                        var userClaims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.Name, user.Id.ToString()),
                            new Claim(ClaimTypes.Role, user.GetUserType())
                        };

                        var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

                        var mainUser = new ClaimsPrincipal(new[] { ClaimIdentity });

                        HttpContext.SignInAsync(mainUser);

                        // Redirection vers la page précédente si returnUrl est spécifié et est une URL locale
                        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                            return Redirect(returnUrl);
                        return Redirect("/");
                    }
                    ModelState.AddModelError("user.Email", "Prénom et/ou mot de passe incorrect(s)");
                }
                return View(model);
            }

            return View("/");
        }

        /// <summary>
        /// Déconnexion de l'utilisateur.
        /// </summary>
        /// <returns>Redirection vers la page d'accueil.</returns>
        // Action pour la déconnexion de l'utilisateur
        public ActionResult LogOut()
        {
            HttpContext.SignOutAsync();
            return Redirect("/");
        }

        /// <summary>
        /// Ajoute un client.
        /// </summary>
        /// <returns>Vue pour ajouter un client.</returns>
        //Création d'un compte client
        // Action pour l'ajout d'un client
        [AllowAnonymous]
        public IActionResult AddCustomer()
        {
            return View();
        }

        /// <summary>
        /// Traite le formulaire d'ajout d'un client.
        /// </summary>
        /// <param name="model">Modèle d'affichage de l'utilisateur.</param>
        /// <returns>Vue pour ajouter un client ou redirection.</returns>
        // Action pour le traitement du formulaire d'ajout d'un client
        [AllowAnonymous]
        [HttpPost]
        public IActionResult AddCustomers(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var userDAL = new UserDAL())
                {
                    int customerId = userDAL.CreateCustomer(model.LastName, model.FirstName, model.Email, model.Password, model.Address, model.PhoneNumber, model.BirthDate, model.LoyaltyPoint, model.CommentPoint);
                    User user = userDAL.Authentification(model.Email, model.Password);
                    // Création des revendications (claims) pour l'utilisateur authentifié
                    var userClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, customerId.ToString()),
                        new Claim(ClaimTypes.Role, user.GetUserType())
                    };

                    var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

                    var mainUser = new ClaimsPrincipal(new[] { ClaimIdentity });

                    HttpContext.SignInAsync(mainUser);

                    return RedirectToAction("IndexTEST", "Home");
                }
            }

            return View("AddCustomer", model);
        }

        /// <summary>
        /// Ajoute un partenaire.
        /// </summary>
        /// <returns>Vue pour ajouter un partenaire.</returns>
        // Action pour l'ajout d'un partenaire
        //[Authorize(Roles = "Administrator, Partner")]
        public IActionResult AddPartner()
        {
            return View();
        }

        /// <summary>
        /// Traite le formulaire d'ajout d'un partenaire.
        /// </summary>
        /// <param name="model">Modèle d'affichage de l'utilisateur.</param>
        /// <returns>Vue pour ajouter un partenaire ou redirection.</returns>
        // Action pour le traitement du formulaire d'ajout d'un partenaire
        //[Authorize(Roles = "Administrator, Partner")]
        [HttpPost]
        public IActionResult AddPartners(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var userDAL = new UserDAL())
                {
                    int partnerId = userDAL.CreatePartner(model.LastName, model.FirstName, model.Email, model.Password, model.Address, model.PhoneNumber, model.BirthDate, model.RoleName, model.RoleType);
                    User user = userDAL.Authentification(model.Email, model.Password);
                    // Création des revendications (claims) pour l'utilisateur authentifié
                    var userClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, partnerId.ToString()),
                        new Claim(ClaimTypes.Role, user.GetUserType())
                    };

                    var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

                    var mainUser = new ClaimsPrincipal(new[] { ClaimIdentity });

                    HttpContext.SignInAsync(mainUser);

                    return RedirectToAction("IndexTEST", "Home");
                }
            }

            return View("AddPartner", model);
        }

        /// <summary>
        /// Ajoute un administrateur.
        /// </summary>
        /// <returns>Vue pour ajouter un administrateur.</returns>
        // Action pour l'ajout d'un administrateur
        [Authorize(Roles = "Administrator")]
        public IActionResult AddAdministrator()
        {
            return View();
        }


        /// <summary>
        /// Action pour le traitement du formulaire d'ajout d'un administrateur
        /// </summary>
        /// <param name="model">Le modèle de l'utilisateur à ajouter</param>
        /// <returns>Renvoie une redirection vers l'action IndexTEST du contrôleur Home</returns>
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult AddAdministrators(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var userDAL = new UserDAL())
                {
                    int administratorId = userDAL.CreateAdministrator(model.LastName, model.FirstName, model.Email, model.Password, model.Address, model.PhoneNumber, model.BirthDate);
                    User user = userDAL.Authentification(model.Email, model.Password);
                    // Création des revendications (claims) pour l'utilisateur authentifié
                    var userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, administratorId.ToString()),
                new Claim(ClaimTypes.Role, user.GetUserType())
            };

                    var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

                    var mainUser = new ClaimsPrincipal(new[] { ClaimIdentity });

                    HttpContext.SignInAsync(mainUser);

                    return RedirectToAction("IndexTEST", "Home");
                }
            }

            return View("AddAdministrator", model);
        }

        /// <summary>
        /// Action pour la suppression d'un utilisateur
        /// </summary>
        /// <param name="id">L'ID de l'utilisateur à supprimer</param>
        /// <returns>Renvoie une redirection vers l'action IndexTEST du contrôleur Home</returns>
        [Authorize(Roles = "Administrator, Customer")]
        public IActionResult DeleteUsers(int id)
        {
            using (var userDAL = new UserDAL())
            {
                userDAL.DeleteUser(id);
            }

            return RedirectToAction("IndexTEST", "Home");
        }

        /// <summary>
        /// Action pour la modification d'un utilisateur
        /// </summary>
        /// <param name="id">L'ID de l'utilisateur à modifier</param>
        /// <returns>Renvoie une vue contenant le modèle de l'utilisateur à modifier</returns>
        [Authorize(Roles = "Administrator, Customer")]
        public IActionResult ChangeUser(int id)
        {
            using (var userDAL = new UserDAL())
            {
                var user = userDAL.GetAllUsersWithTypeWithId(id);
                if (user == null)
                {
                    return NotFound();
                }

                var model = MapUserToViewModel(user);
                return View(model);
            }
        }

        /// <summary>
        /// Action pour le traitement du formulaire de modification d'un utilisateur
        /// </summary>
        /// <param name="id">L'ID de l'utilisateur à modifier</param>
        /// <param name="model">Le modèle de l'utilisateur modifié</param>
        /// <returns>Renvoie une redirection vers l'action IndexTEST du contrôleur Home</returns>
        [HttpPost]
        public IActionResult ChangeUsers(int id, UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var userDAL = new UserDAL())
                {
                    userDAL.ModifyUser(id, model.LastName, model.FirstName, model.Email, model.Password, model.Address, model.PhoneNumber, model.BirthDate, model.ProfilePicture);
                }
                return RedirectToAction("IndexTEST", "Home");
            }

            return View(model);
        }

        /// <summary>
        /// Action pour la modification d'un client
        /// </summary>
        /// <param name="id">L'ID du client à modifier</param>
        /// <returns>Renvoie une vue contenant le modèle du client à modifier</returns>
        [Authorize(Roles = "Administrator, Customer")]
        public IActionResult ChangeCustomer(int id)
        {
            using (var userDAL = new UserDAL())
            {
                var user = userDAL.GetAllUsersWithTypeWithId(id);
                if (user == null || user.Customer == null)
                {
                    return NotFound("Not found");
                }

                var model = MapUserToViewModel(user);
                return View(model);
            }
        }

        /// <summary>
        /// Action pour le traitement du formulaire de modification d'un client
        /// </summary>
        /// <param name="id">L'ID du client à modifier</param>
        /// <param name="model">Le modèle du client modifié</param>
        /// <returns>Renvoie une redirection vers l'action IndexTEST du contrôleur Home</returns>
        [Authorize(Roles = "Administrator, Customer")]
        [HttpPost]
        public IActionResult ChangeCustomers(int id, UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var userDAL = new UserDAL())
                {
                    userDAL.ModifyCustomer(id, model.LastName, model.FirstName, model.Email, model.Password, model.Address, model.PhoneNumber, model.BirthDate, model.LoyaltyPoint, model.CommentPoint);
                }
                return RedirectToAction("IndexTEST", "Home");
            }

            return View("ChangeCustomer", model);
        }

        /// <summary>
        /// Action pour la modification d'un partenaire
        /// </summary>
        /// <param name="id">L'ID du partenaire à modifier</param>
        /// <returns>Renvoie une vue contenant le modèle du partenaire à modifier</returns>
        [Authorize(Roles = "Administrator, Partner")]
        public IActionResult ChangePartner(int id)
        {
            using (var userDAL = new UserDAL())
            {
                var user = userDAL.GetAllUsersWithTypeWithId(id);
                if (user == null || user.Partner == null)
                {
                    return NotFound();
                }

                var model = MapUserToViewModel(user);
                return View(model);
            }
        }

        /// <summary>
        /// Action pour le traitement du formulaire de modification d'un partenaire
        /// </summary>
        /// <param name="id">L'ID du partenaire à modifier</param>
        /// <param name="model">Le modèle du partenaire modifié</param>
        /// <returns>Renvoie une redirection vers l'action IndexTEST du contrôleur Home</returns>
        [Authorize(Roles = "Administrator, Partner")]
        [HttpPost]
        public IActionResult ChangePartners(int id, UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var userDAL = new UserDAL())
                {
                    userDAL.ModifyPartner(id, model.LastName, model.FirstName, model.Email, model.Password, model.Address, model.PhoneNumber, model.BirthDate, model.RoleName, model.RoleType);
                }
                return RedirectToAction("IndexTEST", "Home");
            }

            return View("ChangePartner", model);
        }

        /// <summary>
        /// Action pour la modification d'un administrateur
        /// </summary>
        /// <param name="id">L'ID de l'administrateur à modifier</param>
        /// <returns>Renvoie une vue contenant le modèle de l'administrateur à modifier</returns>
        [Authorize(Roles = "Administrator")]
        public IActionResult ChangeAdministrator(int id)
        {
            using (var userDAL = new UserDAL())
            {
                var user = userDAL.GetAllUsersWithTypeWithId(id);
                if (user == null || user.Administrator == null)
                {
                    return NotFound();
                }

                var model = MapUserToViewModel(user);
                return View(model);
            }
        }

        /// <summary>
        /// Action pour le traitement du formulaire de modification d'un administrateur
        /// </summary>
        /// <param name="id">L'ID de l'administrateur à modifier</param>
        /// <param name="model">Le modèle de l'administrateur modifié</param>
        /// <returns>Renvoie une redirection vers l'action Index du contrôleur Home</returns>
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult ChangeAdministrators(int id, UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var userDAL = new UserDAL())
                {
                    userDAL.ModifyAdministrator(id, model.LastName, model.FirstName, model.Email, model.Password, model.Address, model.PhoneNumber, model.BirthDate);
                }
                return RedirectToAction("IndexTEST", "Home");
            }

            return View("ChangeAdministrator", model);
        }

        /// <summary>
        /// Action pour la recherche d'un utilisateur
        /// </summary>
        /// <param name="query">La chaîne de recherche</param>
        /// <returns>Renvoie une vue contenant les résultats de recherche</returns>
        [Authorize(Roles = "Administrator")]
        public IActionResult FindUsers(string query)
        {
            using (var userDAL = new UserDAL())
            {
                var users = userDAL.SearchUser(query);
                var userViewModels = MapUsersToViewModels(users);
                var viewModel = new UserViewModel { Users = userViewModels };
                return View("ListeResultatRecherche", viewModel);
            }
        }

        /// <summary>
        /// Action pour afficher le profil de l'utilisateur actuellement connecté
        /// </summary>
        /// <returns>Renvoie une vue contenant le modèle de l'utilisateur</returns>
        [Authorize]
        public IActionResult Profile()
        {
            using (var userDAL = new UserDAL())
            {
                int userId = int.Parse(HttpContext.User.Identity.Name);
                var user = userDAL.GetAllUsersWithTypeWithId(userId);
                if (user == null)
                {
                    return NotFound();
                }

                var model = MapUserToViewModel(user);
                return View(model);
            }
        }

        /// <summary>
        /// Méthode pour mapper un objet User vers un ViewModel UserViewModel
        /// </summary>
        /// <param name="user">L'objet User à mapper</param>
        /// <returns>Renvoie un ViewModel UserViewModel mappé à partir de l'objet User</returns>
        public UserViewModel MapUserToViewModel(User user)
        {
            var model = new UserViewModel
            {
                UserId = user.Id,
                LastName = user.LastName,
                FirstName = user.FirstName,
                Email = user.Email,
                Password = user.Password,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                BirthDate = user.BirthDate,
                ProfilePicture = user.ProfilePicture,
                UserType = user.GetUserType()
            };

            if (user.Customer != null)
            {
                model.CustomerId = user.Customer.Id;
                model.LoyaltyPoint = user.Customer.LoyaltyPoint;
                model.CommentPoint = user.Customer.CommentPoint;
            }
            if (user.Partner != null)
            {
                model.PartnerId = user.Partner.Id;
                if (user.Partner.Role != null)
                {
                    model.RoleId = user.Partner.Role.Id;
                    model.RoleName = user.Partner.Role.Name;
                    model.RoleType = user.Partner.Role.Type;
                }
            }
            if (user.Administrator != null)
            {
                model.AdministratorId = user.Administrator.Id;
            }
            return model;
        }

        /// <summary>
        /// Méthode pour mapper une liste d'utilisateurs vers une liste de ViewModels d'utilisateurs
        /// </summary>
        /// <param name="users">La liste d'utilisateurs à mapper</param>
        /// <returns>Renvoie une liste de ViewModels d'utilisateurs mappée à partir de la liste d'utilisateurs</returns>
        public List<UserViewModel> MapUsersToViewModels(List<User> users)
        {
            return users.Select(MapUserToViewModel).ToList();
        }
    }
}