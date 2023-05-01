namespace HajurKoCarRental.Models.DataModels
{
    public class Payment
    {
        public string Id { get; set; }
        public string RentalRequestId { get; set; }
        public RentalRequest RentalRequest { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } // e.g., CreditCard, PayPal, etc
    }
}
