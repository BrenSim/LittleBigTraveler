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
        // Action pour afficher la liste des destinations
        public IActionResult List()
        {
            using (var destinationDAL = new DestinationDAL())
            {
                var destinations = destinationDAL.GetAllDestinations();
                return View(destinations);
            }
        }

        // Action pour ajouter une destination (affiche le formulaire)
        public IActionResult AddDestination()
        {
            return View();
        }

        // Méthode pour traiter le formulaire d'ajout d'une destination
        [HttpPost]
        public IActionResult AddDestinations(DestinationViewModel model)
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

        // Action pour supprimer une destination
        public IActionResult DeleteDestinations(int id)
        {
            using (var destinationDAL = new DestinationDAL())
            {
                destinationDAL.DeleteDestination(id);
            }

            return RedirectToAction("List");
        }

        // Action pour modifier une destination (affiche le formulaire de modification)
        public IActionResult ChangeDestination(int id)
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
                    Images = destination.Images,
                    ExternalLinks = destination.ExternalLinks
                };

                return View(model);
            }
        }

        // Méthode pour traiter le formulaire de modification d'une destination
        [HttpPost]
        public IActionResult ChangeDestinations(int id, DestinationViewModel model)
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

        // Action pour rechercher des destinations
        public IActionResult FindDestinations(string query)
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
