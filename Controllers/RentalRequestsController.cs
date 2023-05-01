using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using HajurKoCarRental.Models;
using HajurKoCarRental.Services;
using HajurKoCarRental.Models.ViewModels;
using HajurKoCarRental.Models.DataModels;

namespace HajurKoCarRental.Controllers
{
    // RentalRequestController handles actions related to rental requests.
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RentalRequestController : Controller
    {
        private readonly RentalRequestService _rentalRequestService;
        private readonly CarService _carService;
        private readonly UserManager<ApplicationUser> _userManager;
        // Constructor initializes the rental request service, car service, and user manager.
        public RentalRequestController(RentalRequestService rentalRequestService, CarService carService, UserManager<ApplicationUser> userManager)
        {
            _rentalRequestService = rentalRequestService;
            _carService = carService;
            _userManager = userManager;
        }

        // GET: api/RentalRequest
        [HttpGet]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> Index()
        {
            var rentalRequests = await _rentalRequestService.GetAllRentalRequestsAsync();
            return View(rentalRequests);
        }

        // GET: api/RentalRequest/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            // You need to create a view model for creating a rental request and pass it to the view.
            return View();
        }

        // POST: api/RentalRequest/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RentalRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var rentalRequest = new RentalRequest
                {
                    CustomerId = user.Id,
                    CarId = model.CarId,
                    RequestDate = DateTime.UtcNow,
                    RentalStartDate = model.RentalStartDate,
                    RentalEndDate = model.RentalEndDate,
                    TotalCost = model.TotalCost,
                    IsApproved = false,
                    IsCancelled = false
                };

                await _rentalRequestService.AddRentalRequestAsync(rentalRequest);

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // POST: api/RentalRequest/ValidateRentalRequest/{id}
        [HttpPost("ValidateRentalRequest/{id}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> ValidateRentalRequest(string id)
        {
            var rentalRequest = await _rentalRequestService.GetRentalRequestByIdAsync(id);
            if (rentalRequest == null)
            {
                return NotFound();
            }

            // Implement rental request validation logic here.
            return RedirectToAction(nameof(Index));
        }

        // POST: api/RentalRequest/CancelRentalRequest/{id}
        [HttpPost("CancelRentalRequest/{id}")]
        public async Task<IActionResult> CancelRentalRequest(string id)
        {
            var rentalRequest = await _rentalRequestService.GetRentalRequestByIdAsync(id);
            if (rentalRequest == null)
            {
                return NotFound();
            }

            // Implement rental request cancellation logic here.
            return RedirectToAction(nameof(Index));
        }

        // POST: api/RentalRequest/CompleteRequest/{id}
        [HttpPost("CompleteRequest/{id}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> CompleteRequest(string id)
        {
            var rentalRequest = await _rentalRequestService.GetRentalRequestByIdAsync(id);
            if (rentalRequest == null)
            {
                return NotFound();
            }

            //
            // Implement rental request completion logic here.
            return RedirectToAction(nameof(Index));
        }

        // GET: api/RentalRequest/RentalHistory
        [HttpGet("RentalHistory")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> RentalHistory()
        {
            var rentalHistory = await _rentalRequestService.GetAllRentalRequestsAsync();
            return View(rentalHistory);
        }
    }
}