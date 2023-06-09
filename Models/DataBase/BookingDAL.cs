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

    /// <summary>
    /// Crée une réservation (Booking).
    /// </summary>
    /// <param name="userId">L'ID de l'utilisateur associé à la réservation</param>
    /// <param name="packageId">L'ID du package associé à la réservation</param>
    /// <returns>Renvoie l'ID de la réservation créée</returns>
    public int CreateBooking(int userId, int packageId)
    {
        var user = _bddContext.Users.FirstOrDefault(u => u.Id == userId);
        var package = _bddContext.Packages.Include(p => p.ServiceForPackage)
                                           .FirstOrDefault(p => p.Id == packageId);

        if (user != null && package != null)
        {
            Booking booking = new Booking()
            {
                UserId = userId,
                PackageId = packageId,
                Price = package.Price,
                Payments = new List<Payment>(),
                Evaluations = new List<Evaluation>()
            };

            _bddContext.Bookings.Add(booking);
            _bddContext.SaveChanges();

            return booking.Id;
        }
        else
        {
            throw new Exception("User or Package not found");
        }
    }

    /// <summary>
    /// Supprime une réservation (Booking).
    /// </summary>
    /// <param name="bookingId">L'ID de la réservation à supprimer</param>
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

    /// <summary>
    /// Récupère une réservation (Booking) par ID.
    /// </summary>
    /// <param name="bookingId">L'ID de la réservation à récupérer</param>
    /// <returns>Renvoie l'objet Booking correspondant à l'ID spécifié</returns>
    public Booking GetBookingById(int bookingId)
    {
        var booking = _bddContext.Bookings
            .Include(b => b.User)
            .Include(b => b.Package)
                .ThenInclude(p => p.Travel)
            .Include(b => b.Package)
                .ThenInclude(p => p.ServiceForPackage)
            .FirstOrDefault(b => b.Id == bookingId);
        return booking;
    }

    /// <summary>
    /// Récupère toutes les réservations (Booking) d'un utilisateur.
    /// </summary>
    /// <param name="userId">L'ID de l'utilisateur</param>
    /// <returns>Renvoie une liste de réservations (Booking) de l'utilisateur spécifié</returns>
    public List<Booking> GetUserBookings(int userId)
    {
        return _bddContext.Bookings
            .Include(b => b.User)
            .Include(b => b.Package)
                .ThenInclude(p => p.Travel)
            .Include(b => b.Package)
                .ThenInclude(p => p.ServiceForPackage)
            .Where(b => b.User.Id == userId)
            .ToList();
    }

    /// <summary>
    /// Récupère un PackageTravel par ID.
    /// </summary>
    /// <param name="id">L'ID du PackageTravel</param>
    /// <returns>Renvoie l'objet Package correspondant à l'ID spécifié</returns>
    public Package GetPackageById(int id)
    {
        return _bddContext.Packages
            .Include(a => a.Travel)
            .Include(a => a.ServiceForPackage)
            .FirstOrDefault(a => a.Id == id);
    }
}
