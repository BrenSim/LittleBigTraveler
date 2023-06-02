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

public class AllInclusiveTravelController : Controller
{
    private readonly IHttpContextAccessor HttpContextAccessor;

    public AllInclusiveTravelController(IHttpContextAccessor httpContextAccessor)
    {
        HttpContextAccessor = httpContextAccessor;
    }

    // Action pour afficher la liste des AllInclusiveTravel d'un client
    public IActionResult List()
    {
        // Récupérer l'ID du client connecté depuis le contexte HTTP
        int customerId = int.Parse(HttpContext.User.Identity.Name);

        using (var allInclusiveTravelDAL = new AllInclusiveTravelDAL(HttpContextAccessor))
        {
            var allInclusiveTravels = allInclusiveTravelDAL.GetCustomerAllInclusiveTravels(customerId);

            return View(allInclusiveTravels);
        }
    }

    // Action pour créer un AllInclusiveTravel
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
    [HttpPost]
    public IActionResult CreateAllInclusiveTravel([Bind("TravelId,Name,Description,SelectedServiceId")] AllInclusiveTravelViewModel model)
    {
        // Récupéreration de l'ID du client connecté depuis le contexte HTTP
        int customerId = int.Parse(HttpContext.User.Identity.Name);

        using (var allInclusiveTravelDAL = new AllInclusiveTravelDAL(HttpContextAccessor))
        {
            try
            {
                var travelId = model.TravelId;
                var travel = allInclusiveTravelDAL.GetTravelById(travelId);
                if (travel == null)
                {
                    return NotFound("le travel n'est pas valide FORM");
                }

                // Création de voyage tout compris sans services.
                int allInclusiveTravelId = allInclusiveTravelDAL.CreateAllInclusiveTravel(customerId, travelId, model.Name, model.Description, new List<Service>());

                if (model.SelectedServiceId != null && model.SelectedServiceId.Any())
                {
                    using (var serviceDAL = new ServiceDAL())
                    {
                        foreach (var serviceId in model.SelectedServiceId)
                        {
                            var service = serviceDAL.GetServiceWithId(serviceId);
                                        allInclusiveTravelDAL.AddServiceToAllInclusiveTravel(allInclusiveTravelId, service);
                        }
                    }
                }

                return RedirectToAction("List"); // Redirection vers la page de la liste des AllInclusiveTravel
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            // Si une erreur revenir à la vue avec les données saisies
            return View(model);
        }
    }



    //[HttpPost]
    //public IActionResult CreateAllInclusiveTravel([Bind("TravelId,Name,Description,SelectedServiceIds")] AllInclusiveTravelViewModel model)
    //{
    //    // Récupérer l'ID du client connecté depuis le contexte HTTP
    //    int customerId = int.Parse(HttpContext.User.Identity.Name);

    //    using (var allInclusiveTravelDAL = new AllInclusiveTravelDAL(HttpContextAccessor))
    //    {
    //        try
    //        {
    //            var travelId = model.TravelId;
    //            var travel = allInclusiveTravelDAL.GetTravelById(travelId);
    //            if (travel == null)
    //            {
    //                return NotFound("le travel n'est pas valide FORM");
    //            }

    //            var services = new List<Service>();
    //            if (model.SelectedServiceId != null && model.SelectedServiceId.Any())
    //            {
    //                using (var serviceDAL = new ServiceDAL())
    //                {
    //                    services = serviceDAL.GetServiceWithIds(model.SelectedServiceId);
    //                }
    //            }

    //            allInclusiveTravelDAL.CreateAllInclusiveTravel(customerId, travelId, model.Name, model.Description, services);
    //            return RedirectToAction("List"); // Rediriger vers la page de la liste des AllInclusiveTravel
    //        }
    //        catch (Exception ex)
    //        {
    //            ModelState.AddModelError("", ex.Message);
    //        }

    //        // Si une erreur s'est produite, revenir à la vue avec les données saisies
    //        return View(model);
    //    }
    //}

    // Action pour supprimer un AllInclusiveTravel
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

    // Action pour modifier un AllInclusiveTravel
    public IActionResult ModifyAllInclusiveTravel(int id)
    {
        using (var allInclusiveTravelDAL = new AllInclusiveTravelDAL(HttpContextAccessor))
        {
            var allInclusiveTravel = allInclusiveTravelDAL.GetAllInclusiveTravelById(id);
            if (allInclusiveTravel == null)
            {
                return NotFound("AllInclusiveTravel incorrect");
            }

            var model = new AllInclusiveTravelViewModel(HttpContextAccessor)
            {
                Id = allInclusiveTravel.Id,
                Name = allInclusiveTravel.Name,
                Description = allInclusiveTravel.Description,
                TravelId = allInclusiveTravel.TravelId,
                SelectedServiceId = allInclusiveTravel.ServiceForPackage.Select(s => s.Id).ToList(),
                Services = allInclusiveTravel.Travel.Destination.Services
            };

            return View(model);
        }
    }

    // Action pour le traitement du formulaire de modification d'un AllInclusiveTravel
    [HttpPost]
    public IActionResult ModifyAllInclusiveTravel([Bind("Id,Name,Description,TravelId,SelectedServiceIds")] AllInclusiveTravelViewModel model)
    {
        using (var allInclusiveTravelDAL = new AllInclusiveTravelDAL(HttpContextAccessor))
        {
            try
            {
                var allInclusiveTravel = allInclusiveTravelDAL.GetAllInclusiveTravelById(model.Id);
                if (allInclusiveTravel == null)
                {
                    return NotFound("AllInclusiveTravel incorrect");
                }

                var services = new List<Service>();
                if (model.SelectedServiceId != null && model.SelectedServiceId.Any())
                {
                    using (var serviceDAL = new ServiceDAL())
                    {
                        services = serviceDAL.GetServiceWithIds(model.SelectedServiceId);
                    }
                }

                allInclusiveTravelDAL.ModifyAllInclusiveTravel(model.Id, model.Name, model.Description, services);
                return RedirectToAction("List"); // Rediriger vers la page de la liste des AllInclusiveTravel
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            // Si une erreur s'est produite, revenir à la vue avec les données saisies
            return View(model);
        }
    }
}


