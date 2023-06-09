using System.Collections.Generic;
using System.Linq;
using LittleBigTraveler.Models.DataBase;
using LittleBigTraveler.Models.TravelClasses;
using Microsoft.EntityFrameworkCore;

namespace LittleBigTraveler.Models.DataBase
{
    public class DestinationDAL : IDestinationDAL
    {
        private BddContext _bddContext;

        public DestinationDAL()
        {
            _bddContext = new BddContext();
        }

        /// <summary>
        /// Supprime et recrée la base de données (méthode appelée dans BddContext).
        /// </summary>
        public void DeleteCreateDatabase()
        {
            _bddContext.Database.EnsureDeleted();
            _bddContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

        /// <summary>
        /// Crée une nouvelle destination.
        /// </summary>
        /// <param name="country">Le pays de la destination</param>
        /// <param name="city">La ville de la destination</param>
        /// <param name="description">La description de la destination</param>
        /// <param name="images">La liste des images de la destination</param>
        /// <param name="link">Le lien externe de la destination</param>
        /// <returns>Renvoie l'ID de la destination créée</returns>
        public int CreateDestination(string country, string city, string description, List<string> images, string link)
        {
            Destination destination = new Destination()
            {
                Country = country,
                City = city,
                Description = description,
                Images = images,
                ExternalLinks = link
            };

            _bddContext.Destinations.Add(destination);
            _bddContext.SaveChanges();

            return destination.Id;
        }

        /// <summary>
        /// Supprime une destination.
        /// </summary>
        /// <param name="id">L'ID de la destination à supprimer</param>
        public void DeleteDestination(int id)
        {
            var destination = _bddContext.Destinations.FirstOrDefault(d => d.Id == id);
            if (destination != null)
            {
                _bddContext.Destinations.Remove(destination);
                _bddContext.SaveChanges();
            }
        }

        /// <summary>
        /// Modifie une destination existante.
        /// </summary>
        /// <param name="id">L'ID de la destination à modifier</param>
        /// <param name="country">Le pays de la destination</param>
        /// <param name="city">La ville de la destination</param>
        /// <param name="description">La description de la destination</param>
        /// <param name="images">La liste des images de la destination</param>
        /// <param name="link">Le lien externe de la destination</param>
        public void ModifyDestination(int id, string country, string city, string description, List<string> images, string link)
        {
            var destination = _bddContext.Destinations.FirstOrDefault(d => d.Id == id);
            if (destination != null)
            {
                destination.Country = country;
                destination.City = city;
                destination.Description = description;
                destination.Images = images;
                destination.ExternalLinks = link;

                _bddContext.SaveChanges();
            }
        }

        /// <summary>
        /// Recherche des destinations selon le pays (country) ou la ville (city).
        /// </summary>
        /// <param name="query">La chaîne de recherche</param>
        /// <returns>Renvoie une liste de destinations correspondant à la recherche</returns>
        public List<Destination> SearchDestination(string query)
        {
            IQueryable<Destination> recherche = _bddContext.Destinations;

            if (!string.IsNullOrEmpty(query))
            {
                recherche = recherche.Where(d => d.Country.Contains(query) || d.City.Contains(query));
            }

            return recherche.ToList();
        }

        /// <summary>
        /// Récupère toutes les destinations.
        /// </summary>
        /// <returns>Renvoie une liste de toutes les destinations</returns>
        public List<Destination> GetAllDestinations()
        {
            return _bddContext.Destinations.ToList();
        }

        /// <summary>
        /// Récupère une destination par son ID avec ses services associés.
        /// </summary>
        /// <param name="id">L'ID de la destination à récupérer</param>
        /// <returns>Renvoie l'objet Destination correspondant à l'ID spécifié avec ses services associés</returns>
        public Destination GetDestinationWithId(int id)
        {
            return _bddContext.Destinations
                           .Include(d => d.Services)  // Charge les services associés
                           .FirstOrDefault(d => d.Id == id);
        }

        /// <summary>
        /// Récupère les services associés à une destination.
        /// </summary>
        /// <param name="destinationId">L'ID de la destination</param>
        /// <returns>Renvoie une liste de services associés à la destination spécifiée</returns>
        public List<Service> GetServicesByDestinationId(int destinationId)
        {
            return _bddContext.Services
                       .Where(s => s.DestinationId == destinationId)
                       .ToList();
        }
    }
}
