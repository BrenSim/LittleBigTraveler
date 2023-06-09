using System;

namespace LittleBigTraveler.ViewModels
{
    public class ConfirmationPayViewModel
    {
        public int PaymentId { get; set; }
        public double TotalAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public int NumCB { get; set; }
    }
}
