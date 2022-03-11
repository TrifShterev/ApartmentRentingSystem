using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApartmentRentingSystem.Data
{
    public class ApartmentRentingDbContext : IdentityDbContext
    {
        public ApartmentRentingDbContext(DbContextOptions<ApartmentRentingDbContext> options)
            : base(options)
        {
        }

        public DbSet<Apartment> Apartments { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Apartment>()
                .HasOne(a => a.Category)
                .WithMany(c => c.Apartments)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
