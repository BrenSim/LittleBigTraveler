﻿using System;
using System.Collections.Generic;
using LittleBigTraveler.Models.TravelClasses;
using LittleBigTraveler.Models.UserClasses;

namespace LittleBigTraveler.Models.DataBase
{
    public interface IDestinationDAL : IDisposable
    {
        void DeleteCreateDatabase();

        // Méthodes relatives aux destinations
        List<Destination> GetAllDestinations();
        int CreateDestination(string country, string city, string description, string style, List<string> images, string link);
        void DeleteDestination(int id);
        void ModifyDestination(int id, string country, string city, string description, string style, List<string> images, string link);
        Destination GetDestinationWithId(int id);
        List<Destination> SearchDestination(string query);
    }
}


