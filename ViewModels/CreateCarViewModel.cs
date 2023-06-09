﻿namespace HajurKoCarRental.ViewModels
{
    public class CreateCarViewModel
    {
        public string Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public decimal DailyRate { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAvailable { get; set; }
        public string LicensePlate { get; set; }
    }
}
