using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LittleBigTraveler.Models.DataBase;
using LittleBigTraveler.Models.TravelClasses;
using LittleBigTraveler.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;


namespace LittleBigTraveler.Controllers
{
    /// <summary>
    /// Contrôleur gérant les actions liées aux destinations.
    /// </summary>
    public class DestinationController : Controller
    {

        /// <summary>
        /// Action pour afficher la liste des destinations.
        /// </summary>
        /// <returns>Vue contenant la liste des destinations.</returns>
        [AllowAnonymous]

        public IActionResult List()
        {
            using (var destinationDAL = new DestinationDAL())
            {
                var destinations = destinationDAL.GetAllDestinations();
                return View(destinations);
            }
        }

        /// <summary>
        /// Action pour ajouter une destination (affiche le formulaire).
        /// </summary>
        /// <returns>Vue contenant le formulaire d'ajout d'une destination.</returns>
        [Authorize(Roles = "Administrator")]
        public IActionResult AddDestination()
        {
            return View();
        }

        /// <summary>
        /// Méthode pour traiter le formulaire d'ajout d'une destination.
        /// </summary>
        /// <param name="model">Modèle contenant les informations de la destination à ajouter.</param>
        /// <returns>Redirige vers l'action List en cas de succès, sinon réaffiche le formulaire d'ajout.</returns>
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public IActionResult AddDestinations(DestinationViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var destinationDAL = new DestinationDAL())
                {
                    int destinationId = destinationDAL.CreateDestination(model.Country, model.City, model.Description, model.Images, model.ExternalLinks);
                    return RedirectToAction("List");
                }
            }

            return View("AddDestination", model);
        }

        /// <summary>
        /// Action pour supprimer une destination.
        /// </summary>
        /// <param name="id">ID de la destination à supprimer.</param>
        /// <returns>Redirige vers l'action "List" pour afficher la liste mise à jour des destinations.</returns>
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteDestinations(int id)
        {
            using (var destinationDAL = new DestinationDAL())
            {
                destinationDAL.DeleteDestination(id);
            }

            return RedirectToAction("List");
        }

        /// <summary>
        /// Action pour modifier une destination (affiche le formulaire de modification).
        /// </summary>
        /// <param name="id">ID de la destination à modifier.</param>
        /// <returns>Vue contenant le formulaire de modification de la destination.</returns>
        [Authorize(Roles = "Administrator")]
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

        /// <summary>
        /// Méthode pour traiter le formulaire de modification d'une destination.
        /// </summary>
        /// <param name="id">ID de la destination à modifier.</param>
        /// <param name="model">Modèle contenant les informations modifiées de la destination.</param>
        /// <returns>Redirige vers l'action "List" pour afficher la liste mise à jour des destinations.</returns>
        [Authorize(Roles = "Administrator")]
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

        /// <summary>
        /// Action pour rechercher des destinations.
        /// </summary>
        /// <param name="query">Terme de recherche.</param>
        /// <returns>Vue contenant les résultats de la recherche.</returns>
        [AllowAnonymous]
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
