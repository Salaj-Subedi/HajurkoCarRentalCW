namespace HajurKoCarRental.Models.DataModels
{
    public class SpecialOffer
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string CustomerId { get; set; }
        public ApplicationUser Customer { get; set; }
        public double DiscountPercentage { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
