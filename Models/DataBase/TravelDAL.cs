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
    /// <summary>
    /// Fournit des méthodes d'accès aux données pour l'entité Travel.
    /// </summary>
    public class TravelDAL : ITravelDAL
    {
        private BddContext _bddContext;
        private IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="TravelDAL"/>.
        /// </summary>
        /// <param name="httpContextAccessor">L'accessoir du contexte HTTP.</param>
        public TravelDAL(IHttpContextAccessor httpContextAccessor)
        {
            _bddContext = new BddContext();
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Libère les ressources utilisées par l'instance <see cref="TravelDAL"/>.
        /// </summary>
        public void Dispose()
        {
            _bddContext.Dispose();
        }

        /// <summary>
        /// Crée un nouveau voyage.
        /// </summary>
        /// <param name="destinationId">L'ID de la destination.</param>
        /// <param name="departureLocation">Le lieu de départ.</param>
        /// <param name="departureDate">La date de départ.</param>
        /// <param name="returnDate">La date de retour.</param>
        /// <param name="price">Le prix.</param>
        /// <param name="numParticipants">Le nombre de participants.</param>
        /// <returns>L'ID du voyage créé.</returns>
        public int CreateTravel(int destinationId, string departureLocation, DateTime departureDate, DateTime returnDate, int price, int numParticipants)
        {
            var destination = _bddContext.Destinations.FirstOrDefault(d => d.Id == destinationId);
            if (destination != null)
            {
                var travel = new Travel()
                {
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
                throw new Exception("Destination non valide.");
            }
        }

        /// <summary>
        /// Supprime un voyage par son ID.
        /// </summary>
        /// <param name="id">L'ID du voyage à supprimer.</param>
        public void DeleteTravel(int id)
        {
            var travel = _bddContext.Travels.FirstOrDefault(t => t.Id == id);
            if (travel != null)
            {
                _bddContext.Travels.Remove(travel);
                _bddContext.SaveChanges();
            }
        }

        /// <summary>
        /// Modifie un voyage existant.
        /// </summary>
        /// <param name="id">L'ID du voyage à modifier.</param>
        /// <param name="destinationId">Le nouvel ID de la destination.</param>
        /// <param name="departureLocation">Le nouveau lieu de départ.</param>
        /// <param name="departureDate">La nouvelle date de départ.</param>
        /// <param name="returnDate">La nouvelle date de retour.</param>
        /// <param name="price">Le nouveau prix.</param>
        /// <param name="numParticipants">Le nouveau nombre de participants.</param>
        public void ModifyTravel(int id, int destinationId, string departureLocation, DateTime departureDate, DateTime returnDate, int price, int numParticipants)
        {
            var travel = _bddContext.Travels
                .Include(t => t.Destination)
                .FirstOrDefault(t => t.Id == id);

            var destination = _bddContext.Destinations
                .FirstOrDefault(d => d.Id == destinationId);

            if (travel != null && destination != null)
            {
                if (travel.Destination.Id != destinationId)
                {
                    throw new Exception("Modification de la destination non autorisée.");
                }

                travel.DepartureLocation = departureLocation;
                travel.DepartureDate = departureDate;
                travel.ReturnDate = returnDate;
                travel.Price = price;
                travel.NumParticipants = numParticipants;

                travel.Destination = destination;

                _bddContext.SaveChanges();
            }
            else
            {
                throw new Exception("Voyage ou destination non valide.");
            }
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
        /// Récupère tous les voyages.
        /// </summary>
        /// <returns>Une liste de tous les voyages.</returns>
        public List<Travel> GetAllTravels()
        {
            return _bddContext.Travels
                .Include(t => t.Destination)
                .ToList();
        }

        /// <summary>
        /// Enregistre les modifications apportées à la base de données.
        /// </summary>
        public void SaveChanges()
        {
            _bddContext.SaveChanges();
        }
    }
}





