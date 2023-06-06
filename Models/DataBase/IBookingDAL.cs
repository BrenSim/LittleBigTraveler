using System;
using System.Collections.Generic;
using LittleBigTraveler.Models.TravelClasses;

namespace LittleBigTraveler.Models.DataBase
{
    public interface IBookingDAL : IDisposable
    {
        // Créer une réservation
        int CreateBooking(int customerId, int packageId);

        // Récupérer une réservation par son ID
        Booking GetBookingById(int id);

        // Supprimer une réservation
        void DeleteBooking(int id);

        Package GetPackageById(int packageId);

        List<Booking> GetCustomerBookings(int customerId);
    }
}
