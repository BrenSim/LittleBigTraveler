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
        private readonly Dal _dal;

        public UserController(Dal dal)
        {
            _dal = dal;
        }

        public IActionResult Liste()
        {
            var users = _dal.ObtientTousUsersAvecType();
            var userViewModels = MapUsersToViewModels(users);
            return View(userViewModels);
        }

        public IActionResult AjoutUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AjouterUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                int userId = CreateUser(model);
                // Autres actions à effectuer après la création de l'utilisateur
                return RedirectToAction("Index", "Home");
            }

            return View("AjoutUser", model);
        }

        public IActionResult SuppUser(int id)
        {
            _dal.SupprimerUser(id);
            // Autres actions à effectuer après la suppression de l'utilisateur

            return RedirectToAction("Liste");
        }

        public IActionResult ModiUser(int id)
        {
            var user = _dal.ObtientUserParIdAvecType(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = MapUserToViewModel(user);

            return View(model);
        }

        [HttpPost]
        public IActionResult ModifierUser(int id, UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                _dal.ModifierUser(id, model.LastName, model.FirstName, model.Email, model.Password, model.Address, model.PhoneNumber, model.BirthDate, model.ProfilePicture);
                // Autres actions à effectuer après la modification de l'utilisateur

                return RedirectToAction("Liste");
            }

            return View("ModiUser", model);
        }

        public IActionResult Rechercher(string query)
        {
            var users = _dal.RechercherUsers(query);
            var userViewModels = MapUsersToViewModels(users);
            var viewModel = new UserViewModel { Users = userViewModels };

            return View("ListeResultatRecherche", viewModel);
        }

        private int CreateUser(UserViewModel model)
        {
            switch (model.UserType)
            {
                case "Customer":
                    return _dal.CreerClient(model.LastName, model.FirstName, model.Email, model.Password, model.Address, model.PhoneNumber, model.BirthDate, model.LoyaltyPoint, model.CommentPoint);
                case "Partner":
                    return _dal.CreerPartenaireAvecRole(model.LastName, model.FirstName, model.Email, model.Password, model.Address, model.PhoneNumber, model.BirthDate, model.RoleName, model.RoleType);
                case "Administrator":
                    return _dal.CreerAdministrateur(model.LastName, model.FirstName, model.Email, model.Password, model.Address, model.PhoneNumber, model.BirthDate);
                default:
                    throw new ArgumentException("Invalid user type");
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
                model.LoyaltyPoint = user.Customer.LoyaltyPoint;
                model.CommentPoint = user.Customer.CommentPoint;
            }
            if (user.Partner != null)
            {
                model.RoleName = user.Partner.Role.Name;
                model.RoleType = user.Partner.Role.Type;
            }

            return model;
        }

        private List<UserViewModel> MapUsersToViewModels(List<User> users)
        {
            return users.Select(MapUserToViewModel).ToList();
        }
    }
}