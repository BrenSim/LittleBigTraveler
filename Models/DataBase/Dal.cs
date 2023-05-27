using System;
using System.Collections.Generic;
using System.Linq;
using LittleBigTraveler.Models.TravelClasses;
using static System.Net.Mime.MediaTypeNames;

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
        public List<Destination> RechercherDestinations(string query)
        {
            IQueryable<Destination> recherche = _bddContext.Destinations;

            if (!string.IsNullOrEmpty(query))
            {
                recherche = recherche.Where(d => d.Country.Contains(query) || d.City.Contains(query) || d.Style.Contains(query));
            }

            return recherche.ToList();
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


// Création, suppression et modification des données "Service"

        // Création des données "Service"
        public int CreerService(string name, double price, DateTime schedule, string location, string type, int maxCapacity, List<string> images, string link)
        {
            Service service = new Service()
            {
                Name = name,
                Price = price,
                Schedule = schedule,
                Location = location,
                Type = type,
                MaxCapacity = maxCapacity,
                Images = images,
                ExternalLinks = link
        };

            _bddContext.Services.Add(service);
            _bddContext.SaveChanges();

            return service.Id;
        }

        // Suppression des données "Service"
        public void SupprimerService(int id)
        {
            var service = _bddContext.Services.FirstOrDefault(s => s.Id == id);
            if (service != null)
            {
                _bddContext.Services.Remove(service);
                _bddContext.SaveChanges();
            }
        }

        // Modification des données "Service"
        public void ModifierService(int id, string name, double price, DateTime schedule, string location, string type, int maxCapacity, List<string> images, string link)
        {
            var service = _bddContext.Services.FirstOrDefault(s => s.Id == id);
            if (service != null)
            {
                service.Name = name;
                service.Price = price;
                service.Schedule = schedule;
                service.Location = location;
                service.Type = type;
                service.MaxCapacity = maxCapacity;
                service.Images = images;
                service.ExternalLinks = link;


                _bddContext.SaveChanges();
            }
        }

        // Recherche dans les données "Service" d'après le type
        public List<Service> RechercherServices(string query)
        {
            IQueryable<Service> recherche = _bddContext.Services;

            if (!string.IsNullOrEmpty(query))
            {
                double prix;
                if (double.TryParse(query, out prix))
                {
                    recherche = recherche.Where(s => s.Price <= prix);
                }
                else
                {
                    recherche = recherche.Where(s => s.Name.Contains(query) || s => s.Type.Contains(query) || s.Location.Contains(query));
                }
            }

            return recherche.ToList();
        }

        // Récupération de tous les services
        public List<Service> ObtientTousServices()
        {
            return _bddContext.Services.ToList();
        }

        // Récupération des données "Service" par ID
        public Service ObtientServiceParId(int id)
        {
            return _bddContext.Services.FirstOrDefault(s => s.Id == id);
        }


    }
}

