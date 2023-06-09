using System;
using System.Collections.Generic;
using LittleBigTraveler.Models.TravelClasses;
using LittleBigTraveler.Models.UserClasses;

namespace LittleBigTraveler.Models.DataBase
{
    public interface IDestinationDAL : IDisposable
    {
        void DeleteCreateDatabase();

        List<Destination> GetAllDestinations();
        int CreateDestination(string country, string city, string description, List<string> images, string link);
        void DeleteDestination(int id);
        void ModifyDestination(int id, string country, string city, string description, List<string> images, string link);
        Destination GetDestinationWithId(int id);
        List<Destination> SearchDestination(string query);
    }
}



