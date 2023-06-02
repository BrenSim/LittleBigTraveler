using System;
using System.Collections.Generic;
using System.Linq;
using LittleBigTraveler.Models.DataBase;
using LittleBigTraveler.Models.TravelClasses;

namespace LittleBigTraveler.Models.DataBase
{
    public class ServiceDAL : IServiceDAL
    {
        private BddContext _bddContext;

        public ServiceDAL()
        {
            _bddContext = new BddContext();
        }
        public void UpdateAllInclusiveTravel(Service service, int allinclusiveId)
        {
            service.AllInclusiveTravelId = allinclusiveId;
            _bddContext.SaveChanges();
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

        // Création des données "Service"
        public int CreateService(string name, double price, DateTime schedule, string location, string type, string style, int maxCapacity, List<string> images, string link, int destinationId)
        {
            Service service = new Service()
            {
                Name = name,
                Price = price,
                Schedule = schedule,
                Location = location,
                Type = type,
                Style = style,
                MaxCapacity = maxCapacity,
                Images = images,
                ExternalLinks = link,
                DestinationId = destinationId
            };

            _bddContext.Services.Add(service);
            _bddContext.SaveChanges();

            return service.Id;
        }

        // Suppression des données "Service"
        public void DeleteService(int id)
        {
            var service = _bddContext.Services.FirstOrDefault(s => s.Id == id);
            if (service != null)
            {
                _bddContext.Services.Remove(service);
                _bddContext.SaveChanges();
            }
        }

        // Modification des données "Service"
        public void ModifyService(int id, string name, double price, DateTime schedule, string location, string type, string style, int maxCapacity, List<string> images, string link, int destinationId)
        {
            var service = _bddContext.Services.FirstOrDefault(s => s.Id == id);
            if (service != null)
            {
                service.Name = name;
                service.Price = price;
                service.Schedule = schedule;
                service.Location = location;
                service.Type = type;
                service.Style = style;
                service.MaxCapacity = maxCapacity;
                service.Images = images;
                service.ExternalLinks = link;
                service.DestinationId = destinationId;

                _bddContext.SaveChanges();
            }
        }

        // Recherche dans les données "Service" en fonction du type
        public List<Service> SearchService(string query)
        {
            IQueryable<Service> recherche = _bddContext.Services;

            if (!string.IsNullOrEmpty(query))
            {
                recherche = recherche.Where(s => s.Name.Contains(query) || s.Type.Contains(query) || s.Location.Contains(query) || s.Style.Contains(query));
            }

            return recherche.ToList();
        }

        // Récupération de tous les services
        public List<Service> GetAllServices()
        {
            return _bddContext.Services.ToList();
        }

        // Récupération des données "Service" par ID
        public Service GetServiceWithId(int id)
        {
            return _bddContext.Services.FirstOrDefault(s => s.Id == id);
        }

        // Récupération des données "Service" par liste d'IDs
        public List<Service> GetServiceWithIds(List<int> ids)
        {
            return _bddContext.Services.Where(s => ids.Contains(s.Id)).ToList();
        }
    }
}
