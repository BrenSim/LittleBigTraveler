using System;
using System.Collections.Generic;
using LittleBigTraveler.Models.TravelClasses;

namespace LittleBigTraveler.Models.DataBase
{
    public interface ITravelDAL : IDisposable
    {
        int CreateTravel(int customerId, int destinationId, string departureLocation, DateTime departureDate, DateTime returnDate, double price, int numParticipants);
        void ModifyTravel(int id, int customerId, int destinationId, string departureLocation, DateTime departureDate, DateTime returnDate, double price, int numParticipants);
        void DeleteTravel(int id);
        Travel GetTravelWithId(int id);
        List<Travel> GetAllTravels();
    }
}


