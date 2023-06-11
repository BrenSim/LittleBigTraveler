using System;
using System.Linq;
using System.Collections.Generic;
using LittleBigTraveler.Models.DataBase;
using LittleBigTraveler.Models.TravelClasses;
using LittleBigTraveler.Models.UserClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LittleBigTraveler.Models.DataBase
{
    /// <summary>
    /// Fournit des méthodes d'accès aux données pour l'entité Package.
    /// </summary>
    public class PackageDAL : IPackageDAL
    {
        private BddContext _bddContext;
        private IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="PackageDAL"/>.
        /// </summary>
        /// <param name="httpContextAccessor">L'accessoire du contexte HTTP.</param>
        public PackageDAL(IHttpContextAccessor httpContextAccessor)
        {
            _bddContext = new BddContext();
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Supprime et recrée la base de données.
        /// </summary>
        public void DeleteCreateDatabase()
        {
            _bddContext.Database.EnsureDeleted();
            _bddContext.Database.EnsureCreated();
        }

        /// <summary>
        /// Libère les ressources utilisées par l'instance <see cref="PackageDAL"/>.
        /// </summary>
        public void Dispose()
        {
            _bddContext.Dispose();
        }

        /// <summary>
        /// Récupère tous les services disponibles.
        /// </summary>
        /// <returns>Une liste des services disponibles.</returns>
        public List<Service> GetAvailableServices()
        {
            return _bddContext.Services.ToList();
        }

        /// <summary>
        /// Récupère un voyage par son ID.
        /// </summary>
        /// <param name="travelId">L'ID du voyage à récupérer.</param>
        /// <returns>Le voyage avec l'ID spécifié, ou null s'il n'est pas trouvé.</returns>
        public Travel GetTravelById(int travelId)
        {
            return _bddContext.Travels
                .Include(t => t.Destination)
                .FirstOrDefault(t => t.Id == travelId);
        }

        /// <summary>
        /// Crée un nouveau package.
        /// </summary>
        /// <param name="travelId">L'ID du voyage associé.</param>
        /// <param name="name">Le nom du package.</param>
        /// <param name="description">La description du package.</param>
        /// <param name="services">La liste des services pour le package.</param>
        /// <returns>L'ID du package créé.</returns>
        public int CreatePackage(int travelId, string name, string description, List<Service> services)
        {
            var travel = _bddContext.Travels.Include(t => t.Destination).FirstOrDefault(t => t.Id == travelId);

            if (travel != null)
            {
                Package package = new Package()
                {
                    Travel = travel,
                    Name = name,
                    Description = description,
                    Price = travel.Price,
                    ServiceForPackage = new List<Service>()
                };

                package.ServiceForPackage.AddRange(services);
                package.Price = travel.Price + services.Sum(s => s.Price);

                _bddContext.Packages.Add(package);
                _bddContext.SaveChanges();

                return package.Id;
            }
            else
            {
                throw new Exception("Client ou voyage introuvable.");
            }
        }

        /// <summary>
        /// Supprime un package.
        /// </summary>
        /// <param name="packageId">L'ID du package à supprimer.</param>
        public void DeletePackage(int packageId)
        {
            var package = _bddContext.Packages.Include(a => a.ServiceForPackage)
                                              .Include(a => a.Travel)
                                              .FirstOrDefault(a => a.Id == packageId);

            if (package != null)
            {
                var services = new List<Service>(package.ServiceForPackage);

                foreach (var service in services)
                {
                    service.PackageId = null;
                    _bddContext.Entry(service).State = EntityState.Modified;
                }

                package.ServiceForPackage.Clear();

                var travel = package.Travel;

                _bddContext.Packages.Remove(package);

                if (travel != null)
                {
                    _bddContext.Travels.Remove(travel);
                }

                _bddContext.SaveChanges();
            }
            else
            {
                throw new Exception("Package introuvable.");
            }
        }

        /// <summary>
        /// Met à jour un package.
        /// </summary>
        /// <param name="packageId">L'ID du package à mettre à jour.</param>
        /// <param name="travelId">L'ID du voyage associé.</param>
        /// <param name="name">Le nouveau nom du package.</param>
        /// <param name="description">La nouvelle description du package.</param>
        /// <param name="services">La nouvelle liste des services pour le package.</param>
        public void UpdatePackage(int packageId, int travelId, string name, string description, List<Service> services)
        {
            var package = _bddContext.Packages.Find(packageId);

            if (package != null)
            {
                var travel = _bddContext.Travels.Include(t => t.Destination)
                                                .FirstOrDefault(t => t.Id == travelId);
                if (travel != null)
                {
                    package.Travel = travel;
                    package.Name = name;
                    package.Description = description;
                    package.ServiceForPackage = new List<Service>();
                    package.ServiceForPackage.Clear();
                    package.ServiceForPackage.AddRange(services);
                    package.Price = travel.Price + services.Sum(s => s.Price);

                    _bddContext.Packages.Update(package);
                    _bddContext.SaveChanges();
                }
                else
                {
                    throw new Exception("Voyage invalide.");
                }
            }
            else
            {
                throw new Exception("Package invalide.");
            }
        }

        /// <summary>
        /// Crée un package surprise.
        /// </summary>
        /// <returns>Le package surprise créé.</returns>
        public Package CreateSurprisePackage()
        {
            var random = new Random();

            var destinations = _bddContext.Destinations.ToList();

            if (destinations.Count == 0)
            {
                throw new Exception("Il n'y a pas de destinations disponibles pour créer un voyage surprise.");
            }

            var randomDestination = destinations[random.Next(destinations.Count)];

            var packageName = "Votre voyage surprise !";

            var destinationServices = _bddContext.Services.Where(s => s.DestinationId == randomDestination.Id).ToList();

            if (destinationServices.Count < 2)
            {
                throw new Exception("Il y a moins de 2 services disponibles pour cette destination.");
            }

            var numServices = random.Next(2, Math.Min(destinationServices.Count, 4) + 1);

            var selectedServices = destinationServices.OrderBy(x => random.Next()).Take(numServices).ToList();

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

            var packageId = CreatePackage(travel.Id, packageName, "Votre voyage surprise généré avec soin par notre algorithme", selectedServices);

            var createdPackage = _bddContext.Packages
                .Include(p => p.ServiceForPackage)
                .FirstOrDefault(p => p.Id == packageId);

            return createdPackage;
        }

        /// <summary>
        /// Récupère toutes les données de type "PackageTravel".
        /// </summary>
        /// <returns>Une liste de tous les packages.</returns>
        public List<Package> GetAllPackage()
        {
            return _bddContext.Packages.ToList();
        }

        /// <summary>
        /// Récupère un package par son ID.
        /// </summary>
        /// <param name="id">L'ID du package à récupérer.</param>
        /// <returns>Le package avec l'ID spécifié, ou null s'il n'est pas trouvé.</returns>
        public Package GetPackageById(int id)
        {
            return _bddContext.Packages
                .Include(a => a.Travel)
                    .ThenInclude(t => t.Destination)
                .Include(a => a.ServiceForPackage)
                .FirstOrDefault(a => a.Id == id);
        }

        /// <summary>
        /// Effectue une recherche multicritère de packages.
        /// </summary>
        /// <param name="destination">La destination recherchée.</param>
        /// <param name="departureMonth">Le mois de départ recherché.</param>
        /// <param name="minPrice">Le prix minimum recherché.</param>
        /// <param name="maxPrice">Le prix maximum recherché.</param>
        /// <returns>Une liste des packages correspondant aux critères de recherche.</returns>
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

