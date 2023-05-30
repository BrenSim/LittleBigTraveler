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
        public IActionResult List()
        {
            using (var userDAL = new UserDAL())
            {
                var users = userDAL.GetAllUsersWithType();
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
        public IActionResult AddCustomers(UserViewModel model) // Méthode à appeller dans "l'action" du Front
        {
            if (ModelState.IsValid)
            {
                using (var userDAL = new UserDAL())
                {
                    int customerId = userDAL.CreateCustomer(model.LastName, model.FirstName, model.Email, model.Password, model.Address, model.PhoneNumber, model.BirthDate, model.LoyaltyPoint, model.CommentPoint);
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
        public IActionResult AddPartners(UserViewModel model) // Méthode à appeller dans "l'action" du Front
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

        [HttpPost]
        public IActionResult AddAdministrators(UserViewModel model) // Méthode à appeller dans "l'action" du Front
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
        public IActionResult DeleteUsers(int id) // Méthode à appeller dans "l'action" du Front
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

        [HttpPost]
        public IActionResult ChangeUsers(int id, UserViewModel model) // Méthode à appeller dans "l'action" du Front
        {
            if (ModelState.IsValid)
            {
                using (var userDAL = new UserDAL())
                {
                    userDAL.ModifyUser(id, model.LastName, model.FirstName, model.Email, model.Password, model.Address, model.PhoneNumber, model.BirthDate, model.ProfilePicture);
                }
                return RedirectToAction("Liste");
            }

            return View("ModiUser", model);
        }

        // Action pour la recherche d'un utilisateur
        public IActionResult FindUsers(string query) // Méthode à appeller dans "l'action" du Front
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

        // Action pour "Mapper" le ViewModel
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


        public List<UserViewModel> MapUsersToViewModels(List<User> users)
        {
            return users.Select(MapUserToViewModel).ToList();
        }
    }
}
