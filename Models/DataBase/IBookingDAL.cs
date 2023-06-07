using System;
using System.Collections.Generic;
using LittleBigTraveler.Models.TravelClasses;

namespace LittleBigTraveler.Models.DataBase
{
    public interface IBookingDAL : IDisposable
    {
        int CreateBooking(int userId, int packageId);
        Booking GetBookingById(int id);
        void DeleteBooking(int id);
        Package GetPackageById(int packageId);
        List<Booking> GetUserBookings(int userId);
    }
}
