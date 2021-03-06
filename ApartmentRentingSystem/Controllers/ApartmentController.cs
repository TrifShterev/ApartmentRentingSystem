using System;
using System.Linq;
using ApartmentRentingSystem.Infrastructure;
using ApartmentRentingSystem.Models.Apartments;
using ApartmentRentingSystem.Services.Apartments;
using ApartmentRentingSystem.Services.Brokers;
using ApartmentRentingSystem.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace ApartmentRentingSystem.Controllers
{
    public class ApartmentController : Controller
    {
        
        private readonly IApartmentsService _apartmentsService;
        private readonly IBrokerService _brokerService;
        private readonly IMapper _mapper;

        public ApartmentController(IApartmentsService apartmentsService, IBrokerService brokerService, IMapper mapper)
        {
            
            _apartmentsService = apartmentsService;
            _brokerService = brokerService;
            _mapper = mapper;
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

           var apartmentId = this._apartmentsService.AddApartment(apartment, brokerId);

           TempData[Constants.WebConstants.GlobalMessageKey] = $"Your estate was added {(this.User.IsAdmin() ? String.Empty : "and waits for approval by Administrator")}!";

            return RedirectToAction(nameof(Details), new {id = apartmentId});
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.User.GetId();

            if (!_brokerService.UserIsBroker(userId) && !User.IsAdmin())
            {
                return RedirectToAction(nameof(BrokerController.Create), "Broker");
            }

            var apartment = this._apartmentsService.Details(id);

            if (apartment.UserId != userId && !User.IsAdmin())
            {
                return Unauthorized();
            }

            var apartmentForm = this._mapper.Map<ApartmentFormModel>(apartment);

            apartmentForm.Categories = this._apartmentsService.GetApartmentCategories();

            return View(apartmentForm);
        }


        [HttpPost]
        [Authorize]
        public IActionResult Edit(int id,ApartmentFormModel apartment)
        {
            var brokerId = _brokerService.BrokerId(this.User.GetId());

            if (brokerId == 0 && !User.IsAdmin())
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

            var apartmentIsEdited = this._apartmentsService.EditApartment(id, apartment, brokerId, this.User.IsAdmin());

            if (!apartmentIsEdited && !User.IsAdmin())
            {
                return BadRequest();
            }

            TempData[Constants.WebConstants.GlobalMessageKey] = $"Your estate was edited {(this.User.IsAdmin() ? String.Empty : "and waits for approval by Administrator")}!";

            return RedirectToAction(nameof(Details), new { id });
        }

       
        public IActionResult Delete(int id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            var apartment = _apartmentsService.Details(id);

          
            return View(apartment);
        }

       
        //Post
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
           _apartmentsService.Delete(id);

            return RedirectToAction("All");
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

        public IActionResult Details(int id, string information)
        {
            var apartment = this._apartmentsService.Details(id);

            //TODO: prevents URL from forEaching and scraping
            //if (!information.Contains(apartment.ApartmentType) || !information.Contains(apartment.Location))
            //{
            //    return BadRequest();
            //}

            return View(apartment);
        }

        [Authorize]
        public IActionResult Mine()
        {
            var myApartments = this._apartmentsService.ApartmentsOwnedByUser(this.User.GetId());

            return View(myApartments);
        }

    }

    
}
