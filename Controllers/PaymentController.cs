using LittleBigTraveler.Models.DataBase;
using LittleBigTraveler.Models.UserClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using LittleBigTraveler.ViewModels;
using Microsoft.AspNetCore.Authorization;
using LittleBigTraveler.Models.TravelClasses;

public class PaymentController : Controller
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PaymentController(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Action pour afficher le formulaire de création d'un paiement.
    /// </summary>
    /// <param name="bookingId">ID de la réservation associée au paiement.</param>
    /// <param name="totalAmount">Montant total du paiement.</param>
    /// <returns>Vue contenant le formulaire de création d'un paiement.</returns>
    //[Authorize]
    //public IActionResult Create(int bookingId, double totalAmount)
    //{
    //    var model = new PaymentViewModel
    //    {
    //        BookingId = bookingId,
    //        TotalAmount = totalAmount
    //    };

    //    return View(model);
    //}

    /// <summary>
    /// Méthode pour traiter le formulaire de création d'un paiement.
    /// </summary>
    /// <param name="bookingId">ID de la réservation associée au paiement.</param>
    /// <param name="totalAmount">Montant total du paiement.</param>
    /// <param name="numCB">Numéro de carte bancaire.</param>
    /// <returns>Redirige vers la page de confirmation du paiement en cas de succès, sinon renvoie un code d'erreur.</returns>
    [Authorize]
    [HttpPost]
    public IActionResult Create(int bookingId, double totalAmount, int numCB)
    {
        try
        {
            // Récupérer l'ID de l'utilisateur connecté depuis le contexte HTTP
            int userId = int.Parse(HttpContext.User.Identity.Name);

            using (var paymentDAL = new PaymentDAL(_httpContextAccessor))
            {
                // Créer un nouveau paiement
                int paymentId = paymentDAL.CreatePayment(userId, bookingId, numCB);

                // Rediriger vers la page de confirmation du paiement avec l'ID du paiement
                return RedirectToAction("Confirmation", new { paymentId });
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Méthode pour créer un nouveau paiement.
    /// </summary>
    /// <param name="bookingId">L'identifiant de la réservation pour laquelle le paiement doit être créé.</param>
    /// <returns>Une vue avec le modèle de vue de paiement initialisé.</returns>
    [Authorize]
    public IActionResult Create(int bookingId)
    {
        try
        {
            Booking booking;

            using (var bookingDAL = new BookingDAL(_httpContextAccessor))
            {
                // Récupérer les détails de la réservation
                booking = bookingDAL.GetBookingById(bookingId);
                if (booking == null)
                {
                    return NotFound("Booking introuvable");

                }
            }

            // Initialiser le modèle de vue de paiement
            PaymentViewModel model = new PaymentViewModel
            {
                BookingId = bookingId,
                TotalAmount = booking.Price,
                FirstName = booking.User.FirstName,
                LastName = booking.User.LastName,
                Email = booking.User.Email,
                PackageName = booking.Package.Name,
                Description = booking.Package.Description
            };

            return View(model);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Action pour afficher la page de confirmation d'un paiement.
    /// </summary>
    /// <param name="paymentId">ID du paiement.</param>
    /// <returns>Vue contenant la confirmation du paiement.</returns>
    [Authorize]
    public IActionResult Confirmation(int paymentId)
    {
        try
        {
            // Récupérer l'ID de l'utilisateur connecté depuis le contexte HTTP
            int userId = int.Parse(HttpContext.User.Identity.Name);

            using (var paymentDAL = new PaymentDAL(_httpContextAccessor))
            {
                // Récupérer le paiement en utilisant l'ID de paiement
                Payment payment = paymentDAL.GetPaymentById(paymentId);

                // Vérifier si le paiement existe et appartient à l'utilisateur
                if (payment == null || payment.UserId != userId)
                {
                    return NotFound("Paiment introuvable");
                }

                // Créer le modèle de vue pour la confirmation du paiement
                var model = new ConfirmationPayViewModel
                {
                    PaymentId = payment.Id,
                    TotalAmount = payment.TotalAmount,
                    PaymentDate = payment.PaymentDate,
                    NumCB = payment.NumCB
                };

                return View(model);
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Action pour supprimer un paiement.
    /// </summary>
    /// <param name="paymentId">ID du paiement à supprimer.</param>
    /// <returns>Redirige vers la page de la liste des paiements en cas de succès, sinon renvoie un code d'erreur.</returns>
    [Authorize]
    public IActionResult Delete(int paymentId)
    {
        try
        {
            // Récupérer l'ID de l'utilisateur connecté depuis le contexte HTTP
            int userId = int.Parse(HttpContext.User.Identity.Name);

            using (var paymentDAL = new PaymentDAL(_httpContextAccessor))
            {
                // Récupérer le paiement en utilisant l'ID de paiement
                Payment payment = paymentDAL.GetPaymentById(paymentId);

                // Vérifier si le paiement existe et appartient à l'utilisateur
                if (payment == null || payment.UserId != userId)
                {
                    return NotFound("Paiment introuvable");
                }

                // Supprimer le paiement
                paymentDAL.DeletePayment(paymentId);

                return RedirectToAction("List");
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Action pour afficher la liste des paiements de l'utilisateur.
    /// </summary>
    /// <returns>Vue contenant la liste des paiements de l'utilisateur.</returns>
    [Authorize]
    public IActionResult List()
    {
        try
        {
            // Récupérer l'ID de l'utilisateur connecté depuis le contexte HTTP
            int userId = int.Parse(HttpContext.User.Identity.Name);

            using (var paymentDAL = new PaymentDAL(_httpContextAccessor))
            {
                // Récupérer les paiements de l'utilisateur
                var payments = paymentDAL.GetUserPayments(userId);

                return View(payments);
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}