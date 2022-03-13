using System.Collections.Generic;
using System.Linq;
using ApartmentRentingSystem.Data;
using ApartmentRentingSystem.Models.Apartments;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentRentingSystem.Controllers
{
    public class ApartmentController : Controller
    {
        private readonly ApartmentRentingDbContext _db;

        public ApartmentController(ApartmentRentingDbContext _db)
        => this._db = _db;

        public IActionResult Add()
            => View(new AddApartmentFormModel()
            {
                Categories = this.GetApartmentCategories()
            });

        [HttpPost]
        public IActionResult Add(AddApartmentFormModel apartment)
        {

            if (!this._db.Categories.Any(c => c.Id == apartment.CategoryId))
            {
                this.ModelState
                    .AddModelError(nameof(apartment.CategoryId), "Category doesn't exist!");
            }

            if (!ModelState.IsValid)
            {
                apartment.Categories = this.GetApartmentCategories();
            }

            var newApartment = new Apartment
            {

                ApartmentType = apartment.ApartmentType,
                Location = apartment.Location,
                ImageUrl = apartment.ImageUrl,
                Year = apartment.Year,
                Description = apartment.Description

            };

            this._db.Apartments.Add(newApartment);
            this._db.SaveChanges();


            return RedirectToAction("Index", "Home");
        }

        private IEnumerable<ApartmentCategoryViewModel> GetApartmentCategories()
            => this._db
                .Categories
                .Select(c => new ApartmentCategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                }).ToList();
    }

    
}
