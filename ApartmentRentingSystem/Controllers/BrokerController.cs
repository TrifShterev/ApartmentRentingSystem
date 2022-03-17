using System.Linq;
using ApartmentRentingSystem.Data;
using ApartmentRentingSystem.Data.Models;
using ApartmentRentingSystem.Infrastructure;
using ApartmentRentingSystem.Models.Broker;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentRentingSystem.Controllers
{
    public class BrokerController : Controller
    {
        private readonly ApartmentRentingDbContext _db;

        public BrokerController(ApartmentRentingDbContext db)
        {
            _db = db;
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

            var userIsAlreadyBroker = _db
                .Brokers
                .Any(b => b.UserId == userId);

            if (userIsAlreadyBroker)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(broker);
            }

            var newBroker = new Broker
            {
                Name = broker.Name,
                PhoneNumber = broker.PhoneNumber,
                UserId = userId
            };

            _db.Brokers.Add(newBroker);
            _db.SaveChanges();

            return RedirectToAction("All", "Apartment");
        }
    }
}
