using System.Collections.Generic;
using System.Linq;
using LittleBigTraveler.Models.DataBase;
using LittleBigTraveler.Models.TravelClasses;
namespace LittleBigTraveler.Models.DataBase
{
    public class DestinationDAL : IDestinationDAL
    {
        private BddContext _bddContext;

        public DestinationDAL()
        {
            _bddContext = new BddContext();
        }

        // Supression/Création de la database (méthode appelé dans BddContext)
        public void DeleteCreateDatabase()
        {
            _bddContext.Database.EnsureDeleted();
            _bddContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

        // Création des données "Destination"
        public int CreateDestination(string country, string city, string description, List<string> images, string link)
        {

            Destination destination = new Destination()
            {
                Country = country,
                City = city,
                Description = description,
                //Style = style,
                Images = images,
                ExternalLinks = link
            };

            _bddContext.Destinations.Add(destination);
            _bddContext.SaveChanges();

            return destination.Id;
        }

        // Suppression des données "Destination"
        public void DeleteDestination(int id)
        {
            var destination = _bddContext.Destinations.FirstOrDefault(d => d.Id == id);
            if (destination != null)
            {
                _bddContext.Destinations.Remove(destination);
                _bddContext.SaveChanges();
            }
        }

        // Modification des données "Destination"
        public void ModifyDestination(int id, string country, string city, string description, List<string> images, string link)
        {
            var destination = _bddContext.Destinations.FirstOrDefault(d => d.Id == id);
            if (destination != null)
            {
                destination.Country = country;
                destination.City = city;
                destination.Description = description;
                //destination.Style = style;
                destination.Images = images;
                destination.ExternalLinks = link;

                _bddContext.SaveChanges();
            }
        }

        // Recherche dans les données "Destination" d'après country, city et style
        public List<Destination> SearchDestination(string query)
        {
            IQueryable<Destination> recherche = _bddContext.Destinations;

            if (!string.IsNullOrEmpty(query))
            {
                recherche = recherche.Where(d => d.Country.Contains(query) || d.City.Contains(query));
            }

            return recherche.ToList();
        }


        // Récupération de toutes les données "Destination"
        public List<Destination> GetAllDestinations()
        {
            return _bddContext.Destinations.ToList();
        }

        // Récupération des données "Destination" par ID
        public Destination GetDestinationWithId(int id)
        {
            return _bddContext.Destinations.FirstOrDefault(d => d.Id == id);
        }
    }
}


