using System.ComponentModel.DataAnnotations;
using HajurKoCarRental.Models;

namespace HajurKoCarRental.Models.ViewModels
{
    public class PaymentViewModel
    {
        [Required]
        public string RentalRequestId { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.00")]
        public decimal Amount { get; set; }

        //[Required]
        //[Display(Name = "Payment Method")]
        //public PaymentMethod PaymentMethod { get; set; }
    }
}
