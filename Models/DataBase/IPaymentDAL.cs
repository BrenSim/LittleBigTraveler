using LittleBigTraveler.Models.UserClasses;
using System;
using System.Collections.Generic;

public interface IPaymentDAL : IDisposable
{
    int CreatePayment(int userId, int bookingId, double totalAmount, int numCB);
    void DeletePayment(int paymentId);
    Payment GetPaymentById(int paymentId);
    List<Payment> GetUserPayments(int userId);
}
