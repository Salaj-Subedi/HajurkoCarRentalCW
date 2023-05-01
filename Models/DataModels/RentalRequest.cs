namespace HajurKoCarRental.Models.DataModels
{
    public class RentalRequest
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public ApplicationUser Customer { get; set; }
        public string CarId { get; set; }
        public Car Car { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime RentalStartDate { get; set; }
        public DateTime RentalEndDate { get; set; }
        public decimal TotalCost { get; set; }
        public bool IsApproved { get; set; }
        public string AuthorizedById { get; set; }
        public ApplicationUser AuthorizedBy { get; set; }
        public bool IsCancelled { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}
