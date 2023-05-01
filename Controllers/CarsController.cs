using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HajurKoCarRental.Models.DataModels;
using HajurKoCarRental.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using HajurKoCarRental.Models.ViewModels;

namespace HajurKoCarRental.Controllers
{
    // This controller handles all the API endpoints related to cars
    public class CarsController : Controller
    {
        // Dependency injection of the CarService to perform operations on car data
        private readonly ICarService _service;

        public CarsController(ICarService service)
        {
            _service = service;
        }

        // This action returns a list of all available cars
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            // Retrieve all cars from the database using the CarService
            var cars = await _service.GetAllCarsAsync();
            // Render the list of cars in the view
            return View(cars);
            //var html = "<div>All Cars.</div>";
            //return base.Content(html, "text/html");

        }

        // GET: api/Cars/AddCar
        // This action renders the form to add a new car
        // Accessible only to users with Admin or Staff roles
        [HttpGet]
        [Authorize(Roles = "Admin, Staff")]
        public IActionResult AddCar()
        {
            // Render the AddCar view
            return View();
        }

        // POST: api/Cars/AddCar
        // This action handles the form submission for adding a new car
        // Accessible only to users with Admin or Staff roles
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> AddCar(CarViewModel model)
        {
            // Check if the submitted form data is valid
            if (ModelState.IsValid)
            {
                // Create a new Car object from the submitted form data
                var car = new Car
                {
                    Id = model.Id,
                    Make = model.Make,
                    Model = model.Model,
                    Year = model.Year,
                    DailyRate = model.DailyRate,
                    ImageUrl = model.ImageUrl,
                    IsAvailable = model.IsAvailable,
                    LicensePlate = model.LicensePlate
                };

                // Add the new car to the database using the CarService
                await _service.CreateCarAsync(car);
                // Redirect to the list of cars
                return RedirectToAction(nameof(Index));
            }

            // If form data is not valid, render the AddCar view again with the submitted data
            return View(model);
        }

        // GET: api/Cars/DeleteCar/{id}
        // This action renders the form to delete a car
        // Accessible only to users with Admin or Staff roles
        [HttpGet]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> DeleteCar(string id)
        {
            // Check if the car ID is provided
            if (id == null)
            {
                return NotFound();
            }

            // Retrieve the car with the given ID using the CarService
            var car = await _service.GetCarByIdAsync(id);

            // If the car is not found, return a NotFound result
            if (car == null)
            {
                return NotFound();
            }

            // Render the DeleteCar view with the car data
            return View(car);
        }

        // POST: api/Cars/DeleteCar/{id}
        // This action handles the form submission for deleting a car
        // Accessible only to
        // POST: api/Cars/DeleteCar/{id}
        // This action handles the form submission for deleting a car
        // Accessible only to users with Admin or Staff roles
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> DeleteCarConfirmed(string id)
        {
            // Delete the car with the given ID using the CarService
            await _service.DeleteCarAsync(id);
            // Redirect to the list of cars
            return RedirectToAction(nameof(Index));
        }
    }
}
