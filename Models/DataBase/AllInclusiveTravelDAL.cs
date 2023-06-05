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
    public class AllInclusiveTravelDAL : IAllInclusiveTravelDAL
    {
        private BddContext _bddContext;
        private IHttpContextAccessor _httpContextAccessor;

        public AllInclusiveTravelDAL(IHttpContextAccessor httpContextAccessor)
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

        // Redéfinition de GetTravelById dans ce DAL pour faciliter la récupération des "Travel" dans le controller
        public Travel GetTravelById(int travelId)
        {
            return _bddContext.Travels
                .Include(t => t.Customer)
                    .ThenInclude(c => c.User)
                .Include(t => t.Destination)
                .FirstOrDefault(t => t.Id == travelId);
        }


        // Création d'un AllInclusiveTravel
        public int CreateAllInclusiveTravel(int customerId, int travelId, string name, string description, List<Service> services)
        {
            var customer = _bddContext.Customers.Include(c => c.User).FirstOrDefault(c => c.Id == customerId);
            var travel = _bddContext.Travels.Include(t => t.Customer).ThenInclude(c => c.User).Include(t => t.Destination).FirstOrDefault(t => t.Id == travelId);

            if (customer != null && travel != null)
            {
                AllInclusiveTravel allInclusiveTravel = new AllInclusiveTravel()
                {
                    Customer = customer,
                    Travel = travel,
                    Name = name,
                    Description = description,
                    Price = travel.Price, // Par défaut, le prix est initialisé avec le prix du Travel
                    ServiceForPackage = new List<Service>()
                };

                // Ajout des services sélectionnés au package AllInclusiveTravel
                allInclusiveTravel.ServiceForPackage.AddRange(services);
                // Mise à jour du prix total du package
                allInclusiveTravel.Price += services.Sum(s => s.Price);

                _bddContext.AllInclusiveTravels.Add(allInclusiveTravel);
                _bddContext.SaveChanges();

                return allInclusiveTravel.Id;
            }
            else
            {
                throw new Exception("Client ou Voyage incorrect");
            }
        }

        // Suppression d'un AllInclusiveTravel par ID (et de son Travel associé)
        public void DeleteAllInclusiveTravel(int id)
        {
            var allInclusiveTravel = _bddContext.AllInclusiveTravels
                .Include(a => a.ServiceForPackage)
                .FirstOrDefault(a => a.Id == id);

            if (allInclusiveTravel != null)
            {
                var travel = allInclusiveTravel.Travel; 

                foreach (var service in allInclusiveTravel.ServiceForPackage)
                {
                    _bddContext.Entry(service).State = EntityState.Unchanged;
                }

                _bddContext.AllInclusiveTravels.Remove(allInclusiveTravel);
                _bddContext.Travels.Remove(travel); 
                _bddContext.SaveChanges();
            }
        }


        // Modification d'un AllInclusiveTravel par ID
        public void UpdateAllInclusiveTravel(int id, int customerId, int travelId, string name, string description, List<Service> services)
        {
            var allInclusiveTravel = _bddContext.AllInclusiveTravels.Include(a => a.Customer).ThenInclude(c => c.User).Include(a => a.Travel).FirstOrDefault(a => a.Id == id);
            if (allInclusiveTravel == null)
            {
                throw new Exception("AllInclusiveTravel non trouvé");
            }

            var customer = _bddContext.Customers.Include(c => c.User).FirstOrDefault(c => c.Id == customerId);
            var travel = _bddContext.Travels.Include(t => t.Customer).ThenInclude(c => c.User).Include(t => t.Destination).FirstOrDefault(t => t.Id == travelId);
            if (customer == null || travel == null)
            {
                throw new Exception("Client ou Voyage incorrect");
            }

            allInclusiveTravel.Customer = customer;
            allInclusiveTravel.Travel = travel;
            allInclusiveTravel.Name = name;
            allInclusiveTravel.Description = description;

            // Mettre à jour les services
            allInclusiveTravel.ServiceForPackage.Clear();
            allInclusiveTravel.ServiceForPackage.AddRange(services);

            // Mettre à jour le prix
            allInclusiveTravel.Price = travel.Price + services.Sum(s => s.Price);

            _bddContext.AllInclusiveTravels.Update(allInclusiveTravel);
            _bddContext.SaveChanges();
        }


        // Récupération de toutes les données "AllInclusiveTravel"
        public List<AllInclusiveTravel> GetAllInclusiveTravel()
        {
            return _bddContext.AllInclusiveTravels.ToList();
        }

        // Récupération d'un AllInclusiveTravel par ID
        public AllInclusiveTravel GetAllInclusiveTravelById(int id)
        {
            return _bddContext.AllInclusiveTravels
                .Include(a => a.Customer)
                    .ThenInclude(c => c.User)
                .Include(a => a.Travel)
                .Include(a => a.ServiceForPackage) 
                .FirstOrDefault(a => a.Id == id);
        }

        // Récupération des AllInclusiveTravel d'un client par son ID
        public List<AllInclusiveTravel> GetCustomerAllInclusiveTravels(int customerId)
        {

            // Récupération de l'ID du client connecté à partir du contexte HTTP
            customerId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
            return _bddContext.AllInclusiveTravels
                .Include(a => a.Customer)
                    .ThenInclude(c => c.User)
                .Include(a => a.Travel)
                .Where(a => a.Customer.Id == customerId)
                .ToList();
        }

        

    }
}




