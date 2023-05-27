using System;
using System.Collections.Generic;
using LittleBigTraveler.Models.TravelClasses;

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
    }
}
