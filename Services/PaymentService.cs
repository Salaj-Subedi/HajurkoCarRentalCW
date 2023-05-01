using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HajurKoCarRental.Models;
using HajurKoCarRental.Data;
using HajurKoCarRental.Models.DataModels;

namespace HajurKoCarRental.Services
{
    public class PaymentService
    {
        private readonly ApplicationDbContext _context;

        public PaymentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> ProcessPaymentAsync(Payment payment)
        {
            if (payment == null)
            {
                throw new ArgumentNullException(nameof(payment));
            }

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return payment;
        }

        public async Task<Payment> GetPaymentByIdAsync(int paymentId)
        {
            var payment = await _context.Payments.FindAsync(paymentId);

            if (payment == null)
            {
                return null;
            }

            return payment;
        }

        //public async Task<IEnumerable<Payment>> GetPaymentHistoryAsync(string userId)
        //{
            //if (string.IsNullOrWhiteSpace(userId))
            //{
                //throw new ArgumentNullException(nameof(userId));
            //}

            //var payments = _context.Payments.Where(p => p.UserId == userId);

            //return await Task.FromResult(payments.ToList());
        }
    //}
}
