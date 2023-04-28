using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HajurKoCarRental.Data;
using HajurKoCarRental.Models;
using HajurKoCarRental.ViewModels;

namespace HajurKoCarRental.Controllers
{
    [Authorize]
    public class RentalRequestController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RentalRequestController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Display rental requests for admin and staff
        public async Task<IActionResult> Index()
        {
            var rentalRequests = await _context.RentalRequests.Include(r => r.Customer).Include(r => r.Car).ToListAsync();
            return View(rentalRequests);
        }

        // Display form to create rental request
        public IActionResult Create()
        {
            // You need to create a view model for creating a rental request and pass it to the view
            return View();
        }

        // Handle form submission for creating rental request
        [HttpPost]
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

                _context.RentalRequests.Add(rentalRequest);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // Validate rental request for staff and admin
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> ValidateRequest(int id)
        {
            var rentalRequest = await _context.RentalRequests.FindAsync(id);
            if (rentalRequest == null)
            {
                return NotFound();
            }

            // Implement rental request validation logic here
            return RedirectToAction(nameof(Index));
        }

        // Cancel rental request for customers
        public async Task<IActionResult> CancelRequest(int id)
        {
            var rentalRequest = await _context.RentalRequests.FindAsync(id);
            if (rentalRequest == null)
            {
                return NotFound();
            }

            // Implement rental request cancellation logic here
            return RedirectToAction(nameof(Index));
        }

        // Complete rental request for staff and admin
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> CompleteRequest(int id)
        {
            var rentalRequest = await _context.RentalRequests.FindAsync(id);
            if (rentalRequest == null)
            {
                return NotFound();
            }

            // Implement rental request completion logic here
            return RedirectToAction(nameof(Index));
        }

        // Display rental history for admin and staff
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> RentalHistory()
        {
            var rentalHistory = await _context.RentalRequests.Include(r => r.Customer).Include(r => r.Car).Where(r => r.IsApproved).ToListAsync();
            return View(rentalHistory);
        }
    }
}
