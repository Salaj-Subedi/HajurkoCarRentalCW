namespace HajurKoCarRental.Models.DataModels
{
    public class Document
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string DocumentType { get; set; } // e.g., DrivingLicense, CitizenshipPaper, etc.
        public string DocumentPath { get; set; }
        public DateTime UploadDate { get; set; }
    }
}
