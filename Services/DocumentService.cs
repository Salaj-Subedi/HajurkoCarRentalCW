using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HajurKoCarRental.Data;
using HajurKoCarRental.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace HajurKoCarRental.Services
{
    public class DocumentService
    {
        private readonly ApplicationDbContext _context;
        private readonly string _documentUploadPath;

        public DocumentService(ApplicationDbContext context)
        {
            _context = context;
            _documentUploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "documents");
        }

        public async Task<Document> UploadDocumentAsync(Document document, Stream fileStream)
        {
            if (!Directory.Exists(_documentUploadPath))
            {
                Directory.CreateDirectory(_documentUploadPath);
            }

            var fileName = $"{document.Id}_{document.DocumentType}.pdf";
            var filePath = Path.Combine(_documentUploadPath, fileName);

            using (var file = File.Create(filePath))
            {
                await fileStream.CopyToAsync(file);
            }

            document.DocumentPath = $"/documents/{fileName}";
            document.UploadDate = DateTime.UtcNow;

            _context.Documents.Add(document);
            await _context.SaveChangesAsync();

            return document;
        }

        public async Task<Document> GetDocumentAsync(string userId, string documentType)
        {
            return await _context.Documents
                .Where(d => d.UserId == userId && d.DocumentType == documentType)
                .OrderByDescending(d => d.UploadDate)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> UserHasDocumentAsync(string userId, string documentType)
        {
            return await _context.Documents.AnyAsync(d => d.UserId == userId && d.DocumentType == documentType);
        }
    }
}
