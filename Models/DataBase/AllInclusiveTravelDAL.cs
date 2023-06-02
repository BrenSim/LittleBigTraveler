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
            //var travel = _bddContext.Travels.FirstOrDefault(t => t.Id == travelId);
            var travel = _bddContext.Travels
                .Include(t => t.Customer)
                    .ThenInclude(c => c.User)
                .Include(t => t.Destination)
                .FirstOrDefault(t => t.Id == travelId);


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

        // Suppression d'un AllInclusiveTravel par ID
        public void DeleteAllInclusiveTravel(int id)
        {
            var allInclusiveTravel = _bddContext.AllInclusiveTravels.FirstOrDefault(a => a.Id == id);
            if (allInclusiveTravel != null)
            {
                _bddContext.AllInclusiveTravels.Remove(allInclusiveTravel);
                _bddContext.SaveChanges();
            }
        }

        // Modification d'un AllInclusiveTravel par ID
        public void ModifyAllInclusiveTravel(int id, string name, string description, List<Service> services)
        {
            var allInclusiveTravel = _bddContext.AllInclusiveTravels.FirstOrDefault(a => a.Id == id);
            if (allInclusiveTravel != null)
            {
                allInclusiveTravel.Name = name;
                allInclusiveTravel.Description = description;

                // Supprimer tous les services existants du package
                allInclusiveTravel.ServiceForPackage.Clear();
                // Ajouter les nouveaux services sélectionnés au package AllInclusiveTravel
                allInclusiveTravel.ServiceForPackage.AddRange(services);
                // Mise à jour du prix total du package
                allInclusiveTravel.Price = allInclusiveTravel.Travel.Price + services.Sum(s => s.Price);

                _bddContext.SaveChanges();
            }
            else
            {
                throw new Exception("Voyage All Inclusive non valide.");
            }
        }

        // Récupération d'un AllInclusiveTravel par ID
        public AllInclusiveTravel GetAllInclusiveTravelById(int id)
        {
            return _bddContext.AllInclusiveTravels
                .Include(a => a.Customer)
                    .ThenInclude(c => c.User)
                .Include(a => a.Travel)
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

        // Ajout d'un Service à un AllInclusiveTravel
        public void AddServiceToAllInclusiveTravel(int allInclusiveTravelId, Service service)
        {
            var allInclusiveTravel = _bddContext.AllInclusiveTravels
                .Include(a => a.Travel)
                    .ThenInclude(t => t.Destination)
                        .ThenInclude(d => d.Services)
                .FirstOrDefault(a => a.Id == allInclusiveTravelId);

            if (allInclusiveTravel != null)
            {
                if (allInclusiveTravel.Travel.Destination != null)
                {
                    allInclusiveTravel.ServiceForPackage.Add(service);
                    allInclusiveTravel.Price += service.Price; // Ajout du prix du service au prix total du package
                    _bddContext.SaveChanges();
                }
                else
                {
                    throw new Exception("Destination invalide.");
                }
            }
            else
            {
                throw new Exception("Voyage All Inclusive non valide");
            }
        }

        // Suppression d'un Service d'un AllInclusiveTravel
        public void RemoveServiceFromAllInclusiveTravel(int allInclusiveTravelId, int serviceId)
        {
            var allInclusiveTravel = _bddContext.AllInclusiveTravels
                .Include(a => a.ServiceForPackage)
                .FirstOrDefault(a => a.Id == allInclusiveTravelId);

            if (allInclusiveTravel != null)
            {
                var service = allInclusiveTravel.ServiceForPackage.FirstOrDefault(s => s.Id == serviceId);
                if (service != null)
                {
                    allInclusiveTravel.ServiceForPackage.Remove(service);
                    allInclusiveTravel.Price -= service.Price; // Soustraction du prix du service du prix total du package
                    _bddContext.SaveChanges();
                }
            }
            else
            {
                throw new Exception("Voyage All Inclusive non valide.");
            }
        }
    }
}


