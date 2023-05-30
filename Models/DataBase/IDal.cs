using System;
using System.Collections.Generic;
using LittleBigTraveler.Models.TravelClasses;
using LittleBigTraveler.Models.UserClasses;

namespace LittleBigTraveler.Models.DataBase
{
    public interface IDal : IDisposable
    {
        void DeleteCreateDatabase();

        // Méthodes relatives aux destinations
        List<Destination> ObtientToutesDestination();
        int CreerDestination(string country, string city, string description, string style, List<string> images, string link);
        void SupprimerDestination(int id);
        void ModifierDestination(int id, string country, string city, string description, string style, List<string> images, string link);
        Destination ObtientDestinationParId(int id);
        List<Destination> RechercherDestinations(string searchText);

        // Méthodes relatives aux services
        List<Service> ObtientTousServices();
        int CreerService(string name, double price, DateTime schedule, string location, string type, int maxCapacity, List<string> images, string link);
        void SupprimerService(int id);
        void ModifierService(int id, string name, double price, DateTime schedule, string location, string type, int maxCapacity, List<string> images, string link);
        Service ObtientServiceParId(int id);
        List<Service> RechercherServices(string searchText);

        // Méthodes relatives aux utilisateurs
        int CreerAdministrateur(string lastName, string firstName, string email, string password, string address, string phoneNumber, DateTime birthDate);
        int CreerPartenaireAvecRole(string lastName, string firstName, string email, string password, string address, string phoneNumber, DateTime birthDate, string roleName, string roleType);
        int CreerClient(string lastName, string firstName, string email, string password, string address, string phoneNumber, DateTime birthDate, int loyaltyPoint, int commentPoint);
        void SupprimerUser(int id);
        void ModifierUser(int id, string lastName, string firstName, string email, string password, string address, string phoneNumber, DateTime birthDate, string profilePicture);
        List<User> RechercherUsers(string searchText);
        List<User> ObtientTousUsersAvecType();
        User ObtientUserParIdAvecType(int id);
    }
}
