
using System.Linq;
using ApartmentRentingSystem.Data;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;




public static class ApplicationBuilderExtension
{

    public static IApplicationBuilder PrepareDB(this IApplicationBuilder app)
    {
        using var scopedServices = app.ApplicationServices.CreateScope();

        var data = scopedServices
            .ServiceProvider
            .GetService<ApartmentRentingDbContext>();

        data.Database.Migrate();

        SeedCategories(data);

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
}