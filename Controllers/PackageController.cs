﻿using System;
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

    /// <summary>
    /// Action pour afficher la liste de tous les Package.
    /// </summary>
    /// <returns>Vue contenant la liste des Package.</returns>
    [AllowAnonymous]
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

    /// <summary>
    /// Action pour créer un Package.
    /// </summary>
    /// <param name="travelId">ID du voyage associé au Package.</param>
    /// <returns>Vue contenant le formulaire de création d'un Package.</returns>
    [Authorize]
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

    /// <summary>
    /// Action pour le traitement du formulaire de création d'un Package.
    /// </summary>
    /// <param name="model">Modèle contenant les informations du Package.</param>
    /// <param name="SelectedServiceId">Liste des ID des services sélectionnés.</param>
    /// <returns>Redirige vers la page de la liste des PackageTravel en cas de succès, sinon réaffiche le formulaire avec les erreurs.</returns>
    [Authorize]
    [HttpPost]
    public IActionResult CreatePackage(PackageViewModel model, List<int> SelectedServiceId)
    {

        using (var packageDAL = new PackageDAL(HttpContextAccessor))
        {
            try
            {
                int packageTravelId = packageDAL.CreatePackage(model.TravelId, model.Name, model.Description, new List<Service>());

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
                return RedirectToAction("Details", new { id = packageTravelId }); // Redirection vers la page de détails du package créé

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            // Si une erreur se produit, revenir à la vue avec les données saisies
            return View(model);
        }
    }

    public IActionResult Details(int id)
    {
        using (var packageDAL = new PackageDAL(HttpContextAccessor))
        {
            var package = packageDAL.GetPackageById(id);

            if (package == null)
            {
                return View(null);
            }

            return View(package);
        }
    }


    /// <summary>
    /// Méthode privée pour supprimer le Travel associé au Package.
    /// </summary>
    /// <param name="package">Package à supprimer.</param>
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

    /// <summary>
    /// Action pour supprimer un Package.
    /// </summary>
    /// <param name="id">ID du Package à supprimer.</param>
    /// <returns>Redirige vers la page de la liste des Package en cas de succès, sinon renvoie un code d'erreur.</returns>
    [Authorize]
    public IActionResult DeletePackage(int id)
    {
        using (var packageDAL = new PackageDAL(HttpContextAccessor))
        {
            try
            {
                packageDAL.DeletePackage(id);
                return RedirectToAction("List"); // Rediriger vers la page de la liste des Package
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

    /// <summary>
    /// Action pour afficher le formulaire d'édition d'un Package.
    /// </summary>
    /// <param name="id">ID du Package à éditer.</param>
    /// <returns>Vue contenant le formulaire d'édition d'un Package.</returns>
    [Authorize]
    public IActionResult EditPackage(int id)
    {
        using (var packageDAL = new PackageDAL(HttpContextAccessor))
        {
            var package = packageDAL.GetPackageById(id);
            if (package == null)
            {
                return NotFound("Package non trouvé");
            }

            var travelId = package.TravelId;

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
                        Id = package.Id,
                        TravelId = travelId,
                        Travel = travel,
                        Name = package.Name,
                        Description = package.Description,
                        Price = package.Price,
                        QuantityAvailable = package.QuantityAvailable,
                        Services = services,
                        SelectedServiceId = package.ServiceForPackage.Select(s => s.Id).ToList(),
                    };

                    model.AvailableServices = services;

                    return View(model);
                }
            }
        }
    }

    /// <summary>
    /// Action pour le traitement du formulaire d'édition d'un Package.
    /// </summary>
    /// <param name="id">ID du Package à éditer.</param>
    /// <param name="model">Modèle contenant les informations du Package.</param>
    /// <param name="SelectedServiceId">Liste des ID des services sélectionnés.</param>
    /// <returns>Redirige vers la page de la liste des Package en cas de succès, sinon réaffiche le formulaire avec les erreurs.</returns>
    [Authorize]
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

                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View(model);
        }
    }

    /// <summary>
    /// Action pour créer un PackageSurprise.
    /// </summary>
    /// <returns>Vue contenant le PackageSurprise créé.</returns>
    [Authorize]
    public IActionResult CreateSurprisePackage()
    {
        var packageDAL = new PackageDAL(HttpContextAccessor);

        var package = packageDAL.CreateSurprisePackage();

        if (package == null)
        {
            return View("Erreur");
        }

        // Renvoyer la vue "PackageSurprise" avec le package créé
        return View("PackageSurprise", package);
    }


    /// <summary>
    /// Recherche des packages de voyage en fonction des critères fournis.
    /// </summary>
    /// <param name="destination">Le nom de la destination recherchée.</param>
    /// <param name="departureMonth">Le mois de départ recherché.</param>
    /// <param name="minPrice">Le prix minimum recherché.</param>
    /// <param name="maxPrice">Le prix maximum recherché.</param>
    /// <returns>Une vue 'List' contenant la liste des packages de voyage qui correspondent aux critères de recherche.</returns>

    [AllowAnonymous]
    public IActionResult SearchPackages(string destination, int? departureMonth, double? minPrice, double? maxPrice)
    {
        using (var packageDAL = new PackageDAL(HttpContextAccessor))
        {
            // Utiliser la méthode SearchPackages pour récupérer les packages correspondants
            var packages = packageDAL.SearchPackages(destination, departureMonth, minPrice, maxPrice);

            // Récupérer les détails des voyages associés et des destinations
            foreach (var package in packages)
            {
                package.Travel = travelDAL.GetTravelById(package.TravelId);
                package.Travel.Destination = destinationDAL.GetDestinationWithId(package.Travel.DestinationId);
            }

            return View("List", packages); // Retourner la vue "List" avec les packages recherchés
        }
    }

}


