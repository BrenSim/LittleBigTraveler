using LittleBigTraveler.Models.DataBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LittleBigTraveler.ViewModels;
using System;
using Microsoft.AspNetCore.Authorization;

public class TravelController : Controller
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TravelController(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Action pour afficher la liste des voyages.
    /// </summary>
    /// <returns>Vue contenant la liste des voyages.</returns>
    [Authorize]
    public IActionResult List()
    {
        using (var travelDAL = new TravelDAL(_httpContextAccessor))
        {
            var travels = travelDAL.GetAllTravels();

            if (travels == null || travels.Count == 0)
            {
                return View("List"); // Appelle la vue "List.cshtml" lorsque la liste des voyages est vide
            }

            return View(travels);
        }
    }

    /// <summary>
    /// Action pour créer un voyage.
    /// </summary>
    /// <param name="destinationId">ID de la destination du voyage.</param>
    /// <returns>Vue contenant le formulaire de création d'un voyage.</returns>
    [Authorize]
    public IActionResult CreateTravel(int destinationId)
    {
        using (var destinationDAL = new DestinationDAL())
        {
            var destination = destinationDAL.GetDestinationWithId(destinationId);
            if (destination == null)
            {
                return NotFound("Problème avec la destination");
            }

            var model = new TravelViewModel(_httpContextAccessor)
            {
                DestinationId = destinationId,
                Destination = destination,
                DepartureLocation = "",
                DepartureDate = DateTime.Now,
                ReturnDate = DateTime.Now.AddDays(7),
                Price = 0,
                NumParticipants = 1
            };

            return View(model);
        }
    }

    /// <summary>
    /// Méthode pour traiter le formulaire de création d'un voyage.
    /// </summary>
    /// <param name="model">Modèle contenant les informations du voyage.</param>
    /// <returns>Redirige vers la page d'accueil ou une autre page en cas de succès, sinon réaffiche le formulaire avec les erreurs.</returns>
    [Authorize]
    [HttpPost]
    public IActionResult CreateTravel([Bind("DestinationId,DepartureLocation,DepartureDate,ReturnDate,NumParticipants")] TravelViewModel model)
    {
        // Récupérer l'ID du client connecté depuis le contexte HTTP
        int customerId = int.Parse(HttpContext.User.Identity.Name);

        using (var travelDAL = new TravelDAL(_httpContextAccessor))
        {
            try
            {
                travelDAL.CreateTravel(model.DestinationId, model.DepartureLocation, model.DepartureDate, model.ReturnDate, model.Price, model.NumParticipants);
                return RedirectToAction("List"); // Rediriger vers la page d'accueil ou une autre page
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            // Si une erreur se produit, revenir à la vue avec les données saisies
            return View(model);
        }
    }

    /// <summary>
    /// Action pour afficher le formulaire de modification d'un voyage.
    /// </summary>
    /// <param name="id">ID du voyage à modifier.</param>
    /// <returns>Vue contenant le formulaire de modification du voyage.</returns>
    [Authorize]
    public IActionResult ModifyTravel(int id)
    {
        using (var travelDAL = new TravelDAL(_httpContextAccessor))
        {
            var travel = travelDAL.GetTravelById(id);
            if (travel == null)
            {
                return NotFound("Voyage introuvable");
            }

            var model = new TravelViewModel(_httpContextAccessor)
            {
                Id = travel.Id,
                DestinationId = travel.Destination.Id,
                DestinationCity = travel.Destination.City,
                Destination = travel.Destination,
                DepartureLocation = travel.DepartureLocation,
                DepartureDate = travel.DepartureDate,
                ReturnDate = travel.ReturnDate,
                Price = travel.Price,
                NumParticipants = travel.NumParticipants
            };

            return View(model);
        }
    }

    /// <summary>
    /// Action pour modifier un voyage.
    /// </summary>
    /// <param name="model">Modèle contenant les nouvelles informations du voyage.</param>
    /// <returns>Redirige vers la page de la liste des voyages en cas de succès, sinon réaffiche le formulaire avec les erreurs.</returns>
    [Authorize]
    [HttpPost]
    public IActionResult ModifyTravel(TravelViewModel model)
    {
        // Récupérer l'ID du client connecté depuis le contexte HTTP
        int customerId = int.Parse(HttpContext.User.Identity.Name);

        using (var travelDAL = new TravelDAL(_httpContextAccessor))
        {
            try
            {
                travelDAL.ModifyTravel(model.Id, model.DestinationId, model.DepartureLocation, model.DepartureDate, model.ReturnDate, model.Price, model.NumParticipants);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            // Si une erreur se produit, revenir à la vue avec les données saisies
            return View(model);
        }
    }

    /// <summary>
    /// Action pour supprimer un voyage.
    /// </summary>
    /// <param name="id">ID du voyage à supprimer.</param>
    /// <returns>Redirige vers la page de la liste des voyages en cas de succès, sinon renvoie une réponse NotFound.</returns>
    [Authorize]
    public IActionResult DeleteTravel(int id)
    {
        using (var travelDAL = new TravelDAL(_httpContextAccessor))
        {
            try
            {
                travelDAL.DeleteTravel(id);
                return RedirectToAction("List"); // Rediriger vers la page de la liste des voyages
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
