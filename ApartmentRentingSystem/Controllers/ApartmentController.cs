using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ApartmentRentingSystem.Data;
using ApartmentRentingSystem.Infrastructure;
using ApartmentRentingSystem.Models.Apartments;
using ApartmentRentingSystem.Services.Apartments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentRentingSystem.Controllers
{
    public class ApartmentController : Controller
    {
        private readonly ApartmentRentingDbContext _db;
        private readonly IApartmentsService _apartmentsService;
        public ApartmentController(ApartmentRentingDbContext db, IApartmentsService apartmentsService)
        {
            this._db = db;
            _apartmentsService = apartmentsService;
        }


        [Authorize]
        public IActionResult Add()
        {
            if (!this.UserIsBroker())
            {
                return RedirectToAction(nameof(BrokerController.Create), "Broker");
            }


            return View(new AddApartmentFormModel()
            {
                Categories = this.GetApartmentCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddApartmentFormModel apartment)
        {
            var brokerId = this._db
                .Brokers
                .Where(b=> b.UserId == this.User.GetId())
                .Select(b => b.Id)
                .FirstOrDefault();

            if (brokerId == 0)
            {
                return RedirectToAction(nameof(BrokerController.Create), "Broker");
            }

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
                CategoryId = apartment.CategoryId,
                BrokerId = brokerId

            };

            this._db.Apartments.Add(newApartment);
            this._db.SaveChanges();


            return RedirectToAction(nameof(All));
        }
        
        public IActionResult All([FromQuery] AllApartmentsSearchModel query)
        {


            var apartments = this._apartmentsService.GetAll(
                query.ApartmentType,
                query.SearchTerm,
                query.ApartmentSorting,
                query.CurrentPage,
                AllApartmentsSearchModel.ApartmentsPerPage);

            return View(apartments);
        }


        private bool UserIsBroker()
        {
            //takes the Id from the user via ClaimTypes(check GetId method in Infrastructure/ClaimsPrincipalExtension) other option is via UserManager
            var userId = this.User.GetId();

            return this._db
                .Brokers
                .Any(broker => broker.UserId == userId);
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
