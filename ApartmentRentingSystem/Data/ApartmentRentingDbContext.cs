using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using ApartmentRentingSystem.Data.Models;
using Microsoft.AspNetCore.Identity;
using ApartmentRentingSystem.Models.Apartments;

namespace ApartmentRentingSystem.Data
{
    public class ApartmentRentingDbContext : IdentityDbContext<User>
    {
        public ApartmentRentingDbContext(DbContextOptions<ApartmentRentingDbContext> options)
            : base(options)
        {
        }

        public DbSet<Apartment> Apartments { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Broker> Brokers { get; set; }

        //Orders related tables
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Apartment>()
                .HasOne(a => a.Category)
                .WithMany(c => c.Apartments)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            //One to many relation(one broker can have many apartments)
            builder
                .Entity<Apartment>()
                .HasOne(a => a.Broker)
                .WithMany(b => b.Apartments)
                .HasForeignKey(a => a.BrokerId)
                .OnDelete(DeleteBehavior.Restrict);

            //ONe to One connection between Broker and User(Every Broker is a user)
            builder
                .Entity<Broker>()
                .HasOne<User>()
                .WithOne()
                .HasForeignKey<Broker>(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);
                


            base.OnModelCreating(builder);
        }

       // public DbSet<ApartmentDetailsModel> ApartmentDetailsModel { get; set; }
    }
}
