
using System;
using System.Linq;
using System.Threading.Tasks;
using ApartmentRentingSystem.Data;
using ApartmentRentingSystem.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using static ApartmentRentingSystem.Utilities.Constants.WebConstants;




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
        SeedAdmin(serviceProvider);

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

    private static void SeedAdmin( IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<User>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

       //This code will run asynchronous despite the method is not asynchronous
       Task.Run(async () =>
           {
               if (await roleManager.RoleExistsAsync(AdminRoleName))
               {
                   return;
               }

               var role = new IdentityRole
               {
                   Name = AdminRoleName
               };

               await roleManager.CreateAsync(role);

               const string adminEmail = "admin@ars.com";
               const string adminPassword = "admin12";

               var user = new User
               {
                   Email = adminEmail,
                   UserName = adminEmail,
                   FullName = "Admin"
               };

               await userManager.CreateAsync(user,adminPassword);

               await userManager.AddToRoleAsync(user, role.Name);

           })
           .GetAwaiter()
           .GetResult();
    }
}