using ApartmentRentingSystem.Data;
using ApartmentRentingSystem.Data.Models;
using ApartmentRentingSystem.Services;
using ApartmentRentingSystem.Services.Apartments;
using ApartmentRentingSystem.Services.Brokers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace ApartmentRentingSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApartmentRentingDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<User>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;

                    
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApartmentRentingDbContext>();



            services.AddAutoMapper(typeof(Startup));

            // Adds AntiForgeryToken to the entire solution - prevents SQL injections, XSS...
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>();
            });

            services.AddTransient<IStatsService, StatsService>();
            services.AddTransient<IApartmentsService, ApartmentsService>();
            services.AddTransient<IBrokerService, BrokerService>();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.PrepareDB();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
               
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }
    }
}
