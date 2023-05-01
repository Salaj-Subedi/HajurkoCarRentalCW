using System;
using System.ComponentModel.DataAnnotations;

namespace HajurKoCarRental.Models.ViewModels
{
    public class DamageRequestViewModel
    {
        public string Id { get; set; }

        [Required]
        public string CarId { get; set; }

        [Required]
        public string CustomerId { get; set; }

        [Required]
        public DateTime DamageDate { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        public decimal RepairCost { get; set; }

        public bool IsPaid { get; set; }
    }
}
