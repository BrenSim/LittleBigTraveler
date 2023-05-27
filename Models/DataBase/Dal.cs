using System;
using System.Collections.Generic;
using System.Linq;
using LittleBigTraveler.Models.TravelClasses;

namespace LittleBigTraveler.Models.DataBase
{

// Data access layer général (initialisation de la database)

	public class Dal : IDal
	{
        private BddContext _bddContext;

        public Dal()
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

// Création, suppression et modification des données "Destination"

        // Création des données "Destination"
        public int CreerDestination(string country, string city, string description, string style, List<string> images, string link)
        {
            
            Destination destination = new Destination()
            {
                Country = country,
                City = city,
                Description = description,
                Style = style,
                Images = images,
                ExternalLinks = link
            };

            _bddContext.Destinations.Add(destination);
            _bddContext.SaveChanges();

            return destination.Id;
        }

        // Suppression des données "Destination"
        public void SupprimerDestination(int id)
        {
            var destination = _bddContext.Destinations.FirstOrDefault(d => d.Id == id);
            if (destination != null)
            {
                _bddContext.Destinations.Remove(destination);
                _bddContext.SaveChanges();
            }
        }

        // Modification des données "Destination"
        public void ModifierDestination(int id, string country, string city, string description, string style, List<string> images, string link)
        {
            var destination = _bddContext.Destinations.FirstOrDefault(d => d.Id == id);
            if (destination != null)
            {
                destination.Country = country;
                destination.City = city;
                destination.Description = description;
                destination.Style = style;
                destination.Images = images;
                destination.ExternalLinks = link;

                _bddContext.SaveChanges();
            }
        }

        // Recherche dans les données "Destination" d'après country, city et style
        public List<Destination> RechercherDestinations(string country, string city, string style)
        {
            IQueryable<Destination> query = _bddContext.Destinations;

            if (!string.IsNullOrEmpty(country))
            {
                query = query.Where(d => d.Country == country);
            }

            if (!string.IsNullOrEmpty(city))
            {
                query = query.Where(d => d.City == city);
            }

            if (!string.IsNullOrEmpty(style))
            {
                query = query.Where(d => d.Style == style);
            }

            return query.ToList();
        }


        // Récupération de toutes les données "Destination"
        public List<Destination> ObtientToutesDestination()
        {
            return _bddContext.Destinations.ToList();
        }

        // Récupération des données "Destination" par ID
        public Destination ObtientDestinationParId(int id)
        {
            return _bddContext.Destinations.FirstOrDefault(d => d.Id == id);
        }

    }
}

