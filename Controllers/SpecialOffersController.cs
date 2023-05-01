using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HajurKoCarRental.Models.DataModels;
using HajurKoCarRental.Services;
using HajurKoCarRental.Models.ViewModels;

namespace HajurKoCarRental.Controllers
{
    [Authorize(Roles = "Admin, Staff")]
    [Route("SpecialOffers")]
    public class SpecialOffersController : Controller
    {
        private readonly SpecialOfferService _specialOfferService;

        public SpecialOffersController(SpecialOfferService specialOfferService)
        {
            _specialOfferService = specialOfferService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var specialOffers = await _specialOfferService.GetAllSpecialOffersAsync();
            return View(specialOffers);
        }

        [HttpGet("Add")]
        public IActionResult AddSpecialOffer()
        {
            return View();
        }

        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSpecialOffer(SpecialOfferViewModel model)
        {
            if (ModelState.IsValid)
            {
                var specialOffer = new SpecialOffer
                {
                    Title = model.Title,
                    Description = model.Description,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    DiscountPercentage = model.DiscountPercent
                };

                await _specialOfferService.AddSpecialOfferAsync(specialOffer);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            var specialOffer = await _specialOfferService.GetSpecialOfferByIdAsync(id);
            if (specialOffer == null)
            {
                return NotFound();
            }

            var model = new SpecialOfferViewModel
            {
                Id = specialOffer.Id,
                Title = specialOffer.Title,
                Description = specialOffer.Description,
                StartDate = specialOffer.StartDate,
                EndDate = specialOffer.EndDate,
                DiscountPercent = specialOffer.DiscountPercentage
            };

            return View(model);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SpecialOfferViewModel model)
        {
            if (ModelState.IsValid)
            {
                var specialOffer = new SpecialOffer
                {
                    Id = model.Id,
                    Title = model.Title,
                    Description = model.Description,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    DiscountPercentage = model.DiscountPercent
                };

                await _specialOfferService.UpdateSpecialOfferAsync(specialOffer);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var specialOffer = await _specialOfferService.GetSpecialOfferByIdAsync(id);
            if (specialOffer == null)
            {
                return NotFound();
            }

            return View(specialOffer);
        }

        [HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _specialOfferService.DeleteSpecialOfferAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
