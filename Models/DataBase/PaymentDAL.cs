using LittleBigTraveler.Models.DataBase;
using LittleBigTraveler.Models.UserClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

public class PaymentDAL : IPaymentDAL
{
    private BddContext _bddContext;
    private IHttpContextAccessor _httpContextAccessor;

    public PaymentDAL(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
        _bddContext = new BddContext();
    }

    public void Dispose()
    {
        _bddContext.Dispose();
    }

    /// <summary>
    /// Crée un nouveau paiement.
    /// </summary>
    /// <param name="userId">L'ID de l'utilisateur.</param>
    /// <param name="bookingId">L'ID de la réservation.</param>
    /// <param name="numCB">Le numéro de carte bancaire.</param>
    /// <returns>L'ID du paiement créé.</returns>

    public int CreatePayment(int userId, int bookingId, int numCB)
    {
        var user = _bddContext.Users.FirstOrDefault(u => u.Id == userId);
        var booking = _bddContext.Bookings.FirstOrDefault(b => b.Id == bookingId);

        if (user != null && booking != null)
        {
            Payment payment = new Payment()
            {
                TotalAmount = booking.Price,
                PaymentDate = DateTime.Now,
                NumCB = numCB,
                UserId = userId,
                BookingId = bookingId
            };

            _bddContext.Payments.Add(payment);
            _bddContext.SaveChanges();

            booking.IsConfirmed = true;
            _bddContext.SaveChanges();

            return payment.Id;
        }
        else
        {
            throw new Exception("Utilisateur ou réservation introuvable.");
        }
    }

    /// <summary>
    /// Supprime un paiement.
    /// </summary>
    /// <param name="paymentId">L'ID du paiement à supprimer.</param>
    public void DeletePayment(int paymentId)
    {
        var payment = _bddContext.Payments.FirstOrDefault(p => p.Id == paymentId);

        if (payment != null)
        {
            _bddContext.Payments.Remove(payment);
            _bddContext.SaveChanges();
        }
        else
        {
            throw new Exception("Paiement introuvable.");
        }
    }

    /// <summary>
    /// Récupère un paiement par son ID.
    /// </summary>
    /// <param name="paymentId">L'ID du paiement à récupérer.</param>
    /// <returns>Le paiement avec l'ID spécifié, ou null s'il n'est pas trouvé.</returns>
    public Payment GetPaymentById(int paymentId)
    {
        return _bddContext.Payments
            .Include(p => p.User)
            .Include(p => p.Booking)
            .FirstOrDefault(p => p.Id == paymentId);
    }

    /// <summary>
    /// Récupère tous les paiements d'un utilisateur.
    /// </summary>
    /// <param name="userId">L'ID de l'utilisateur.</param>
    /// <returns>Une liste des paiements de l'utilisateur.</returns>
    public List<Payment> GetUserPayments(int userId)
    {
        return _bddContext.Payments
            .Include(p => p.User)
            .Include(p => p.Booking)
            .Where(p => p.UserId == userId)
            .ToList();
    }
}