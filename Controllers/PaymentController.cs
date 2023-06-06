using LittleBigTraveler.Models.DataBase;
using LittleBigTraveler.Models.UserClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using LittleBigTraveler.ViewModels;

public class PaymentController : Controller
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PaymentController(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    // Action pour afficher le formulaire de création d'un paiement
    public IActionResult Create(int bookingId, double totalAmount)
    {
        var model = new PaymentViewModel
        {
            BookingId = bookingId,
            TotalAmount = totalAmount
        };

        return View(model);
    }

    // Méthode pour traiter le formulaire de création d'un paiement
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
                int paymentId = paymentDAL.CreatePayment(userId, bookingId, totalAmount, numCB);

                // Rediriger vers la page de confirmation du paiement avec l'ID du paiement
                return RedirectToAction("Confirmation", new { paymentId });
            }
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // Action pour afficher la page de confirmation d'un paiement
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
                    return NotFound("Payment not found");
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

    // Action pour supprimer un paiement
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
                    return NotFound("Payment not found");
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

    // Action pour afficher la liste des paiements de l'utilisateur
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
