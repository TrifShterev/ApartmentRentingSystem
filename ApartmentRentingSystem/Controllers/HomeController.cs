using ApartmentRentingSystem.Models;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ApartmentRentingSystem.Data;
using ApartmentRentingSystem.Models.Home;

namespace ApartmentRentingSystem.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApartmentRentingDbContext _db;

        public HomeController(ApartmentRentingDbContext _db)
            => this._db = _db;

      

        public IActionResult Index()
        {

            var allApartments = this._db.Apartments.Count();

            var apartments =
                this._db.Apartments
                    .OrderByDescending(a => a.Id)
                    .Select(a => new ApartmentIndexViewModel()
                    {
                        Id = a.Id,
                        ApartmentType = a.ApartmentType,
                        Location = a.Location,
                        ImageUrl = a.ImageUrl,
                        Year = a.Year
                        
                    })
                    .Take(3)
                    .ToList();

            return View(new IndexViewModel
            {
                AllApartments = allApartments,
                Apartments = apartments
            });
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
