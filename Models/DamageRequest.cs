namespace HajurKoCarRental.Models
{
    public class DamageRequest
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public ApplicationUser Customer { get; set; }
        public string CarId { get; set; }
        public Car Car { get; set; }
        public string Description { get; set; }
        public decimal RepairCost { get; set; }
        public bool IsPaid { get; set; }
    }
}
