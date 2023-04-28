namespace HajurKoCarRental.Models
{
    public class Car
    {
        public string Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal DailyRate { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAvailable { get; set; }
        public string LicensePlate { get; set; }

        // Navigation properties
        public ICollection<RentalRequest> RentalRequests { get; set; }
        public ICollection<DamageRequest> DamageRequests { get; set; }
    }
}
