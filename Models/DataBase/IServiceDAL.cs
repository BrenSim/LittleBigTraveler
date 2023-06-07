using System;
using System.Collections.Generic;
using LittleBigTraveler.Models.TravelClasses;
using LittleBigTraveler.Models.UserClasses;

namespace LittleBigTraveler.Models.DataBase
{
    public interface IServiceDAL : IDisposable
    {
        List<Service> GetAllServices();
        int CreateService(string name, double price, DateTime schedule, string location, string type, string style, int maxCapacity, List<string> images, string link, int destinationId);
        void DeleteService(int id);
        void ModifyService(int id, string name, double price, DateTime schedule, string location, string type, string style, int maxCapacity, List<string> images, string link, int destinationId);
        Service GetServiceWithId(int id);
        List<Service> SearchService(string searchText);

    }
}

