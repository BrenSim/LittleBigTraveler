using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LittleBigTraveler.Models.DataBase;
using LittleBigTraveler.Models.TravelClasses;
using LittleBigTraveler.ViewModels;

namespace LittleBigTraveler.Controllers
{
    public class DestinationController : Controller
    {
        public IActionResult Liste()
        {
            using (var dal = new Dal())
            {
                var destinations = dal.ObtientToutesDestination();
                return View(destinations);
            }
        }

        public IActionResult AjoutDestination()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AjouterDestination(DestinationViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var dal = new Dal())
                {
                    int destinationId = dal.CreerDestination(model.Country, model.City, model.Description, model.Style, model.Images, model.ExternalLinks);
                    // Autres actions à effectuer après la création de la destination
                    return RedirectToAction("Index", "Home");
                }
            }

            return View("AjoutDestination", model);
        }

        public IActionResult SuppDestination(int id)
        {
            using (var dal = new Dal())
            {
                dal.SupprimerDestination(id);
                // Autres actions à effectuer après la suppression de la destination
            }

            return RedirectToAction("Liste");
        }

        public IActionResult ModiDestination(int id)
        {
            using (var dal = new Dal())
            {
                var destination = dal.ObtientDestinationParId(id);
                if (destination == null)
                {
                    return NotFound();
                }

                var model = new DestinationViewModel
                {
                    Id = destination.Id,
                    Country = destination.Country,
                    City = destination.City,
                    Description = destination.Description,
                    Style = destination.Style,
                    Images = destination.Images,
                    ExternalLinks = destination.ExternalLinks
                };

                return View(model);
            }
        }

        [HttpPost]
        public IActionResult ModifierDestination(int id, DestinationViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var dal = new Dal())
                {
                    dal.ModifierDestination(id, model.Country, model.City, model.Description, model.Style, model.Images, model.ExternalLinks);
                    // Autres actions à effectuer après la modification de la destination
                }

                return RedirectToAction("Liste");
            }

            return View("ModiDestination", model);
        }

        public IActionResult Rechercher(string query)
        {
            using (var dal = new Dal())
            {
                List<Destination> destinations = dal.RechercherDestinations(query);
                var viewModel = new DestinationViewModel { Destinations = destinations };
                return View("ListeResultatRecherche", viewModel);
            }
        }

    }
}