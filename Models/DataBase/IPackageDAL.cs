using LittleBigTraveler.Models.TravelClasses;
using LittleBigTraveler.Models.UserClasses;
using System;
using System.Collections.Generic;

namespace LittleBigTraveler.Models.DataBase
{
    public interface IPackageDAL : IDisposable
    {
        void DeleteCreateDatabase();
        int CreatePackage(int travelId, string name, string description, List<Service> services);
        void DeletePackage(int packageId);
        void UpdatePackage(int packageId, int travelId, string name, string description, List<Service> services);
        Package GetPackageById(int id);
        List<Package> GetAllPackage();
        Travel GetTravelById(int travelId);
    }
}