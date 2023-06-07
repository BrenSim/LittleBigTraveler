using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LittleBigTraveler.Models.DataBase;
using LittleBigTraveler.Models.TravelClasses;
using LittleBigTraveler.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LittleBigTraveler.Controllers
{
    public class ServiceController : Controller
    {

        // Action pour afficher la liste de tout les services
        public IActionResult List()
        {
            using (var serviceDAL = new ServiceDAL())
            {
                var services = serviceDAL.GetAllServices();
                return View(services);
            }
        }

        // Action pour ajouter un service (affiche le formulaire)
        //[Authorize(Roles = "Administrator, Partner")]
        public IActionResult AddService()
        {
            return View();
        }

        // Méthode pour traiter le formulaire d'ajout d'un service
        //[Authorize(Roles = "Administrator, Partner")]
        [HttpPost]
        public IActionResult AddServices(ServiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var serviceDAL = new ServiceDAL())
                {
                    int serviceId = serviceDAL.CreateService(model.Name, model.Price, model.Schedule, model.Location, model.Type, model.Style, model.MaxCapacity, model.Images, model.ExternalLinks, model.DestinationId); // Ajout de model.DestinationId
                    return RedirectToAction("IndexTEST", "Home");
                }
            }

            return View("AddService", model);
        }

        // Action pour supprimer un service
        [Authorize(Roles = "Administrator, Partner")]
        public IActionResult DeleteServices(int id)
        {
            using (var serviceDAL = new ServiceDAL())
            {
                serviceDAL.DeleteService(id);
            }

            return RedirectToAction("List");
        }

        // Action pour modifier un service (affiche le formulaire de modification)
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

        // Méthode pour traiter le formulaire de modification d'un service
        [Authorize(Roles = "Administrator, Partner")]
        [HttpPost]
        public IActionResult ChangeServices(int id, ServiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var serviceDAL = new ServiceDAL())
                {
                    serviceDAL.ModifyService(id, model.Name, model.Price, model.Schedule, model.Location, model.Type, model.Style, model.MaxCapacity, model.Images, model.ExternalLinks, model.DestinationId); // Ajout de model.DestinationId
                }

                return RedirectToAction("List");
            }

            return View("ChangeService", model);
        }

        // Action pour rechercher des services
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


