using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LittleBigTraveler.Models.DataBase;
using LittleBigTraveler.Models.TravelClasses;
using LittleBigTraveler.ViewModels;

namespace LittleBigTraveler.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult List()
        {
            using (var serviceDAL = new ServiceDAL())
            {
                var services = serviceDAL.GetAllServices();
                return View(services);
            }
        }

        // Création des données "Service"
        public IActionResult AddService() // Action pour aller à la vue AddService (formulaire de suppression)
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddServices(ServiceViewModel model) // Méthode à appeller dans "l'action" de la vue AddService
        {
            if (ModelState.IsValid)
            {
                using (var serviceDAL = new ServiceDAL())
                {
                    int serviceId = serviceDAL.CreateService(model.Name, model.Price, model.Schedule, model.Location, model.Type, model.MaxCapacity, model.Images, model.ExternalLinks);
                    return RedirectToAction("Index", "Home");
                }
            }

            return View("AddService", model);
        }

        // Suppression des données "Service"
        public IActionResult DeleteServices(int id) // Méthode à appeller sur le bouton Suppression
        {
            using (var serviceDAL = new ServiceDAL())
            {
                serviceDAL.DeleteService(id);
            }

            return RedirectToAction("List");
        }

        // Modification des données "Service"
        public IActionResult ChangeService(int id) // Action pour aller à la vue ChangeService (formulaire de modification)
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
                    MaxCapacity = service.MaxCapacity,
                    Images = service.Images,
                    ExternalLinks = service.ExternalLinks
                };

                return View(model);
            }
        }

        [HttpPost]
        public IActionResult ChangeServices(int id, ServiceViewModel model) // Méthode à appeller dans "l'action" de la vue ChangeService
        {
            if (ModelState.IsValid)
            {
                using (var serviceDAL = new ServiceDAL())
                {
                    serviceDAL.ModifyService(id, model.Name, model.Price, model.Schedule, model.Location, model.Type, model.MaxCapacity, model.Images, model.ExternalLinks);
                }

                return RedirectToAction("List");
            }

            return View("ChangeService", model);
        }

        // Recherche dans les données "Destination" d'après country, city et style
        public IActionResult FindServices(string query) // Méthode à appeller dans "l'action" du bouton Recherche
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
