
using System;
using System.Linq;
using ApartmentRentingSystem.Data;
using ApartmentRentingSystem.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;




public static class ApplicationBuilderExtension
{

    public static IApplicationBuilder PrepareDB(this IApplicationBuilder app)
    {
        using var scopedServices = app.ApplicationServices.CreateScope();
        var serviceProvider = scopedServices.ServiceProvider;

        var data = serviceProvider
            .GetRequiredService<ApartmentRentingDbContext>();

        data.Database.Migrate();

        SeedCategories(data);
        SeedAdmin(data, serviceProvider);

        return app;
    }

    private static void SeedCategories(ApartmentRentingDbContext data)
    {
        if (data.Categories.Any())
        {
            return;
        }

        data.Categories.AddRange(new[]
        {
            new Category{Name = "Economy"},

            new Category{Name ="MiddleSize"},

            new Category{Name ="Large"},

            new Category{Name = "Luxury"}
        });

        data.SaveChanges();
    }

    private static void SeedAdmin(ApartmentRentingDbContext data, IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<User>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();


    }
}