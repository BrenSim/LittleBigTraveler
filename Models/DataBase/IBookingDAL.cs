using System;
using System.Collections.Generic;
using LittleBigTraveler.Models.TravelClasses;

namespace LittleBigTraveler.Models.DataBase
{
    public interface IBookingDAL : IDisposable
    {
        // Créer une réservation
        int CreateBooking(int userId, int packageId);

        // Récupérer une réservation par son ID
        Booking GetBookingById(int id);

        // Supprimer une réservation
        void DeleteBooking(int id);

        // Récupérer un package par son ID
        Package GetPackageById(int packageId);

        // Récupérer toutes les réservations d'un utilisateur
        List<Booking> GetUserBookings(int userId);
    }
}
