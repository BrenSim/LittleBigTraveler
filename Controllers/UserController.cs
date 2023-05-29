using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LittleBigTraveler.Models.DataBase;
using LittleBigTraveler.Models.UserClasses;
using LittleBigTraveler.ViewModels;

namespace LittleBigTraveler.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Liste()
        {
            using (var dal = new Dal())
            {
                var users = dal.ObtientTousUsersAvecType();
                var userViewModels = MapUsersToViewModels(users);
                return View(userViewModels);
            }
        }

        // Action pour l'ajout d'un client
        public IActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCustomers(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var dal = new Dal())
                {
                    int customerId = dal.CreerClient(model.LastName, model.FirstName, model.Email, model.Password, model.Address, model.PhoneNumber, model.BirthDate, model.LoyaltyPoint, model.CommentPoint);
                    // Autres actions à effectuer après la création du client
                    return RedirectToAction("Index", "Home");
                }
            }

            return View("AddCustomer", model);
        }

        // Action pour l'ajout d'un partenaire
        public IActionResult AddPartner()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPartners(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var dal = new Dal())
                {
                    int partnerId = dal.CreerPartenaireAvecRole(model.LastName, model.FirstName, model.Email, model.Password, model.Address, model.PhoneNumber, model.BirthDate, model.RoleName, model.RoleType);
                    // Autres actions à effectuer après la création du partenaire
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

        [HttpPost]
        public IActionResult AddAdministrators(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var dal = new Dal())
                {
                    int administratorId = dal.CreerAdministrateur(model.LastName, model.FirstName, model.Email, model.Password, model.Address, model.PhoneNumber, model.BirthDate);
                    // Autres actions à effectuer après la création de l'administrateur
                    return RedirectToAction("Index", "Home");
                }
            }

            return View("AddAdministrator", model);
        }

        public IActionResult SuppUser(int id)
        {
            using (var dal = new Dal())
            {
                dal.SupprimerUser(id);
                // Autres actions à effectuer après la suppression de l'utilisateur
            }

            return RedirectToAction("Liste");
        }

        public IActionResult ModiUser(int id)
        {
            using (var dal = new Dal())
            {
                var user = dal.ObtientUserParIdAvecType(id);
                if (user == null)
                {
                    return NotFound();
                }

                var model = MapUserToViewModel(user);
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult ModifierUser(int id, UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var dal = new Dal())
                {
                    dal.ModifierUser(id, model.LastName, model.FirstName, model.Email, model.Password, model.Address, model.PhoneNumber, model.BirthDate, model.ProfilePicture);
                    // Autres actions à effectuer après la modification de l'utilisateur
                }
                return RedirectToAction("Liste");
            }

            return View("ModiUser", model);
        }

        public IActionResult Rechercher(string query)
        {
            using (var dal = new Dal())
            {
                var users = dal.RechercherUsers(query);
                var userViewModels = MapUsersToViewModels(users);
                var viewModel = new UserViewModel { Users = userViewModels };
                return View("ListeResultatRecherche", viewModel);
            }
        }

        private int CreateUser(UserViewModel model)
        {
            using (var dal = new Dal())
            {
                switch (model.UserType)
                {
                    case "Customer":
                        return dal.CreerClient(model.LastName, model.FirstName, model.Email, model.Password, model.Address, model.PhoneNumber, model.BirthDate, model.LoyaltyPoint, model.CommentPoint);
                    case "Partner":
                        return dal.CreerPartenaireAvecRole(model.LastName, model.FirstName, model.Email, model.Password, model.Address, model.PhoneNumber, model.BirthDate, model.RoleName, model.RoleType);
                    case "Administrator":
                        return dal.CreerAdministrateur(model.LastName, model.FirstName, model.Email, model.Password, model.Address, model.PhoneNumber, model.BirthDate);
                    default:
                        throw new ArgumentException("Invalid user type");
                }
            }
        }

        private UserViewModel MapUserToViewModel(User user)
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


        private List<UserViewModel> MapUsersToViewModels(List<User> users)
        {
            return users.Select(MapUserToViewModel).ToList();
        }
    }
}
