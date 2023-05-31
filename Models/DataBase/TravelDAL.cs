using System;
using System.Collections.Generic;
using System.Linq;
using LittleBigTraveler.Models.DataBase;
using LittleBigTraveler.Models.TravelClasses;

namespace LittleBigTraveler.Models.DataBase
{
    public class TravelDAL : ITravelDAL
    {
        private BddContext _bddContext;

        public TravelDAL()
        {
            _bddContext = new BddContext();
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

        // Création des données "Travel"
        public int CreateTravel(int customerId, int destinationId, string departureLocation, DateTime departureDate, DateTime returnDate, double price, int numParticipants)
        {
            Travel travel = new Travel()
            {
                CustomerId = customerId,
                DestinationId = destinationId,
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

        // Suppression des données "Travel"
        public void DeleteTravel(int id)
        {
            var travel = _bddContext.Travels.FirstOrDefault(t => t.Id == id);
            if (travel != null)
            {
                _bddContext.Travels.Remove(travel);
                _bddContext.SaveChanges();
            }
        }

        // Modification des données "Travel"
        public void ModifyTravel(int id, int customerId, int destinationId, string departureLocation, DateTime departureDate, DateTime returnDate, double price, int numParticipants)
        {
            var travel = _bddContext.Travels.FirstOrDefault(t => t.Id == id);
            if (travel != null)
            {
                travel.CustomerId = customerId;
                travel.DestinationId = destinationId;
                travel.DepartureLocation = departureLocation;
                travel.DepartureDate = departureDate;
                travel.ReturnDate = returnDate;
                travel.Price = price;
                travel.NumParticipants = numParticipants;

                _bddContext.SaveChanges();
            }
        }

        // Récupération de tous les voyages
        public List<Travel> GetAllTravels()
        {
            return _bddContext.Travels.ToList();
        }

        // Récupération d'un voyage par ID
        public Travel GetTravelWithId(int id)
        {
            return _bddContext.Travels.FirstOrDefault(t => t.Id == id);
        }
    }
}
