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
    public class TravelDAL : ITravelDAL
    {
        public void SaveChanges()
        {
            _bddContext.SaveChanges();
        }
        private BddContext _bddContext;
        // Manière de récupérer l'ID du client connecté
        private IHttpContextAccessor _httpContextAccessor;

        public TravelDAL(IHttpContextAccessor httpContextAccessor)
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

        // Création/Ajout d'un "Travel"
        public int CreateTravel(int customerId, int destinationId, string departureLocation, DateTime departureDate, DateTime returnDate, double price, int numParticipants)
        {
            // Récupération du client associé à l'ID fourni
            var customer = _bddContext.Customers.Include(c => c.User).FirstOrDefault(c => c.Id == customerId);
            // Récupération de la destination associée à l'ID fourni
            var destination = _bddContext.Destinations.FirstOrDefault(d => d.Id == destinationId);
            if (customer != null && destination != null)
            {
                // Création d'un nouvel objet Travel avec les données fournies
                Travel travel = new Travel()
                {
                    Customer = customer,
                    Destination = destination,
                    DepartureLocation = departureLocation,
                    DepartureDate = departureDate,
                    ReturnDate = returnDate,
                    Price = price,
                    NumParticipants = numParticipants
                };

                _bddContext.Travels.Add(travel);
                _bddContext.SaveChanges();

                return travel.Id;
            }
            else
            {
                throw new Exception("Customer ou destination non valide.");
            }
        }

        // Suppression d'un "Travel" par ID
        public void DeleteTravel(int id)
        {
            var travel = _bddContext.Travels.FirstOrDefault(t => t.Id == id);
            if (travel != null)
            {
                _bddContext.Travels.Remove(travel);
                _bddContext.SaveChanges();
            }
        }

        public void ModifyTravel(int id, int customerId, int destinationId, string departureLocation, DateTime departureDate, DateTime returnDate, double price, int numParticipants)
        {
            var travel = _bddContext.Travels
                .Include(t => t.Customer)
                .Include(t => t.Destination)
                .FirstOrDefault(t => t.Id == id);

            var customer = _bddContext.Customers
                .Include(c => c.User)
                .FirstOrDefault(c => c.Id == customerId);

            var destination = _bddContext.Destinations
                .FirstOrDefault(d => d.Id == destinationId);

            if (travel != null && customer != null && destination != null)
            {
                // Vérifier si le client ou la destination du voyage a changé
                if (travel.Customer.Id != customerId || travel.Destination.Id != destinationId)
                {
                    throw new Exception("Modification du client ou de la destination non autorisée.");
                }

                // Mise à jour des propriétés du "Travel" avec les nouvelles données fournies
                travel.DepartureLocation = departureLocation;
                travel.DepartureDate = departureDate;
                travel.ReturnDate = returnDate;
                travel.Price = price;
                travel.NumParticipants = numParticipants;

                // Mettre à jour les relations Customer et Destination
                travel.Customer = customer;
                travel.Destination = destination;

                _bddContext.SaveChanges();
            }
            else
            {
                throw new Exception("Client, voyage ou destination non valide.");
            }
        }


        // Récupération des voyages d'un client par son ID
        public List<Travel> GetTravelsByCustomerId(int customerId)
        {
            // Récupération de l'ID du client connecté à partir du contexte HTTP
            customerId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);

            return _bddContext.Travels
                .Include(t => t.Customer)
                    .ThenInclude(c => c.User)
                .Include(t => t.Destination)
                .Where(t => t.Customer.Id == customerId)
                .ToList();
        }

        // Récupération d'un voyage par son ID
        public Travel GetTravelById(int travelId)

        {
            return _bddContext.Travels
                .Include(t => t.Customer)
                    .ThenInclude(c => c.User)
                .Include(t => t.Destination)
                .FirstOrDefault(t => t.Id == travelId);
        }

        // Récupération d'un voyage par son ID pour un client spécifique
        public Travel GetTravelByIdForCustomer(int travelId)
        {
            int customerId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);

            return _bddContext.Travels
                .Include(t => t.Customer)
                    .ThenInclude(c => c.User)
                .Include(t => t.Destination)
                .FirstOrDefault(t => t.Id == travelId && t.Customer.Id == customerId);
        }

    }
}
