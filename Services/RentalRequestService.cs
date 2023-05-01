using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HajurKoCarRental.Data;
using HajurKoCarRental.Models.DataModels;

namespace HajurKoCarRental.Services
{
    // RentalRequestService class handles the business logic for rental requests.
    public class RentalRequestService
    {
        private readonly ApplicationDbContext _context;

        // Constructor initializes the service with the application's DbContext.
        public RentalRequestService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Retrieves a list of all rental requests, including related car, customer, and authorized by staff data.
        public async Task<List<RentalRequest>> GetAllRentalRequestsAsync()
        {
            return await _context.RentalRequests
                .Include(rr => rr.Car)
                .Include(rr => rr.Customer)
                .Include(rr => rr.AuthorizedBy)
                .ToListAsync();
        }

        // Retrieves a single rental request by ID, including related car, customer, and authorized by staff data.
        public async Task<RentalRequest> GetRentalRequestByIdAsync(string id)
        {
            return await _context.RentalRequests
                .Include(rr => rr.Car)
                .Include(rr => rr.Customer)
                .Include(rr => rr.AuthorizedBy)
                .FirstOrDefaultAsync(rr => rr.Id == id);
        }

        // Adds a new rental request to the database.
        public async Task AddRentalRequestAsync(RentalRequest rentalRequest)
        {
            _context.RentalRequests.Add(rentalRequest);
            await _context.SaveChangesAsync();
        }

        // Updates an existing rental request in the database.
        public async Task UpdateRentalRequestAsync(RentalRequest rentalRequest)
        {
            _context.Entry(rentalRequest).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Deletes an existing rental request from the database.
        public async Task DeleteRentalRequestAsync(string id)
        {
            var rentalRequest = await _context.RentalRequests.FindAsync(id);
            _context.RentalRequests.Remove(rentalRequest);
            await _context.SaveChangesAsync();
        }

        // Retrieves a list of rental requests for a specific customer by customer ID.
        public async Task<List<RentalRequest>> GetRentalRequestsByCustomerIdAsync(string customerId)
        {
            return await _context.RentalRequests
                .Include(rr => rr.Car)
                .Include(rr => rr.AuthorizedBy)
                .Where(rr => rr.CustomerId == customerId)
                .ToListAsync();
        }

        // Retrieves a list of rental requests within a specified date range.
        public async Task<List<RentalRequest>> GetRentalRequestsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.RentalRequests
                .Include(rr => rr.Car)
                .Include(rr => rr.Customer)
                .Include(rr => rr.AuthorizedBy)
                .Where(rr => rr.RequestDate >= startDate && rr.RequestDate <= endDate)
                .ToListAsync();
        }

        // Add other methods as needed for managing rental requests.
    }
}
