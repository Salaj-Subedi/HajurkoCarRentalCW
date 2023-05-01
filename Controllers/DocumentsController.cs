using System;
using System.IO;
using System.Threading.Tasks;
using HajurKoCarRental.Models;
using HajurKoCarRental.Models.DataModels;
using HajurKoCarRental.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HajurKoCarRental.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DocumentsController : Controller
    {
        private readonly DocumentService _documentService;
        private readonly UserManager<ApplicationUser> _userManager;

        public DocumentsController(DocumentService documentService, UserManager<ApplicationUser> userManager)
        {
            _documentService = documentService;
            _userManager = userManager;
        }

        [HttpGet("Documents/Upload")]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost("Documents/Upload")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(string documentType, IFormFile documentFile)
        {
            if (documentFile == null || documentFile.Length == 0)
            {
                ModelState.AddModelError("FileError", "Please select a file.");
                return View();
            }

            var user = await _userManager.GetUserAsync(User);
            var document = new Document
            {
                Id = Guid.NewGuid().ToString(),
                UserId = user.Id,
                DocumentType = documentType,
            };

            using var fileStream = documentFile.OpenReadStream();
            await _documentService.UploadDocumentAsync(document, fileStream);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Documents/ViewDocument/{documentType}")]
        public async Task<IActionResult> ViewDocument(string documentType)
        {
            var user = await _userManager.GetUserAsync(User);
            var document = await _documentService.GetDocumentAsync(user.Id, documentType);

            if (document == null)
            {
                return NotFound();
            }

            return File(document.DocumentPath, "application/pdf");
        }
    }
}
