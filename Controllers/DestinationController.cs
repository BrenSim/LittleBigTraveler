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
        public IActionResult List()
        {
            using (var destinationDAL = new DestinationDAL())
            {
                var destinations = destinationDAL.GetAllDestinations();
                return View(destinations);
            }
        }

        // Création des données "Destination"
        public IActionResult AddDestination() // Action pour aller à la vue AddDestination (formulaire d'ajout)
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddDestinations(DestinationViewModel model) // Méthode à appeller dans "l'action" de la vue AddDestination
        {
            if (ModelState.IsValid)
            {
                using (var destinationDAL = new DestinationDAL())
                {
                    int destinationId = destinationDAL.CreateDestination(model.Country, model.City, model.Description, model.Images, model.ExternalLinks);
                    return RedirectToAction("Index", "Home");
                }
            }

            return View("AddDestination", model);
        }

        // Suppression des données "Destination"
        public IActionResult DeleteDestinations(int id) // Méthode à appeller sur le bouton Suppression
        {
            using (var destinationDAL = new DestinationDAL())
            {
                destinationDAL.DeleteDestination(id);
            }

            return RedirectToAction("List");
        }

        // Modification des données "Destination"
        public IActionResult ChangeDestination(int id) // Action pour aller à la vue ChangeDestination (formulaire de modification)
        {
            using (var destinationDAL = new DestinationDAL())
            {
                var destination = destinationDAL.GetDestinationWithId(id);
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
                    //Style = destination.Style,
                    Images = destination.Images,
                    ExternalLinks = destination.ExternalLinks
                };

                return View(model);
            }
        }

        [HttpPost]
        public IActionResult ChangeDestinations(int id, DestinationViewModel model) // Méthode à appeller dans "l'action" de la vue ChangeDestination
        {
            if (ModelState.IsValid)
            {
                using (var destinationDAL = new DestinationDAL())
                {
                    destinationDAL.ModifyDestination(id, model.Country, model.City, model.Description, model.Images, model.ExternalLinks);
                }

                return RedirectToAction("List");
            }

            return View("ChangeDestination", model);
        }

        // Recherche dans les données "Destination" d'après country, city et style
        public IActionResult FindDestinations(string query) // Méthode à appeller dans "l'action" du bouton Recherche
        {
            using (var destinationDAL = new DestinationDAL())
            {
                List<Destination> destinations = destinationDAL.SearchDestination(query);
                var viewModel = new DestinationViewModel { Destinations = destinations };
                return View("ListSearchResult", viewModel);
            }
        }

    }
}
