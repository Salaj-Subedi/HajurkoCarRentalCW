using Microsoft.AspNetCore.Mvc;
using HajurKoCarRental.Models;
using HajurKoCarRental.Data;

namespace HajurKoCarRental.Controllers
{
    public class ImageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ImageController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Invalid file");
            }

            byte[] imageData;

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                imageData = stream.ToArray();
            }

            var image = new ImageModel
            {
                FileName = file.FileName,
                Data = imageData
            };

            _context.Images.Add(image);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        public IActionResult ViewImage()
        {
            var images = _context.Images.ToList();
            return View(images);
        }

        public IActionResult Image()
        {
            return View();
        }


        public IActionResult GetImage(int id)
        {
            var image = _context.Images.FirstOrDefault(i => i.Id == id);
            if (image == null)
            {
                return NotFound();
            }
            return File(image.Data, "image/jpg");
        }


    }
}
