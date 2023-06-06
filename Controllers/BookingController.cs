using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LittleBigTraveler.Models.DataBase;
using LittleBigTraveler.Models.UserClasses;
using LittleBigTraveler.Models.TravelClasses;
using LittleBigTraveler.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

public class BookingController : Controller
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BookingController(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public IActionResult List()
    {
        try
        {
            // Récupérer l'ID de l'utilisateur connecté depuis le contexte HTTP
            int userId = int.Parse(HttpContext.User.Identity.Name);
            List<Booking> bookings;

            using (var bookingDAL = new BookingDAL(_httpContextAccessor))
            {
                // Récupérer les réservations de l'utilisateur
                bookings = bookingDAL.GetUserBookings(userId);
            }

            return View(bookings);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    public IActionResult Create(int packageId)
    {
        try
        {
            // Récupérer l'ID de l'utilisateur connecté depuis le contexte HTTP
            int userId = int.Parse(HttpContext.User.Identity.Name);
            Package package;

            using (var packageDAL = new PackageDAL(_httpContextAccessor))
            {
                // Vérifier si le package existe
                package = packageDAL.GetPackageById(packageId);
                if (package == null)
                {
                    return NotFound("Package not found");
                }
            }

            int bookingId;

            using (var bookingDAL = new BookingDAL(_httpContextAccessor))
            {
                // Créer une nouvelle réservation
                bookingId = bookingDAL.CreateBooking(userId, packageId);
            }

            // Rediriger vers la page de confirmation de réservation avec l'ID de la réservation
            return RedirectToAction("Confirmation", new { bookingId });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    public IActionResult Confirmation(int bookingId)
    {
        try
        {
            // Récupérer l'ID de l'utilisateur connecté depuis le contexte HTTP
            int userId = int.Parse(HttpContext.User.Identity.Name);
            Booking booking;

            using (var bookingDAL = new BookingDAL(_httpContextAccessor))
            {
                // Récupérer la réservation en utilisant l'ID de réservation
                booking = bookingDAL.GetBookingById(bookingId);

                // Vérifier si la réservation existe et appartient à l'utilisateur
                if (booking == null || booking.UserId != userId)
                {
                    return NotFound("Booking not found");
                }
            }

            // Créer une liste contenant uniquement la réservation actuelle
            var model = new List<Booking> { booking };

            return View("Confirmation", model);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //public IActionResult Confirmation(int bookingId)
    //{
    //    try
    //    {
    //        // Récupérer l'ID de l'utilisateur connecté depuis le contexte HTTP
    //        int userId = int.Parse(HttpContext.User.Identity.Name);
    //        //Console.WriteLine("UserId: " + userId);  // Log user id
    //        Booking booking;

    //        using (var bookingDAL = new BookingDAL(_httpContextAccessor))
    //        {
    //            // Récupérer la réservation en utilisant l'ID de réservation
    //            booking = bookingDAL.GetBookingById(bookingId);
    //            //Console.WriteLine("BookingId: " + booking?.Id);  // Log booking id



    //            // Vérifier si la réservation existe et appartient à l'utilisateur
    //            if (booking == null || booking.UserId != userId)
    //            {
    //                return NotFound("Booking not found");
    //            }
    //        }

    //        // Créer le modèle de vue pour la confirmation de réservation
    //        var model = new ConfirmationViewModel
    //        {
    //            BookingId = booking.Id,
    //            CustomerId = userId,
    //            PackageName = booking.Package.Name,
    //            PackageDescription = booking.Package.Description,
    //            PackagePrice = (decimal)booking.Package.Price
    //        };

    //        return View(model);
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //}

    public IActionResult Delete(int bookingId)
    {
        try
        {
            // Récupérer l'ID de l'utilisateur connecté depuis le contexte HTTP
            int userId = int.Parse(HttpContext.User.Identity.Name);
            Booking booking;

            using (var bookingDAL = new BookingDAL(_httpContextAccessor))
            {
                // Récupérer la réservation en utilisant l'ID de réservation
                booking = bookingDAL.GetBookingById(bookingId);

                // Vérifier si la réservation existe et appartient à l'utilisateur
                if (booking == null || booking.UserId != userId)
                {
                    return NotFound("Booking not found");
                }

                // Supprimer la réservation
                bookingDAL.DeleteBooking(bookingId);
            }

            return RedirectToAction("List");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}


