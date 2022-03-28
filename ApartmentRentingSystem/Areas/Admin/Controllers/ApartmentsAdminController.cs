using ApartmentRentingSystem.Services.Apartments;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentRentingSystem.Areas.Admin.Controllers
{
    public class ApartmentsAdminController : AdminController
    {
        private readonly IApartmentsService _apartmentsService;

        public ApartmentsAdminController(IApartmentsService apartmentsService)
        {
            _apartmentsService = apartmentsService;
        }

        public IActionResult All()
        {
            var allApartments = this._apartmentsService.GetAll(publicOnly: false);

            return View(allApartments);
        }
    }
}
