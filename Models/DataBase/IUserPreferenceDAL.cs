using System;
using LittleBigTraveler.Models.UserClasses;

namespace LittleBigTraveler.Models.DataBase
{
    public interface IUserPreferenceDAL : IDisposable
    {
        void AddPreferredDestination(int userId, int destinationId);
        void AddPreferredService(int userId, int serviceId);
        void RemovePreferredDestination(int userId, int destinationId);
        void RemovePreferredService(int userId, int serviceId);
        UserPreference GetUserPreferences(int userId);
    }
}

