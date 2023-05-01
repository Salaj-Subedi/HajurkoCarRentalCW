using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HajurKoCarRental.Services;
using HajurKoCarRental.Models.DataModels;

namespace HajurKoCarRental.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _service;

        public AdminController(IAdminService Service)
        {
            _service = Service;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ManageUsers()
        {
            var users = await _service.GetAllUsersAsync();
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _service.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(string id, ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.UpdateUserAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(ManageUsers));
                }
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _service.DeleteUserAsync(id);
            return RedirectToAction(nameof(ManageUsers));
        }

        // Implement other actions for managing user roles, system reports, and application health.
    }
}
