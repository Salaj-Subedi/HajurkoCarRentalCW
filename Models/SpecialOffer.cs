namespace HajurKoCarRental.Models
{
    public class SpecialOffer
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public ApplicationUser Customer { get; set; }
        public decimal DiscountPercentage { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
