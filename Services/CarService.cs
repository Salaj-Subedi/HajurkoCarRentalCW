using HajurKoCarRental.Data;
using HajurKoCarRental.Models.DataModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HajurKoCarRental.Services
{
    // Define the interface for the car service with necessary methods
    public interface ICarService
    {
        Task<IEnumerable<Car>> GetAllCarsAsync();
        Task<Car> GetCarByIdAsync(string id);
        Task<Car> CreateCarAsync(Car car);
        Task<Car> UpdateCarAsync(Car car);
        Task DeleteCarAsync(string id);
    }

    // Implement the car service using the ApplicationDbContext
    public class CarService : ICarService
    {
        // Hold a reference to the ApplicationDbContext for querying and updating the database
        private readonly ApplicationDbContext _context;

        // Initialize the CarService with the ApplicationDbContext using dependency injection
        public CarService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Method for retrieving all cars from the database
        public async Task<IEnumerable<Car>> GetAllCarsAsync()
        {
            return await _context.Cars.ToListAsync();
            
        }

        // Method for retrieving a car from the database by its ID
        public async Task<Car> GetCarByIdAsync(string id)
        {
            return await _context.Cars.FindAsync(id);
        }

        // Method for creating a new car and saving it to the database
        public async Task<Car> CreateCarAsync(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();
            return car;
        }

        // Method for updating an existing car in the database
        public async Task<Car> UpdateCarAsync(Car car)
        {
            _context.Entry(car).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return car;
        }

        // Method for deleting a car from the database by its ID
        public async Task DeleteCarAsync(string id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }
        }
    }
}
