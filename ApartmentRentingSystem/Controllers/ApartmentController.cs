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

                return View(apartment);
            }

            var newApartment = new Apartment
            {

                ApartmentType = apartment.ApartmentType,
                Location = apartment.Location,
                ImageUrl = apartment.ImageUrl,
                Year = apartment.Year,
                Description = apartment.Description,
                CategoryId = apartment.CategoryId

            };

            this._db.Apartments.Add(newApartment);
            this._db.SaveChanges();


            return RedirectToAction(nameof(All));
        }

        public IActionResult All()
        {
            var apartments =
                this._db.Apartments
                    .OrderByDescending(a => a.Id)
                    .Select(a => new ApartmentListingViewModel
                    {
                        Id = a.Id,
                        ApartmentType = a.ApartmentType,
                        Location = a.Location,
                        ImageUrl = a.ImageUrl,
                        Year = a.Year,
                        Category = a.Category.Name,
                    }).ToList();

            return View(apartments);
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
