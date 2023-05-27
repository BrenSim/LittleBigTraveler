using System;
using System.Collections.Generic;
using LittleBigTraveler.Models.TravelClasses;
using static System.Net.Mime.MediaTypeNames;

namespace LittleBigTraveler.Models.DataBase
{
    public interface IDal : IDisposable
    {
        void DeleteCreateDatabase();
        List<Destination> ObtientToutesDestination();
        int CreerDestination(string country, string city, string description, string style, List<string> images, string link);
        void SupprimerDestination(int id);
        void ModifierDestination(int id, string country, string city, string description, string style, List<string> images, string link);
        Destination ObtientDestinationParId(int id);
        List<Destination> RechercherDestinations(string searchText);

        List<Service> ObtientTousServices();
        int CreerService(string name, double price, DateTime schedule, string location, string type, int maxCapacity, List<string> images, string link);
        void SupprimerService(int id);
        void ModifierService(int id, string name, double price, DateTime schedule, string location, string type, int maxCapacity, List<string> images, string link);
        Service ObtientServiceParId(int id);
        List<Service> RechercherServices(string searchText);
    }
}
