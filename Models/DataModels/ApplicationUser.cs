using Microsoft.AspNetCore.Identity;

namespace HajurKoCarRental.Models.DataModels
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        // Navigation properties
        public ICollection<Document> Documents { get; set; }
        public ICollection<RentalRequest> RentalRequests { get; set; }
        public ICollection<DamageRequest> DamageRequests { get; set; }
        public ICollection<SpecialOffer> SpecialOffers { get; set; }
    }
}
