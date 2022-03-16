using System;
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

        public IActionResult All([FromQuery] AllApartmentsSearchModel query)
        {
            var apartmentsQuery = this._db
                .Apartments
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.ApartmentType))
            {
                apartmentsQuery = apartmentsQuery
                    .Where(a => a.ApartmentType == query.ApartmentType);
            }

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                apartmentsQuery = apartmentsQuery
                    .Where(a => (a.ApartmentType + " " + a.Location).ToLower()
                        .Contains(query.SearchTerm.ToLower())
                        || a.Description.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            apartmentsQuery = query.ApartmentSorting switch
            {
                ApartmentSortingEnum.Year => apartmentsQuery.OrderByDescending(a => a.Year),
                ApartmentSortingEnum.Location => apartmentsQuery.OrderByDescending(a => a.Location),
                ApartmentSortingEnum.ApartmentType => apartmentsQuery.OrderByDescending(a => a.ApartmentType),
                _=> apartmentsQuery.OrderByDescending(a => a.Id)
            };

            var apartments =
                apartmentsQuery
                    .Select(a => new ApartmentListingViewModel
                    {
                        Id = a.Id,
                        ApartmentType = a.ApartmentType,
                        Location = a.Location,
                        ImageUrl = a.ImageUrl,
                        Year = a.Year,
                        Category = a.Category.Name,
                    }).ToList();

            var apartmentTypes = this._db
                .Apartments
                .Select(a => a.ApartmentType)
                .Distinct()
                .ToList();

            query.ApartmentTypes = apartmentTypes;
            query.Apartments = apartments;

            return View(query);
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
