using System;
using System.Linq;
using System.Collections.Generic;
using LittleBigTraveler.Models.DataBase;
using LittleBigTraveler.Models.TravelClasses;
using LittleBigTraveler.Models.UserClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LittleBigTraveler.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Data;

public class AllInclusiveTravelController : Controller
{
    private readonly IHttpContextAccessor HttpContextAccessor;

    public AllInclusiveTravelController(IHttpContextAccessor httpContextAccessor)
    {
        HttpContextAccessor = httpContextAccessor;
    }

    // Action pour afficher la liste de tout les AllInclusiveTravel
    public IActionResult List()
    {
        using (var allInclusiveTravelDAL = new AllInclusiveTravelDAL(HttpContextAccessor))
        {
            var allInclusiveTravels = allInclusiveTravelDAL.GetAllInclusiveTravel();
            return View(allInclusiveTravels);
        }
    }

    // Action pour afficher la liste des AllInclusiveTravel d'un client
    public IActionResult ListCustomer()
    {
        // Récupérer l'ID du client connecté depuis le contexte HTTP
        int customerId = int.Parse(HttpContext.User.Identity.Name);

        using (var allInclusiveTravelDAL = new AllInclusiveTravelDAL(HttpContextAccessor))
        {
            var allInclusiveTravels = allInclusiveTravelDAL.GetCustomerAllInclusiveTravels(customerId);

            if (allInclusiveTravels == null || allInclusiveTravels.Count == 0)
            {
                return View("ListCustomer"); // Appelle la vue "List.cshtml" lorsque la liste des voyages est vide
            }

            return View(allInclusiveTravels);
        }
    }


    // Action pour créer un AllInclusiveTravel
    [Authorize(Roles = "Administrator")]
    public IActionResult CreateAllInclusiveTravel(int travelId)
    {
        using (var travelDAL = new TravelDAL(HttpContextAccessor))
        {
            var travel = travelDAL.GetTravelById(travelId);
            if (travel == null)
            {
                return NotFound("Le voyage n'est pas valide");
            }

            var destinationId = travel.DestinationId; // Accéder à la propriété DestinationId du voyage

            using (var destinationDAL = new DestinationDAL())
            {
                var destination = destinationDAL.GetDestinationWithId(destinationId);
                if (destination == null)
                {
                    return NotFound("Destination non valide");
                }

                var services = destination.Services;

                var model = new AllInclusiveTravelViewModel(HttpContextAccessor)
                {
                    TravelId = travelId,
                    Travel = travel,
                    Name = "",
                    Description = "",
                    Services = services,
                    SelectedServiceId = new List<int>()
                };

                // Affecter les services disponibles au modèle
                model.AvailableServices = services;

                return View(model);
            }
        }
    }

    // Action pour le traitement du formulaire de création d'un AllInclusiveTravel
    [Authorize(Roles = "Administrator")]
    [HttpPost]
    public IActionResult CreateAllInclusiveTravel(AllInclusiveTravelViewModel model, List<int> SelectedServiceId)
    {
        // Récupération de l'ID du client connecté depuis le contexte HTTP
        int customerId = int.Parse(HttpContext.User.Identity.Name);

        using (var allInclusiveTravelDAL = new AllInclusiveTravelDAL(HttpContextAccessor))
        {
            try
            {
                int allInclusiveTravelId = allInclusiveTravelDAL.CreateAllInclusiveTravel(customerId, model.TravelId, model.Name, model.Description, new List<Service>());

                if (model.SelectedServiceId != null)
                {
                    using (var serviceDAL = new ServiceDAL())
                    {
                        foreach (var serviceId in model.SelectedServiceId)
                        {
                            var service = serviceDAL.GetServiceWithId(serviceId);
                            serviceDAL.UpdateAllInclusiveTravel(service, allInclusiveTravelId);
                        }
                    }
                }

                return RedirectToAction("List"); // Redirection vers la page de la liste des AllInclusiveTravel
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            // Si une erreur se produit, revenir à la vue avec les données saisies
            return View(model);
        }
    }

    // Action pour supprimer un AllInclusiveTravel
    [Authorize(Roles = "Administrator")]
    public IActionResult DeleteAllInclusiveTravel(int id)
    {
        using (var allInclusiveTravelDAL = new AllInclusiveTravelDAL(HttpContextAccessor))
        {
            try
            {
                allInclusiveTravelDAL.DeleteAllInclusiveTravel(id);
                return RedirectToAction("List"); // Rediriger vers la page de la liste des AllInclusiveTravel
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

    // Action pour afficher le formulaire d'édition d'un AllInclusiveTravel
    [Authorize(Roles = "Administrator")]
    public IActionResult EditAllInclusiveTravel(int id)
    {
        using (var allInclusiveTravelDAL = new AllInclusiveTravelDAL(HttpContextAccessor))
        {
            var allInclusiveTravel = allInclusiveTravelDAL.GetAllInclusiveTravelById(id);
            if (allInclusiveTravel == null)
            {
                return NotFound("AllInclusiveTravel non trouvé");
            }

            var travelId = allInclusiveTravel.TravelId;

            using (var travelDAL = new TravelDAL(HttpContextAccessor))
            {
                var travel = travelDAL.GetTravelById(travelId);
                var destinationId = travel.DestinationId;

                using (var destinationDAL = new DestinationDAL())
                {
                    var destination = destinationDAL.GetDestinationWithId(destinationId);
                    var services = destination.Services;

                    var model = new AllInclusiveTravelViewModel(HttpContextAccessor)
                    {
                        TravelId = travelId,
                        Travel = travel,
                        Name = allInclusiveTravel.Name,
                        Description = allInclusiveTravel.Description,
                        Services = services,
                        SelectedServiceId = allInclusiveTravel.ServiceForPackage.Select(s => s.Id).ToList(),
                    };

                    model.AvailableServices = services;

                    return View(model);
                }
            }
        }
    }

    // Action pour le traitement du formulaire d'édition d'un AllInclusiveTravel
    [Authorize(Roles = "Administrator")]
    [HttpPost]
    public IActionResult EditAllInclusiveTravel(int id, AllInclusiveTravelViewModel model, List<int> SelectedServiceId)
    {
        // Récupération de l'ID du client connecté depuis le contexte HTTP
        int customerId = int.Parse(HttpContext.User.Identity.Name);

        using (var allInclusiveTravelDAL = new AllInclusiveTravelDAL(HttpContextAccessor))
        {
            try
            {
                var services = new List<Service>();

                if (model.SelectedServiceId != null)
                {
                    using (var serviceDAL = new ServiceDAL())
                    {
                        foreach (var serviceId in model.SelectedServiceId)
                        {
                            var service = serviceDAL.GetServiceWithId(serviceId);
                            services.Add(service);
                        }
                    }
                }

                allInclusiveTravelDAL.UpdateAllInclusiveTravel(id, customerId, model.TravelId, model.Name, model.Description, services);

                return RedirectToAction("List"); // Redirection vers la page de la liste des AllInclusiveTravel
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            // Si une erreur se produit, revenir à la vue avec les données saisies
            return View(model);
        }
    }
}


