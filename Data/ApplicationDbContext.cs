using System;
using System.Collections.Generic;
using System.Text;
using HajurKoCarRental.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HajurKoCarRental.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<RentalRequest> RentalRequests { get; set; }
        public DbSet<DamageRequest> DamageRequests { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<SpecialOffer> SpecialOffers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ImageModel> Images { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Car model
            builder.Entity<Car>()
                .HasIndex(c => c.LicensePlate)
                .IsUnique();

            // RentalRequest model
            // One-to-many relationship between Car and RentalRequest
            builder.Entity<RentalRequest>()
                .HasOne(rr => rr.Car)
                .WithMany(c => c.RentalRequests)
                .HasForeignKey(rr => rr.CarId);

            // One-to-many relationship between ApplicationUser (Customer) and RentalRequest
            builder.Entity<RentalRequest>()
                .HasOne(rr => rr.Customer)
                .WithMany(c => c.RentalRequests)
                .HasForeignKey(rr => rr.CustomerId);

            // Many-to-one relationship between RentalRequest and ApplicationUser (AuthorizedBy)
            builder.Entity<RentalRequest>()
                .HasOne(rr => rr.AuthorizedBy)
                .WithMany()
                .HasForeignKey(rr => rr.AuthorizedById)
                .IsRequired(false);

            // DamageRequest model
            // One-to-many relationship between ApplicationUser (Customer) and DamageRequest
            builder.Entity<DamageRequest>()
                .HasOne(dr => dr.Customer)
                .WithMany(c => c.DamageRequests)
                .HasForeignKey(dr => dr.CustomerId);

            // One-to-many relationship between Car and DamageRequest
            builder.Entity<DamageRequest>()
                .HasOne(dr => dr.Car)
                .WithMany(c => c.DamageRequests)
                .HasForeignKey(dr => dr.CarId);

            // Document model
            // One-to-many relationship between ApplicationUser and Document
            builder.Entity<Document>()
                .HasOne(d => d.User)
                .WithMany(u => u.Documents)
                .HasForeignKey(d => d.UserId);

            // SpecialOffer model
            // One-to-many relationship between ApplicationUser (Customer) and SpecialOffer
            builder.Entity<SpecialOffer>()
                .HasOne(so => so.Customer)
                .WithMany(c => c.SpecialOffers)
                .HasForeignKey(so => so.CustomerId);

            // Payment model
            // One-to-many relationship between RentalRequest and Payment
            builder.Entity<Payment>()
                .HasOne(p => p.RentalRequest)
                .WithMany(rr => rr.Payments)
                .HasForeignKey(p => p.RentalRequestId);
        }
    }
}
