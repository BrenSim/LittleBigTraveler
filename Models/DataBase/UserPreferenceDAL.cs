using System;
using System.Linq;
using LittleBigTraveler.Models.UserClasses;
using Microsoft.EntityFrameworkCore;

namespace LittleBigTraveler.Models.DataBase
{
    public class UserPreferenceDAL : IUserPreferenceDAL
    {
        private BddContext _bddContext;

        public UserPreferenceDAL()
        {
            _bddContext = new BddContext();
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

        public void AddPreferredDestination(int userId, int destinationId)
        {
            var userPreference = GetUserPreferences(userId);

            if (userPreference != null)
            {
                var destination = _bddContext.Destinations.FirstOrDefault(d => d.Id == destinationId);
                if (destination != null)
                {
                    userPreference.Destination.Add(destination);
                    _bddContext.SaveChanges();
                }
                else
                {
                    throw new Exception("La destination spécifiée n'existe pas.");
                }
            }
            else
            {
                throw new Exception("Les préférences de l'utilisateur n'existent pas.");
            }
        }

        public void AddPreferredService(int userId, int serviceId)
        {
            var userPreference = GetUserPreferences(userId);

            if (userPreference != null)
            {
                var service = _bddContext.Services.FirstOrDefault(s => s.Id == serviceId);
                if (service != null)
                {
                    userPreference.Services.Add(service);
                    _bddContext.SaveChanges();
                }
                else
                {
                    throw new Exception("Le service spécifié n'existe pas.");
                }
            }
            else
            {
                throw new Exception("Les préférences de l'utilisateur n'existent pas.");
            }
        }

        public void RemovePreferredDestination(int userId, int destinationId)
        {
            var userPreference = GetUserPreferences(userId);

            if (userPreference != null)
            {
                var destination = userPreference.Destination.FirstOrDefault(d => d.Id == destinationId);
                if (destination != null)
                {
                    userPreference.Destination.Remove(destination);
                    _bddContext.SaveChanges();
                }
                else
                {
                    throw new Exception("La destination spécifiée n'existe pas dans les préférences de l'utilisateur.");
                }
            }
            else
            {
                throw new Exception("Les préférences de l'utilisateur n'existent pas.");
            }
        }

        public void RemovePreferredService(int userId, int serviceId)
        {
            var userPreference = GetUserPreferences(userId);

            if (userPreference != null)
            {
                var service = userPreference.Services.FirstOrDefault(s => s.Id == serviceId);
                if (service != null)
                {
                    userPreference.Services.Remove(service);
                    _bddContext.SaveChanges();
                }
                else
                {
                    throw new Exception("Le service spécifié n'existe pas dans les préférences de l'utilisateur.");
                }
            }
            else
            {
                throw new Exception("Les préférences de l'utilisateur n'existent pas.");
            }
        }

        public UserPreference GetUserPreferences(int userId)
        {
            return _bddContext.UserPreferences
                               .Include(up => up.Destination)
                               .Include(up => up.Services)
                               .FirstOrDefault(up => up.UserId == userId);
        }
    }
}
