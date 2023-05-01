using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HajurKoCarRental.Models.DataModels;
using HajurKoCarRental.Services;
using HajurKoCarRental.Models.ViewModels;

namespace HajurKoCarRental.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DamageRequestController : Controller
    {
        private readonly DamageRequestService _damageRequestService;

        public DamageRequestController(DamageRequestService damageRequestService)
        {
            _damageRequestService = damageRequestService;
        }

        // GET: api/DamageRequest
        [HttpGet]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> Index()
        {
            var damageRequests = await _damageRequestService.GetDamageRequestsAsync();
            return View(damageRequests);
        }

        // GET: api/DamageRequest/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: api/DamageRequest/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DamageRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var damageRequest = new DamageRequest
                {
                    CarId = model.CarId,
                    CustomerId = model.CustomerId,
                    Description = model.Description,
                    RepairCost = model.RepairCost,
                    IsPaid = false
                };

                await _damageRequestService.AddDamageRequestAsync(damageRequest);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: api/DamageRequest/Edit/{id}
        [HttpGet("Edit/{id}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> Edit(string id)
        {
            var damageRequest = await _damageRequestService.GetDamageRequestByIdAsync(id);
            if (damageRequest == null)
            {
                return NotFound();
            }

            var model = new DamageRequestViewModel
            {
                Id = damageRequest.Id,
                CarId = damageRequest.CarId,
                CustomerId = damageRequest.CustomerId,
                Description = damageRequest.Description,
                RepairCost = damageRequest.RepairCost,
                IsPaid = damageRequest.IsPaid
            };

            return View(model);
        }

        // POST: api/DamageRequest/Edit
        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> Edit(DamageRequestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var damageRequest = new DamageRequest
                {
                    Id = model.Id,
                    CarId = model.CarId,
                    CustomerId = model.CustomerId,
                    Description = model.Description,
                    RepairCost = model.RepairCost,
                    IsPaid = model.IsPaid
                };

                await _damageRequestService.UpdateDamageRequestAsync(damageRequest);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: api/DamageRequest/Delete/{id}
        [HttpGet("Delete/{id}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> Delete(string id)
        {
            var damageRequest = await _damageRequestService.GetDamageRequestByIdAsync(id);
            if (damageRequest == null)
            {
                return NotFound();
            }

            return View(damageRequest);
        }

        // POST: api/DamageRequest/Delete/{id}
        [HttpPost("Delete/{id}")]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _damageRequestService.DeleteDamageRequestAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}