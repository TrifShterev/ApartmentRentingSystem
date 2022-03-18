using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using ApartmentRentingSystem.Data;
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


            return View(new AddApartmentFormModel()
            {
                Categories = _apartmentsService.GetApartmentCategories()
            });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddApartmentFormModel apartment)
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

            var apartmentId = _apartmentsService.AddApartment(apartment, brokerId);


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


       
       
    }

    
}
