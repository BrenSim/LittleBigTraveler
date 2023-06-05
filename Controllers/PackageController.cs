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
using System.Net;

public class PackageController : Controller
{
    private readonly IHttpContextAccessor HttpContextAccessor;
    private readonly TravelDAL travelDAL;
    private readonly DestinationDAL destinationDAL;
    public PackageController(
    IHttpContextAccessor httpContextAccessor,
    TravelDAL travelDAL,
    DestinationDAL destinationDAL)
    {
        HttpContextAccessor = httpContextAccessor;
        this.travelDAL = travelDAL;
        this.destinationDAL = destinationDAL;
    }

    // Action pour afficher la liste de tous les PackageTravel
    public IActionResult List()
    {
        using (var packageDAL = new PackageDAL(HttpContextAccessor))
        {
            var packageTravels = packageDAL.GetAllPackage();

            // Récupérer les détails des voyages associés et des destinations
            foreach (var packageTravel in packageTravels)
            {
                packageTravel.Travel = travelDAL.GetTravelById(packageTravel.TravelId);
                packageTravel.Travel.Destination = destinationDAL.GetDestinationWithId(packageTravel.Travel.DestinationId);
            }

            return View(packageTravels);
        }
    }

    //// Action pour afficher la liste des PackageTravel d'un client
    //public IActionResult ListCustomer()
    //{
    //    // Récupérer l'ID du client connecté depuis le contexte HTTP
    //    int customerId = int.Parse(HttpContext.User.Identity.Name);

    //    using (var packageDAL = new PackageDAL(HttpContextAccessor))
    //    {
    //        var packageTravels = packageDAL.GetCustomerPackageTravels(customerId);

    //        if (packageTravels == null || packageTravels.Count == 0)
    //        {
    //            return View("ListCustomer"); // Appelle la vue "ListCustomer.cshtml" lorsque la liste des voyages est vide
    //        }

    //        return View(packageTravels);
    //    }
    //}

    // Action pour créer un PackageTravel
    [Authorize(Roles = "Administrator, Customer")]
    public IActionResult CreatePackage(int travelId)
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

                var model = new PackageViewModel(HttpContextAccessor)
                {
                    TravelId = travelId,
                    Travel = travel,
                    Name = "",
                    Description = "",
                    Price = 300,
                    QuantityAvailable = 5,
                    Services = services,
                    SelectedServiceId = new List<int>()
                };

                // Affecter les services disponibles au modèle
                model.AvailableServices = services;

