using System;
using System.Collections.Generic;
using System.Linq;
using LittleBigTraveler.Models.DataBase;
using LittleBigTraveler.Models.TravelClasses;

namespace LittleBigTraveler.Models.DataBase
{
    /// <summary>
    /// Fournit des méthodes d'accès aux données pour l'entité Service.
    /// </summary>
    public class ServiceDAL : IServiceDAL
    {
        private BddContext _bddContext;

        /// <summary>
        /// Initialise une nouvelle instance de la classe <see cref="ServiceDAL"/>.
        /// </summary>
        public ServiceDAL()
        {
            _bddContext = new BddContext();
        }

        /// <summary>
        /// Met à jour le service en spécifiant l'ID du package associé.
        /// </summary>
        /// <param name="service">Le service à mettre à jour.</param>
        /// <param name="packageId">L'ID du package associé.</param>
        public void UpdatePackage(Service service, int packageId)
        {
            service.PackageId = packageId;
            _bddContext.SaveChanges();
        }

        /// <summary>
        /// Supprime et recrée la base de données.
        /// </summary>
        public void DeleteCreateDatabase()
        {
            _bddContext.Database.EnsureDeleted();
            _bddContext.Database.EnsureCreated();
        }

        /// <summary>
        /// Libère les ressources utilisées par l'instance <see cref="ServiceDAL"/>.
        /// </summary>
        public void Dispose()
        {
            _bddContext.Dispose();
        }

        /// <summary>
        /// Crée un nouveau service.
        /// </summary>
        /// <param name="name">Le nom du service.</param>
        /// <param name="price">Le prix du service.</param>
        /// <param name="schedule">L'horaire du service.</param>
        /// <param name="location">L'emplacement du service.</param>
        /// <param name="type">Le type du service.</param>
        /// <param name="style">Le style du service.</param>
        /// <param name="maxCapacity">La capacité maximale du service.</param>
        /// <param name="images">La liste des images du service.</param>
        /// <param name="link">Le lien externe du service.</param>
        /// <param name="destinationId">L'ID de la destination associée au service.</param>
        /// <returns>L'ID du service créé.</returns>
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

        /// <summary>
        /// Supprime un service.
        /// </summary>
        /// <param name="id">L'ID du service à supprimer.</param>
        public void DeleteService(int id)
        {
            var service = _bddContext.Services.FirstOrDefault(s => s.Id == id);
            if (service != null)
            {
                _bddContext.Services.Remove(service);
                _bddContext.SaveChanges();
            }
        }

        /// <summary>
        /// Modifie un service existant.
        /// </summary>
        /// <param name="id">L'ID du service à modifier.</param>
        /// <param name="name">Le nouveau nom du service.</param>
        /// <param name="price">Le nouveau prix du service.</param>
        /// <param name="schedule">Le nouvel horaire du service.</param>
        /// <param name="location">Le nouvel emplacement du service.</param>
        /// <param name="type">Le nouveau type du service.</param>
        /// <param name="style">Le nouveau style du service.</param>
        /// <param name="maxCapacity">La nouvelle capacité maximale du service.</param>
        /// <param name="images">La nouvelle liste des images du service.</param>
        /// <param name="link">Le nouveau lien externe du service.</param>
        /// <param name="destinationId">Le nouvel ID de la destination associée au service.</param>
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

        /// <summary>
        /// Effectue une recherche dans les services en fonction du critère spécifié.
        /// </summary>
        /// <param name="query">Le critère de recherche.</param>
        /// <returns>Une liste des services correspondant à la recherche.</returns>
        public List<Service> SearchService(string query)
        {
            IQueryable<Service> recherche = _bddContext.Services;

            if (!string.IsNullOrEmpty(query))
            {
                recherche = recherche.Where(s => s.Name.Contains(query) || s.Type.Contains(query) || s.Location.Contains(query) || s.Style.Contains(query));
            }

            return recherche.ToList();
        }

        /// <summary>
        /// Récupère tous les services.
        /// </summary>
        /// <returns>Une liste de tous les services.</returns>
        public List<Service> GetAllServices()
        {
            return _bddContext.Services.ToList();
        }

        /// <summary>
        /// Récupère un service par son ID.
        /// </summary>
        /// <param name="id">L'ID du service à récupérer.</param>
        /// <returns>Le service avec l'ID spécifié, ou null s'il n'est pas trouvé.</returns>
        public Service GetServiceWithId(int id)
        {
            return _bddContext.Services.FirstOrDefault(s => s.Id == id);
        }

        /// <summary>
        /// Récupère les services correspondant à une liste d'IDs.
        /// </summary>
        /// <param name="ids">La liste des IDs des services à récupérer.</param>
        /// <returns>Une liste de services correspondant aux IDs spécifiés.</returns>
        public List<Service> GetServiceWithIds(List<int> ids)
        {
            return _bddContext.Services.Where(s => ids.Contains(s.Id)).ToList();
        }
    }
}
