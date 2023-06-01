﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LittleBigTraveler.Models.DataBase;
using LittleBigTraveler.Models.UserClasses;
using LittleBigTraveler.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace LittleBigTraveler.Controllers
{
    public class UserController : Controller
    {
        // Action pour afficher la liste des utilisateurs
        public IActionResult List()
        {
            using (var userDAL = new UserDAL())
            {
                var users = userDAL.GetAllUsersWithType();
                var userViewModels = MapUsersToViewModels(users);
                return View(userViewModels);
            }
        }

        // Action pour l'authentification (connexion) de l'utilisateur
        public IActionResult LogIn()
        {
            var model = new UserViewModel { LoggedIn = HttpContext.User.Identity.IsAuthenticated };
            if (model.LoggedIn)
            {
                using (var userDAL = new UserDAL())
                {
                    User user = userDAL.GetUserById(HttpContext.User.Identity.Name);
                    model.FirstName = user.FirstName;
                }
                return View(model);
            }
            return View(model);
        }

        // Action pour le traitement du formulaire de connexion (authentification)
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

            return View("Index", "Home");
        }

        // Action pour la déconnexion de l'utilisateur
        public ActionResult LogOut()
        {
            HttpContext.SignOutAsync();
            return Redirect("/");
        }

        // Action pour l'ajout d'un client
        public IActionResult AddCustomer()
        {
            return View();
        }

        // Action pour le traitement du formulaire d'ajout d'un client
        [HttpPost]
        public IActionResult AddCustomers(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var userDAL = new UserDAL())
                {
                    int customerId = userDAL.CreateCustomer(model.LastName, model.FirstName, model.Email, model.Password, model.Address, model.PhoneNumber, model.BirthDate, model.LoyaltyPoint, model.CommentPoint);

                    // Création des revendications (claims) pour l'utilisateur authentifié
                    var userClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, customerId.ToString()),
                    };

                    var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");

                    var mainUser = new ClaimsPrincipal(new[] { ClaimIdentity });

                    HttpContext.SignInAsync(mainUser);

                    return RedirectToAction("Index", "Home");
                }
            }

            return View("AddCustomer", model);
        }

        // Action pour l'ajout d'un partenaire
        // Action pour l'ajout d'un partenaire
        public IActionResult AddPartner()
        {
            return View();
        }

        // Action pour le traitement du formulaire d'ajout d'un partenaire
        [HttpPost]
        public IActionResult AddPartners(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var userDAL = new UserDAL())
                {
                    int partnerId = userDAL.CreatePartner(model.LastName, model.FirstName, model.Email, model.Password, model.Address, model.PhoneNumber, model.BirthDate, model.RoleName, model.RoleType);
                    return RedirectToAction("Index", "Home");
                }
            }

            return View("AddPartner", model);
        }

        // Action pour l'ajout d'un administrateur
        public IActionResult AddAdministrator()
        {
            return View();
        }

        // Action pour le traitement du formulaire d'ajout d'un administrateur
        [HttpPost]
        public IActionResult AddAdministrators(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var userDAL = new UserDAL())
                {
                    int administratorId = userDAL.CreateAdministrator(model.LastName, model.FirstName, model.Email, model.Password, model.Address, model.PhoneNumber, model.BirthDate);
                    return RedirectToAction("Index", "Home");
                }
            }

            return View("AddAdministrator", model);
        }

        // Action pour la suppression d'un utilisateur
        public IActionResult DeleteUsers(int id)
        {
            using (var userDAL = new UserDAL())
            {
                userDAL.DeleteUser(id);
            }

            return RedirectToAction("List");
        }

        // Action pour la modification d'un utilisateur
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

        // Action pour le traitement du formulaire de modification d'un utilisateur
        [HttpPost]
        public IActionResult ChangeUsers(int id, UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var userDAL = new UserDAL())
                {
                    userDAL.ModifyUser(id, model.LastName, model.FirstName, model.Email, model.Password, model.Address, model.PhoneNumber, model.BirthDate, model.ProfilePicture);
                }
                return RedirectToAction("List");
            }

            return View(model);
        }

        // Action pour la modification d'un client
        [HttpGet]
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

        // Action pour le traitement du formulaire de modification d'un client
        [HttpPost]
        public IActionResult ChangeCustomers(int id, UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var userDAL = new UserDAL())
                {
                    userDAL.ModifyCustomer(id, model.LastName, model.FirstName, model.Email, model.Password, model.Address, model.PhoneNumber, model.BirthDate, model.LoyaltyPoint, model.CommentPoint);
                }
                return RedirectToAction("List");
            }

            return View("ChangeCustomer", model);
        }

        // Action pour la modification d'un partenaire
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

        // Action pour le traitement du formulaire de modification d'un partenaire
        [HttpPost]
        public IActionResult ChangePartners(int id, UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var userDAL = new UserDAL())
                {
                    userDAL.ModifyPartner(id, model.LastName, model.FirstName, model.Email, model.Password, model.Address, model.PhoneNumber, model.BirthDate, model.RoleName, model.RoleType);
                }
                return RedirectToAction("List");
            }

            return View("ChangePartner", model);
        }

        // Action pour la modification d'un administrateur
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

        // Action pour le traitement du formulaire de modification d'un administrateur
        [HttpPost]
        public IActionResult ChangeAdministrators(int id, UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var userDAL = new UserDAL())
                {
                    userDAL.ModifyAdministrator(id, model.LastName, model.FirstName, model.Email, model.Password, model.Address, model.PhoneNumber, model.BirthDate);
                }
                return RedirectToAction("List");
            }

            return View("ChangeAdministrator", model);
        }

        // Action pour la recherche d'un utilisateur
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

        // Action pour la création d'un User (et non pas d'un Customer etc.) (outdated)
        public int CreateUser(UserViewModel model)
        {
            using (var userDAL = new UserDAL())
            {
                switch (model.UserType)
                {
                    case "Customer":
                        return userDAL.CreateCustomer(model.LastName, model.FirstName, model.Email, model.Password, model.Address, model.PhoneNumber, model.BirthDate, model.LoyaltyPoint, model.CommentPoint);
                    case "Partner":
                        return userDAL.CreatePartner(model.LastName, model.FirstName, model.Email, model.Password, model.Address, model.PhoneNumber, model.BirthDate, model.RoleName, model.RoleType);
                    case "Administrator":
                        return userDAL.CreateAdministrator(model.LastName, model.FirstName, model.Email, model.Password, model.Address, model.PhoneNumber, model.BirthDate);
                    default:
                        throw new ArgumentException("Invalid user type");
                }
            }
        }

        // Action pour "Mapper" le ViewModel d'un utilisateur
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
                model.RoleId = user.Partner.Role.Id;
                model.RoleName = user.Partner.Role.Name;
                model.RoleType = user.Partner.Role.Type;
            }
            if (user.Administrator != null)
            {
                model.AdministratorId = user.Administrator.Id;
            }
            return model;
        }

        // Méthode pour mapper une liste d'utilisateurs vers une liste de ViewModels d'utilisateurs
        public List<UserViewModel> MapUsersToViewModels(List<User> users)
        {
            return users.Select(MapUserToViewModel).ToList();
        }
    }
}



