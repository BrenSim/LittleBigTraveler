using LittleBigTraveler.Models.DataBase;
using LittleBigTraveler.Models.TravelClasses;
using LittleBigTraveler.Models.UserClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class BookingDAL : IBookingDAL
{
    private BddContext _bddContext;
    private IHttpContextAccessor _httpContextAccessor;
    public BookingDAL(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _bddContext = new BddContext();
    }

    public void Dispose()
    {
        _bddContext.Dispose();
    }


    // Création d'une réservation (Booking)
    public int CreateBooking(int customerId, int packageId)
    {
        var customer = _bddContext.Customers.FirstOrDefault(c => c.Id == customerId);
        var package = _bddContext.Packages.Include(p => p.ServiceForPackage)
                                           .FirstOrDefault(p => p.Id == packageId);

        if (customer != null && package != null)
        {
            Booking booking = new Booking()
            {
                CustomerId = customerId,
                PackageId = packageId,
                Payments = new List<Payment>(),
                Evaluations = new List<Evaluation>()
            };

            _bddContext.Bookings.Add(booking);
            _bddContext.SaveChanges();

            return booking.Id;
        }
        else
        {
            throw new Exception("Customer or Package not found");
        }
    }

    // Suppression d'une réservation (Booking)
    public void DeleteBooking(int bookingId)
    {
        var booking = _bddContext.Bookings.FirstOrDefault(b => b.Id == bookingId);

        if (booking != null)
        {
            _bddContext.Bookings.Remove(booking);
            _bddContext.SaveChanges();
        }
        else
        {
            throw new Exception("Booking not found");
        }
    }

    // Récupération d'une réservation (Booking) par ID
    public Booking GetBookingById(int bookingId)
    {
        return _bddContext.Bookings
            .Include(b => b.Customer)
                .ThenInclude(c => c.User)
            .Include(b => b.Package)
                .ThenInclude(p => p.Travel)
            .Include(b => b.Package)
                .ThenInclude(p => p.ServiceForPackage)
            .FirstOrDefault(b => b.Id == bookingId);
    }


    // Récupération de toutes les réservations (Booking) d'un client
    public List<Booking> GetCustomerBookings(int customerId)
    {
        return _bddContext.Bookings
            .Include(b => b.Customer)
                .ThenInclude(c => c.User)
            .Include(b => b.Package)
                .ThenInclude(p => p.Travel)
            .Include(b => b.Package)
                .ThenInclude(p => p.ServiceForPackage)
            .Where(b => b.Customer.Id == customerId)
            .ToList();
    }

    // Récupération d'un PackageTravel par ID
    public Package GetPackageById(int id)
    {
        return _bddContext.Packages
            .Include(a => a.Travel)
            .Include(a => a.ServiceForPackage)
            .FirstOrDefault(a => a.Id == id);
    }
}
