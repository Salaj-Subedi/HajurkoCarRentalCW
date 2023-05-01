using System.ComponentModel.DataAnnotations;

namespace HajurKoCarRental.Models.ViewModels
{
    public class CarViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [Display(Name = "Make")]
        public string Make { get; set; }

        [Required]
        [Display(Name = "Model")]
        public string Model { get; set; }

        [Required]
        [Range(1900, 2100)]
        [Display(Name = "Year")]
        public int Year { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        [Display(Name = "Daily Rate")]
        public decimal DailyRate { get; set; }

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; }

        [Required]
        [Display(Name = "License Plate")]
        public string LicensePlate { get; set; }
    }
}
