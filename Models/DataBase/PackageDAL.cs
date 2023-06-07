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
    }
}



//// Suppression d'un PackageTravel par ID (et de son Travel associé)
//public void DeletePackage(int id)
//{
//    var package = _bddContext.Packages
//        .Include(a => a.ServiceForPackage)
//        .FirstOrDefault(a => a.Id == id);

//    if (package != null)
//    {
//        var travel = package.Travel;

//        foreach (var service in package.ServiceForPackage)
//        {
//            _bddContext.Entry(service).State = EntityState.Unchanged;
//        }

//        _bddContext.Packages.Remove(package);
//        _bddContext.Travels.Remove(travel);
//        _bddContext.SaveChanges();
//    }
//    else
//    {
//        throw new Exception("PackageTravel non trouvé");
//    }
//}


//// Modification d'un PackageTravel par ID
//public void UpdatePackage(int id, int travelId, string name, string description, List<Service> services)
//{
//    var package = _bddContext.Packages
//        .Include(a => a.ServiceForPackage)
//        .FirstOrDefault(a => a.Id == id);

//    if (package == null)
//    {
//        throw new Exception("PackageTravel non trouvé");
//    }

//    var travel = _bddContext.Travels.Include(t => t.Destination).FirstOrDefault(t => t.Id == travelId);
//    if (travel == null)
//    {
//        throw new Exception("Voyage incorrect");
//    }

//    package.Travel = travel;
//    package.Name = name;
//    package.Description = description;

//    // Mettre à jour les services
//    package.ServiceForPackage.Clear();
//    package.ServiceForPackage.AddRange(services);

//    // Mettre à jour le prix
//    package.Price = travel.Price + services.Sum(s => s.Price);

//    _bddContext.Packages.Update(package);
//    _bddContext.SaveChanges();
//}

// Récupération des PackageTravel d'un client par son ID
//public List<Package> GetCustomerPackageTravels(int customerId)
//{

//    // Récupération de l'ID du client connecté à partir du contexte HTTP
//    customerId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
//    return _bddContext.PackageTravels
//        .Include(a => a.Customer)
//            .ThenInclude(c => c.User)
//        .Include(a => a.Travel)
//        .Where(a => a.Customer.Id == customerId)
//        .ToList();
//}

