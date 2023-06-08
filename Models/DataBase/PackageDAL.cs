using System;
using System.Linq;
using System.Collections.Generic;
using LittleBigTraveler.Models.DataBase;
using LittleBigTraveler.Models.TravelClasses;
using LittleBigTraveler.Models.UserClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace LittleBigTraveler.Models.DataBase
{
    public class PackageDAL : IPackageDAL
    {
        private BddContext _bddContext;
        private IHttpContextAccessor _httpContextAccessor;
        public PackageDAL(IHttpContextAccessor httpContextAccessor)
        {
            _bddContext = new BddContext();
            _httpContextAccessor = httpContextAccessor;
        }

        // Suppression/Création de la base de données (méthode appelée dans BddContext)
        public void DeleteCreateDatabase()
        {
            _bddContext.Database.EnsureDeleted();
            _bddContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

        public List<Service> GetAvailableServices()
        {
            return _bddContext.Services.ToList();
        }

        // Redéfinition de GetTravelById dans ce DAL pour faciliter la récupération des "Travel" dans le controller
        public Travel GetTravelById(int travelId)
        {
            return _bddContext.Travels
                .Include(t => t.Destination)
                .FirstOrDefault(t => t.Id == travelId);
        }


        // Création d'un Package
        public int CreatePackage(int travelId, string name, string description, List<Service> services)
        {
            //var customer = _bddContext.Customers.Include(c => c.User).FirstOrDefault(c => c.Id == customerId);
            var travel = _bddContext.Travels.Include(t => t.Destination).FirstOrDefault(t => t.Id == travelId);

            if (travel != null)
            {
                Package package = new Package()
                {
                    Travel = travel,
                    Name = name,
                    Description = description,
                    Price = travel.Price, // Par défaut, le prix est initialisé avec le prix du Travel
                    ServiceForPackage = new List<Service>()
                };

                // Ajout des services sélectionnés au package PackageTravel
                package.ServiceForPackage.AddRange(services);
                // Update price
                package.Price = travel.Price + services.Sum(s => s.Price);

                _bddContext.Packages.Add(package);
                _bddContext.SaveChanges();

                return package.Id;
            }
            else
            {
                throw new Exception("Client ou Voyage incorrect");
            }
        }

        // Suppression d'un Package
        public void DeletePackage(int packageId)
        {
            var package = _bddContext.Packages.Include(a => a.ServiceForPackage)
                                              .Include(a => a.Travel)  // Inclure le Travel associé
                                              .FirstOrDefault(a => a.Id == packageId);

            if (package != null)
            {
                // Créer une copie de la collection de services
                var services = new List<Service>(package.ServiceForPackage);

                // Supprimer les services associés au package sans supprimer les originaux 
                foreach (var service in services)
                {
                    service.PackageId = null;
                    _bddContext.Entry(service).State = EntityState.Modified;
                    // Ou, pour supprimer le service de la base de données:
                    // _bddContext.Services.Remove(service);
                }

                package.ServiceForPackage.Clear();

                var travel = package.Travel; // Récupérer le Travel associé

                _bddContext.Packages.Remove(package);

                if (travel != null) // Supprimer le Travel, si il existe
                {
                    _bddContext.Travels.Remove(travel);
                }

                _bddContext.SaveChanges();
            }
            else
            {
                throw new Exception("Package not found");
            }
        }

        // Update Package
        public void UpdatePackage(int packageId, int travelId, string name, string description, List<Service> services)
        {
            var package = _bddContext.Packages.Find(packageId);

            if (package != null)
            {
                var travel = _bddContext.Travels.Include(t => t.Destination)
                                                .FirstOrDefault(t => t.Id == travelId);
                if (travel != null)
                {
                    // Update package 
                    package.Travel = travel;
                    package.Name = name;
                    package.Description = description;

                    // Update services
                    package.ServiceForPackage = new List<Service>();
                    package.ServiceForPackage.Clear();
                    package.ServiceForPackage.AddRange(services);

                    // Update price
                    package.Price = travel.Price + services.Sum(s => s.Price);

                    _bddContext.Packages.Update(package);
                    _bddContext.SaveChanges();
                }
                else
                {
                    throw new Exception("Voyage incorrect");
                }
            }
            else
            {
                throw new Exception("Package incorrect");
            }
        }

        // Creation d'un voyage surprise
        public Package CreateSurprisePackage()
        {
            var random = new Random();

            // Récupérer toutes les destinations disponibles
            var destinations = _bddContext.Destinations.ToList();

            if (destinations.Count == 0)
            {
                throw new Exception("Il n'y a pas de destinations disponibles pour créer un voyage surprise.");
            }

            // Sélectionner une destination aléatoire
            var randomDestination = destinations[random.Next(destinations.Count)];

            // Générer un nom aléatoire pour le package surprise
            var packageName = "Votre voyage surprise!";

            // Récupérer les services disponibles de la destination
            var destinationServices = _bddContext.Services.Where(s => s.DestinationId == randomDestination.Id).ToList();

            // Si moins de 2 services sont disponibles, tous les utiliser
            if (destinationServices.Count < 2)
            {
                throw new Exception("Il y a moins de 2 services disponibles pour cette destination.");
            }

            // Sélectionner un nombre aléatoire de services à associer au package, entre 2 et 4
            var numServices = random.Next(2, Math.Min(destinationServices.Count, 4) + 1);

            // Sélectionner de manière aléatoire les services à associer au package
            var selectedServices = destinationServices.OrderBy(x => random.Next()).Take(numServices).ToList();

            // Créer le voyage avec la destination aléatoire
            var travel = new Travel
            {
                DestinationId = randomDestination.Id,
                DepartureLocation = "Départ de votre choix",
                DepartureDate = DateTime.Now.AddDays(12),
                ReturnDate = DateTime.Now.AddDays(16),
                Price = 750,
                NumParticipants = 1
            };
            _bddContext.Travels.Add(travel);
            _bddContext.SaveChanges();

            // Créer le package avec le voyage aléatoire et les services sélectionnés
            var packageId = CreatePackage(travel.Id, packageName, "Votre voyage surprise généré avec soin par notre algorithme ", selectedServices);

            // Récupérer le package de la base de données
            var createdPackage = _bddContext.Packages
                .Include(p => p.ServiceForPackage)
                .FirstOrDefault(p => p.Id == packageId);

            return createdPackage;
        }



        // Récupération de toutes les données "PackageTravel"
        public List<Package> GetAllPackage()
        {
            return _bddContext.Packages.ToList();
        }

        // Récupération d'un PackageTravel par ID
        public Package GetPackageById(int id)
        {
            return _bddContext.Packages
                .Include(a => a.Travel)
                .Include(a => a.ServiceForPackage)
                .FirstOrDefault(a => a.Id == id);
        }

        public List<Package> SearchPackages(string destination, int? departureMonth, double? minPrice, double? maxPrice)
        {
            IQueryable<Package> recherche = _bddContext.Packages
                .Include(a => a.Travel)
                .Include(a => a.ServiceForPackage);

            if (!string.IsNullOrEmpty(destination))
            {
                recherche = recherche.Where(a => a.Travel.Destination.City.Contains(destination));
            }

            if (departureMonth.HasValue)
            {
                recherche = recherche.Where(a => a.Travel.DepartureDate.Month == departureMonth.Value);
            }

            if (minPrice.HasValue)
            {
                recherche = recherche.Where(a => a.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                recherche = recherche.Where(a => a.Price <= maxPrice.Value);
            }

            return recherche.ToList();
        }

    }
}




