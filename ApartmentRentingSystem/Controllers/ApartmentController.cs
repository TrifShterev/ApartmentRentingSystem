using System.Linq;
using ApartmentRentingSystem.Infrastructure;
using ApartmentRentingSystem.Models.Apartments;
using ApartmentRentingSystem.Services.Apartments;
using ApartmentRentingSystem.Services.Brokers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentRentingSystem.Controllers
{
    public class ApartmentController : Controller
    {
        
        private readonly IApartmentsService _apartmentsService;
        private readonly IBrokerService _brokerService;

        public ApartmentController(IApartmentsService apartmentsService, IBrokerService brokerService)
        {
            
            _apartmentsService = apartmentsService;
            _brokerService = brokerService;
        }


        [Authorize]
        public IActionResult Add()
        {
            if (!_brokerService.UserIsBroker(this.User.GetId()))
            {
                return RedirectToAction(nameof(BrokerController.Create), "Broker");
            }


            return View(new ApartmentFormModel()
            {
                Categories = _apartmentsService.GetApartmentCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(ApartmentFormModel apartment)
        {
            var brokerId = _brokerService.BrokerId(this.User.GetId());

            if (brokerId == 0)
            {
                return RedirectToAction(nameof(BrokerController.Create), "Broker");
            }

            if (!_apartmentsService.GetApartmentCategories().Any(c => c.Id == apartment.CategoryId))
            {
                this.ModelState
                    .AddModelError(nameof(apartment.CategoryId), "Category doesn't exist!");
            }

            if (!ModelState.IsValid)
            {
                apartment.Categories = _apartmentsService.GetApartmentCategories();

                return View(apartment);
            }

            this._apartmentsService.AddApartment(apartment, brokerId);


            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            if (!_brokerService.UserIsBroker(userId))
            {
                return RedirectToAction(nameof(BrokerController.Create), "Broker");
            }

            var apartment = this._apartmentsService.Details(id);

            if (apartment.UserId != userId)
            {
                return Unauthorized();
            }


            return View(new ApartmentFormModel()
            {
                ApartmentType = apartment.ApartmentType,
                Location = apartment.Location,
                Description = apartment.Description,
                ImageUrl = apartment.ImageUrl,
                Year = apartment.Year,
                CategoryId = apartment.CategoryId,
                Categories = _apartmentsService.GetApartmentCategories()
            });
        }


        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id,ApartmentFormModel apartment)
        {
            var brokerId = _brokerService.BrokerId(this.User.GetId());

            if (brokerId == 0)
            {
                return RedirectToAction(nameof(BrokerController.Create), "Broker");
            }

            if (!_apartmentsService.GetApartmentCategories().Any(c => c.Id == apartment.CategoryId))
            {
                this.ModelState
                    .AddModelError(nameof(apartment.CategoryId), "Category doesn't exist!");
            }

            if (!ModelState.IsValid)
            {
                apartment.Categories = _apartmentsService.GetApartmentCategories();

                return View(apartment);
            }

            var apartmentIsEdited = this._apartmentsService.EditApartment(id, apartment, brokerId);

            if (!apartmentIsEdited)
            {
                return BadRequest();
            }

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


        [Authorize]
        public IActionResult Mine()
        {
            var myApartments = this._apartmentsService.ApartmentsOwnedByUser(this.User.GetId());

            return View(myApartments);
        }

    }

    
}
