using ApartmentRentingSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ApartmentRentingSystem.Services;
namespace ApartmentRentingSystem.Controllers
{
    public class HomeController : Controller
    {

        
        private readonly IStatsService _statistics;

        public HomeController(IStatsService statistics)
        {
            this._statistics = statistics;
           
        }

      

        public IActionResult Index()
        {

            var totalStatistics = this._statistics.GetStatistics();

            return View(totalStatistics);
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
