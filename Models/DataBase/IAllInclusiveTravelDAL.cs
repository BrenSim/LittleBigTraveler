using LittleBigTraveler.Models.TravelClasses;
using LittleBigTraveler.Models.UserClasses;
using System;
using System.Collections.Generic;

namespace LittleBigTraveler.Models.DataBase
{
    public interface IAllInclusiveTravelDAL : IDisposable
    {
        void DeleteCreateDatabase();
        int CreateAllInclusiveTravel(int customerId, int travelId, string name, string description, List<Service> services);
        void DeleteAllInclusiveTravel(int id);
        void ModifyAllInclusiveTravel(int id, string name, string description, List<Service> services);
        AllInclusiveTravel GetAllInclusiveTravelById(int id);
        List<AllInclusiveTravel> GetCustomerAllInclusiveTravels(int customerId);
        //void AddServiceToAllInclusiveTravel(int allInclusiveTravelId, Service service);
        //void RemoveServiceFromAllInclusiveTravel(int allInclusiveTravelId, int serviceId);
        Travel GetTravelById(int travelId);
    }
}
