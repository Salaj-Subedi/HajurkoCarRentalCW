using System;
using System.ComponentModel.DataAnnotations;
using HajurKoCarRental.Models;

namespace HajurKoCarRental.Models.ViewModels
{
    public class RentalRequestViewModel
    {
        [Required]
        [Display(Name = "Car ID")]
        public string CarId { get; set; }

        [Required]
        [Display(Name = "Rental Start Date")]
        [DataType(DataType.Date)]
        public DateTime RentalStartDate { get; set; }

        [Required]
        [Display(Name = "Rental End Date")]
        [DataType(DataType.Date)]
        public DateTime RentalEndDate { get; set; }

        [Required]
        [Display(Name = "Total Cost")]
        [DataType(DataType.Currency)]
        public decimal TotalCost { get; set; }
    }
}
