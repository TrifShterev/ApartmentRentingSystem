using System.Linq;
using ApartmentRentingSystem.Data;
using ApartmentRentingSystem.Data.Models;
using ApartmentRentingSystem.Infrastructure;
using ApartmentRentingSystem.Models.Broker;
using ApartmentRentingSystem.Services.Brokers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentRentingSystem.Controllers
{
    public class BrokerController : Controller
    {
        private readonly IBrokerService _brokerService;

        public BrokerController(IBrokerService brokerService)
        {
            _brokerService = brokerService;
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(BecomeBrokerFormModel broker)
        {
            var userId = this.User.GetId();

            if (_brokerService.UserIsBroker(this.User.GetId()))
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(broker);
            }

            _brokerService.AddBroker(broker,userId);

           

            return RedirectToAction("All", "Apartment");
        }
    }
}
