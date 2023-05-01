using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HajurKoCarRental.Data;
using HajurKoCarRental.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace HajurKoCarRental.Services
{
    // DamageRequestService class handles actions related to damage requests in the application
    public class DamageRequestService
    {
        private readonly ApplicationDbContext _context;

        // Constructor takes ApplicationDbContext as a parameter and initializes the _context field
        public DamageRequestService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Retrieves a list of all damage requests, including related car and customer information
        public async Task<List<DamageRequest>> GetDamageRequestsAsync()
        {
            return await _context.DamageRequests
                .Include(dr => dr.Car)
                .Include(dr => dr.Customer)
                .ToListAsync();
        }

        // Retrieves a damage request by its ID, including related car and customer information
        public async Task<DamageRequest> GetDamageRequestByIdAsync(string id)
        {
            return await _context.DamageRequests
                .Include(dr => dr.Car)
                .Include(dr => dr.Customer)
                .FirstOrDefaultAsync(dr => dr.Id == id);
        }

        // Adds a new damage request to the database, returns true if the operation was successful
        public async Task<bool> AddDamageRequestAsync(DamageRequest damageRequest)
        {
            _context.DamageRequests.Add(damageRequest);
            return await _context.SaveChangesAsync() > 0;
        }

        // Updates an existing damage request in the database, returns true if the operation was successful
        public async Task<bool> UpdateDamageRequestAsync(DamageRequest damageRequest)
        {
            _context.Entry(damageRequest).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        // Deletes a damage request by its ID, returns true if the operation was successful
        public async Task<bool> DeleteDamageRequestAsync(string id)
        {
            var damageRequest = await GetDamageRequestByIdAsync(id);
            if (damageRequest == null) return false;

            _context.DamageRequests.Remove(damageRequest);
            return await _context.SaveChangesAsync() > 0;
        }

        // Retrieves a list of unpaid damage requests, including related car and customer information
        public async Task<List<DamageRequest>> GetUnpaidDamageRequestsAsync()
        {
            return await _context.DamageRequests
                .Include(dr => dr.Car)
                .Include(dr => dr.Customer)
                .Where(dr => !dr.IsPaid)
                .ToListAsync();
        }

        // Checks if a customer has any unpaid damage requests, returns true if any unpaid requests are found
        public async Task<bool> CustomerHasUnpaidDamageRequestsAsync(string customerId)
        {
            return await _context.DamageRequests
                .AnyAsync(dr => dr.CustomerId == customerId && !dr.IsPaid);
        }
    }
}
