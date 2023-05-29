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
        public IActionResult Liste()
        {
            using (var dal = new Dal())
            {
                var services = dal.ObtientTousServices();
                return View(services);
            }
        }

        public IActionResult AjoutService()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AjouterService(ServiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var dal = new Dal())
                {
                    int serviceId = dal.CreerService(model.Name, model.Price, model.Schedule, model.Location, model.Type, model.MaxCapacity, model.Images, model.ExternalLinks);
                    // Autres actions à effectuer après la création du service
                    return RedirectToAction("Index", "Home");
                }
            }

            return View("AjoutService", model);
        }

        public IActionResult SuppService(int id)
        {
            using (var dal = new Dal())
            {
                dal.SupprimerService(id);
                // Autres actions à effectuer après la suppression du service
            }

            return RedirectToAction("Liste");
        }

        public IActionResult ModiService(int id)
        {
            using (var dal = new Dal())
            {
                var service = dal.ObtientServiceParId(id);
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
        public IActionResult ModifierService(int id, ServiceViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var dal = new Dal())
                {
                    dal.ModifierService(id, model.Name, model.Price, model.Schedule, model.Location, model.Type, model.MaxCapacity, model.Images, model.ExternalLinks);
                    // Autres actions à effectuer après la modification du service
                }

                return RedirectToAction("Liste");
            }

            return View("ModiService", model);
        }

        public IActionResult Rechercher(string query)
        {
            using (var dal = new Dal())
            {
                List<Service> services = dal.RechercherServices(query);
                var viewModel = new ServiceViewModel { Services = services };
                return View("ListeResultatRecherche", viewModel);
            }
        }
    }
}
