using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HajurKoCarRental.Data;
using HajurKoCarRental.Models.DataModels;
using Microsoft.EntityFrameworkCore;

namespace HajurKoCarRental.Services
{
    public class SpecialOfferService
    {
        private readonly ApplicationDbContext _context;

        public SpecialOfferService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SpecialOffer>> GetAllSpecialOffersAsync()
        {
            return await _context.SpecialOffers.ToListAsync();
        }

        public async Task<SpecialOffer> GetSpecialOfferByIdAsync(string id)
        {
            return await _context.SpecialOffers.FindAsync(id);
        }

        public async Task<SpecialOffer> AddSpecialOfferAsync(SpecialOffer offer)
        {
            _context.SpecialOffers.Add(offer);
            await _context.SaveChangesAsync();
            return offer;
        }

        public async Task<SpecialOffer> UpdateSpecialOfferAsync(SpecialOffer offer)
        {
            _context.Entry(offer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return offer;
        }

        public async Task<SpecialOffer> DeleteSpecialOfferAsync(int id)
        {
            var offer = await _context.SpecialOffers.FindAsync(id);
            if (offer != null)
            {
                _context.SpecialOffers.Remove(offer);
                await _context.SaveChangesAsync();
            }
            return offer;
        }

        public bool OfferExists(string id)
        {
            return _context.SpecialOffers.Any(e => e.Id == id);
        }
    }
}
