using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HajurKoCarRental.Models;
using HajurKoCarRental.Models.ViewModels;
using HajurKoCarRental.Services;
using HajurKoCarRental.Models.DataModels;

namespace HajurKoCarRental.Controllers
{
    [Authorize(Roles = "Customer")]
    public class PaymentController : Controller
    {
        private readonly PaymentService _paymentService;
        private readonly RentalRequestService _rentalRequestService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PaymentController(PaymentService paymentService, RentalRequestService rentalRequestService, UserManager<ApplicationUser> userManager)
        {
            _paymentService = paymentService;
            _rentalRequestService = rentalRequestService;
            _userManager = userManager;
        }

        public async Task<IActionResult> InitiatePayment(string rentalRequestId)
        {
            var rentalRequest = await _rentalRequestService.GetRentalRequestByIdAsync(rentalRequestId);
            if (rentalRequest == null)
            {
                return NotFound();
            }

            var model = new PaymentViewModel
            {
                RentalRequestId = rentalRequest.Id,
                Amount = rentalRequest.TotalCost
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessPayment(PaymentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var payment = new Payment
                {
                    RentalRequestId = model.RentalRequestId,
                    //UserId = currentUser.Id,
                    Amount = model.Amount,
                    //PaymentMethod = model.PaymentMethod,
                    PaymentDate = DateTime.UtcNow
                };

                var result = await _paymentService.ProcessPaymentAsync(payment);

                return RedirectToAction(nameof(ConfirmPayment), new { paymentId = result.Id });
            }

            return View(model);
        }

        public async Task<IActionResult> ConfirmPayment(int paymentId)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(paymentId);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        //public async Task<IActionResult> ListPaymentHistory()
        //{
            //var currentUser = await _userManager.GetUserAsync(User);
            //var paymentHistory = await _paymentService.GetPaymentHistoryAsync(currentUser.Id);

            //return View(paymentHistory);
        //}
    }
}
