using System;
using System.ComponentModel.DataAnnotations;

namespace HajurKoCarRental.Models.ViewModels
{
    public class SpecialOfferViewModel
    {
        public string Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(1, 100)]
        public double DiscountPercent { get; set; }
    }
}
