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

    // Création d'un paiement
    public int CreatePayment(int userId, int bookingId, double totalAmount, int numCB)
    {
        var user = _bddContext.Users.FirstOrDefault(u => u.Id == userId);
        var booking = _bddContext.Bookings.FirstOrDefault(b => b.Id == bookingId);

        if (user != null && booking != null)
        {
            Payment payment = new Payment()
            {
                TotalAmount = totalAmount,
                PaymentDate = DateTime.Now,
                NumCB = numCB, // Numéro de carte bancaire
                UserId = userId,
                BookingId = bookingId
            };

            _bddContext.Payments.Add(payment);
            _bddContext.SaveChanges();

            // Marquer la réservation comme validée
            booking.IsConfirmed = true;
            _bddContext.SaveChanges();

            return payment.Id;
        }
        else
        {
            throw new Exception("User or Booking not found");
        }
    }

    // Suppression d'un paiement
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
            throw new Exception("Payment not found");
        }
    }

    // Récupération d'un paiement par ID
    public Payment GetPaymentById(int paymentId)
    {
        return _bddContext.Payments
            .Include(p => p.User)
            .Include(p => p.Booking)
            .FirstOrDefault(p => p.Id == paymentId);
    }

    // Récupération de tous les paiements d'un utilisateur
    public List<Payment> GetUserPayments(int userId)
    {
        return _bddContext.Payments
            .Include(p => p.User)
            .Include(p => p.Booking)
            .Where(p => p.UserId == userId)
            .ToList();
    }
}
