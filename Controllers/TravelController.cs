using LittleBigTraveler.Models.DataBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LittleBigTraveler.ViewModels;
using System;

public class TravelController : Controller
{
    private readonly IHttpContextAccessor HttpContextAccessor;

    public TravelController(IHttpContextAccessor httpContextAccessor)
    {
        HttpContextAccessor = httpContextAccessor;
    }

    // Action pour afficher la liste des voyages d'un client
    public IActionResult List()
    {
        // Récupérer l'ID du client connecté depuis le contexte HTTP
        int customerId = int.Parse(HttpContext.User.Identity.Name);

        using (var travelDAL = new TravelDAL(HttpContextAccessor))
        {
            var travels = travelDAL.GetTravelsByCustomerId(customerId);

            if (travels == null || travels.Count == 0)
            {
                return View("List"); // Appelle la vue "List.cshtml" lorsque la liste des voyages est vide
            }

            return View(travels);
        }
    }

    // Action pour créer un voyage
    public IActionResult CreateTravel(int destinationId)
    {
        using (var destinationDAL = new DestinationDAL())
        {
            var destination = destinationDAL.GetDestinationWithId(destinationId);
            if (destination == null)
            {
                return NotFound("problème destination");
            }

            var model = new TravelViewModel(HttpContextAccessor)
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

    // Action pour le traitement du formulaire de création d'un voyage
    [HttpPost]
    public IActionResult CreateTravel([Bind("DestinationId,DepartureLocation,DepartureDate,ReturnDate,Price,NumParticipants")] TravelViewModel model)
    {
        // Récupérer l'ID du client connecté depuis le contexte HTTP
        int customerId = int.Parse(HttpContext.User.Identity.Name);

        using (var travelDAL = new TravelDAL(HttpContextAccessor))
        {
            try
            {
                travelDAL.CreateTravel(customerId, model.DestinationId, model.DepartureLocation, model.DepartureDate, model.ReturnDate, model.Price, model.NumParticipants);
                return RedirectToAction("List"); // Rediriger vers la page d'accueil ou une autre page
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            // Si une erreur s'est produite, revenir à la vue avec les données saisies
            return View(model);
        }
    }

    // Action pour modifier un voyage
    [HttpPost]
    public IActionResult ModifyTravel(int id, int destinationId, string departureLocation, DateTime departureDate, DateTime returnDate, double price, int numParticipants)
    {
        // Récupérer l'ID du client connecté depuis le contexte HTTP
        int customerId = int.Parse(HttpContext.User.Identity.Name);

        using (var travelDAL = new TravelDAL(HttpContextAccessor))
        {
            try
            {
                travelDAL.ModifyTravel(id, customerId, destinationId, departureLocation, departureDate, returnDate, price, numParticipants);
                return RedirectToAction("Index", "Home"); // Rediriger vers la page d'accueil ou une autre page appropriée
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            // Récupérer la destination associée à l'ID
            using (var destinationDAL = new DestinationDAL())
            {
                var destination = destinationDAL.GetDestinationWithId(destinationId);
                // Si une erreur s'est produite, revenir à la vue avec les données saisies et la destination
                return View(new TravelViewModel(HttpContextAccessor)
                {
                    Id = id,
                    DestinationId = destinationId,
                    Destination = destination,
                    DepartureLocation = departureLocation,
                    DepartureDate = departureDate,
                    ReturnDate = returnDate,
                    Price = price,
                    NumParticipants = numParticipants
                });
            }
        }
    }
}