                return View(model);
            }
        }
    }

    // Action pour le traitement du formulaire de création d'un PackageTravel
    [Authorize(Roles = "Administrator, Customer")]
    [HttpPost]
    public IActionResult CreatePackage(PackageViewModel model, List<int> SelectedServiceId)
    {

        using (var packageDAL = new PackageDAL(HttpContextAccessor))
        {
            try
            {
                int packageTravelId = packageDAL.CreatePackage( model.TravelId, model.Name, model.Description, new List<Service>());

                if (model.SelectedServiceId != null)
                {
                    using (var serviceDAL = new ServiceDAL())
                    {
                        foreach (var serviceId in model.SelectedServiceId)
                        {
                            var service = serviceDAL.GetServiceWithId(serviceId);
                            serviceDAL.UpdatePackage(service, packageTravelId);
                        }
                    }
                }

                return RedirectToAction("List"); // Redirection vers la page de la liste des PackageTravel
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            // Si une erreur se produit, revenir à la vue avec les données saisies
            return View(model);
        }
    }

    // Action pour supprimer le Travel associé au PackageTravel
    private void DeleteAssociatedTravel(Package package)
    {
        if (package != null && package.Travel != null)
        {
            using (var travelDAL = new TravelDAL(HttpContextAccessor))
            {
                travelDAL.DeleteTravel(package.Travel.Id);
            }
        }
    }

    // Action pour supprimer un PackageTravel
    [Authorize(Roles = "Administrator, Customer")]
    public IActionResult DeletePackage(int id)
    {
        using (var packageDAL = new PackageDAL(HttpContextAccessor))
        {
            try
            {
                packageDAL.DeletePackage(id);
                return RedirectToAction("List"); // Rediriger vers la page de la liste des PackageTravel
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }


    //// Action pour supprimer un PackageTravel
    //[Authorize(Roles = "Administrator, Customer")]
    //public IActionResult DeletePackage(int id)
    //{
    //    using (var packageDAL = new PackageDAL(HttpContextAccessor))
    //    {
    //        try
    //        {
    //            var packageTravel = packageDAL.GetPackageById(id);
    //            DeleteAssociatedTravel(packageTravel);
    //            packageDAL.DeletePackage(id);
    //            return RedirectToAction("List"); // Rediriger vers la page de la liste des PackageTravel
    //        }
    //        catch (Exception ex)
    //        {
    //            return NotFound(ex.Message);
    //        }
    //    }
    //}

    // Action pour afficher le formulaire d'édition d'un PackageTravel
    [Authorize(Roles = "Administrator, Customer")]
    public IActionResult EditPackage(int id)
    {
        using (var packageDAL = new PackageDAL(HttpContextAccessor))
        {
            var packageTravel = packageDAL.GetPackageById(id);
            if (packageTravel == null)
            {
                return NotFound("PackageTravel non trouvé");
            }

            var travelId = packageTravel.TravelId;

            using (var travelDAL = new TravelDAL(HttpContextAccessor))
            {
                var travel = travelDAL.GetTravelById(travelId);
                var destinationId = travel.DestinationId;

                using (var destinationDAL = new DestinationDAL())
                {
                    var destination = destinationDAL.GetDestinationWithId(destinationId);
                    var services = destination.Services;

                    var model = new PackageViewModel(HttpContextAccessor)
                    {
                        Id = packageTravel.Id,
                        TravelId = travelId,
                        Travel = travel,
                        Name = packageTravel.Name,
                        Description = packageTravel.Description,
                        Price = packageTravel.Price,
                        QuantityAvailable = packageTravel.QuantityAvailable,
                        Services = services,
                        SelectedServiceId = packageTravel.ServiceForPackage.Select(s => s.Id).ToList(),
                    };

                    model.AvailableServices = services;

                    return View(model);
                }
            }
        }
    }

    // Action pour le traitement du formulaire d'édition d'un PackageTravel
    [Authorize(Roles = "Administrator, Customer")]
    [HttpPost]
    public IActionResult EditPackage(int id, PackageViewModel model, List<int> SelectedServiceId)
    {
        using (var packageDAL = new PackageDAL(HttpContextAccessor))
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

                packageDAL.UpdatePackage(id, model.TravelId, model.Name, model.Description, services);

                return RedirectToAction("List"); // Redirection vers la page de la liste des PackageTravel
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            // Si une erreur se produit, revenir à la vue avec les données saisies
            return View(model);
        }
    }

    //// Action pour le traitement du formulaire d'édition d'un PackageTravel
    //[Authorize(Roles = "Administrator, Customer")]
    //[HttpPost]
    //public IActionResult EditPackage(int id, PackageViewModel model, List<int> SelectedServiceId)
    //{

    //    using (var packageDAL = new PackageDAL(HttpContextAccessor))
    //    {
    //        try
    //        {
    //            var services = new List<Service>();

    //            if (model.SelectedServiceId != null)
    //            {
    //                using (var serviceDAL = new ServiceDAL())
    //                {
    //                    foreach (var serviceId in model.SelectedServiceId)
    //                    {
    //                        var service = serviceDAL.GetServiceWithId(serviceId);
    //                        services.Add(service);
    //                    }
    //                }
    //            }

    //            packageDAL.UpdatePackage(id, model.TravelId, model.Name, model.Description, services);

    //            return RedirectToAction("List"); // Redirection vers la page de la liste des PackageTravel
    //        }
    //        catch (Exception ex)
    //        {
    //            ModelState.AddModelError("ERREUR", ex.Message);
    //        }

    //        // Si une erreur se produit, revenir à la vue avec les données saisies
    //        return View(model);
    //    }
    //}
}