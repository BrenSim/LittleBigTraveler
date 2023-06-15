using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LittleBigTraveler.Models.DataBase;
using LittleBigTraveler.Models.TravelClasses;
using LittleBigTraveler.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LittleBigTraveler.Controllers
{
    public class ServiceController : Controller
    {



        /// <summary>
        /// Action pour afficher la liste de tous les services.
        /// </summary>
        /// <returns>Vue contenant la liste des services.</returns>
        [AllowAnonymous]
        public IActionResult List()
        {
            using (var serviceDAL = new ServiceDAL())
            {
                var services = serviceDAL.GetAllServices();
                return View(services);
            }
        }

        /// <summary>
        /// Action pour ajouter un service (affiche le formulaire).
        /// </summary>
        /// <returns>Vue contenant le formulaire d'ajout d'un service.</returns>
        [Authorize(Roles = "Administrator, Partner")]
        public IActionResult AddService()
        {
            return View();
        }

        /// <summary>
        /// Méthode pour traiter le formulaire d'ajout d'un service.
        /// </summary>
        /// <param name="model">Modèle contenant les informations du service.</param>
        /// <returns>Redirige vers la page d'accueil en cas de succès, sinon réaffiche le formulaire avec les erreurs.</returns>
        [Authorize(Roles = "Administrator, Partner")]
        [HttpPost]
        public IActionResult AddServices(ServiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var serviceDAL = new ServiceDAL())
                {

                    int serviceId = serviceDAL.CreateService(model.Name, model.Price, model.Schedule, model.Location, model.Type, model.Style, model.MaxCapacity, model.Images, model.ExternalLinks, model.DestinationId);

                    return RedirectToAction("IndexTEST", "Home");
                }
            }

            return View("AddService", model);
        }

        /// <summary>
        /// Action pour supprimer un service.
        /// </summary>
        /// <param name="id">ID du service à supprimer.</param>
        /// <returns>Redirige vers la page de la liste des services.</returns>
        [Authorize(Roles = "Administrator, Partner")]
        public IActionResult DeleteServices(int id)
        {
            using (var serviceDAL = new ServiceDAL())
            {
                serviceDAL.DeleteService(id);
            }

            return RedirectToAction("List");
        }

        /// <summary>
        /// Action pour modifier un service (affiche le formulaire de modification).
        /// </summary>
        /// <param name="id">ID du service à modifier.</param>
        /// <returns>Vue contenant le formulaire de modification du service.</returns>
        [Authorize(Roles = "Administrator, Partner")]
        public IActionResult ChangeService(int id)
        {
            using (var serviceDAL = new ServiceDAL())
            {
                var service = serviceDAL.GetServiceWithId(id);
                if (service == null)
                {
                    return NotFound();
                }

                var model = new ServiceViewModel
                {
                    Id = service.Id,
                    Name = service.Name,
                    Price = service.Price,
                    Schedule = service.Schedule,
                    Location = service.Location,
                    Type = service.Type,
                    Style = service.Style,
                    MaxCapacity = service.MaxCapacity,
                    Images = service.Images,
                    ExternalLinks = service.ExternalLinks
                };

                return View(model);
            }
        }

        /// <summary>
        /// Méthode pour traiter le formulaire de modification d'un service.
        /// </summary>
        /// <param name="id">ID du service à modifier.</param>
        /// <param name="model">Modèle contenant les nouvelles informations du service.</param>
        /// <returns>Redirige vers la page de la liste des services en cas de succès, sinon réaffiche le formulaire avec les erreurs.</returns>
        [Authorize(Roles = "Administrator, Partner")]
        [HttpPost]
        public IActionResult ChangeServices(int id, ServiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var serviceDAL = new ServiceDAL())
                {

                    serviceDAL.ModifyService(id, model.Name, model.Price, model.Schedule, model.Location, model.Type, model.Style, model.MaxCapacity, model.Images, model.ExternalLinks, model.DestinationId);

                }

                return RedirectToAction("List");
            }

            return View("ChangeService", model);
        }

        /// <summary>
        /// Action pour rechercher des services.
        /// </summary>
        /// <param name="query">Requête de recherche.</param>
        /// <returns>Vue contenant les résultats de la recherche.</returns>
        [AllowAnonymous]
        public IActionResult FindServices(string query)
        {
            using (var serviceDAL = new ServiceDAL())
            {
                List<Service> services = serviceDAL.SearchService(query);
                var viewModel = new ServiceViewModel { Services = services };
                return View("ListSearchResult", viewModel);
            }
        }
    }
}


