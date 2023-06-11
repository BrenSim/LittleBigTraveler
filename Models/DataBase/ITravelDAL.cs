using System;
using System.Collections.Generic;
using LittleBigTraveler.Models.TravelClasses;

namespace LittleBigTraveler.Models.DataBase
{
    public interface ITravelDAL : IDisposable
    {
        int CreateTravel(int destinationId, string departureLocation, DateTime departureDate, DateTime returnDate, int price, int numParticipants);
        void ModifyTravel(int id, int destinationId, string departureLocation, DateTime departureDate, DateTime returnDate, int price, int numParticipants);
        void DeleteTravel(int id);
    }
}