using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApartmentRentingSystem.Utilities;

namespace ApartmentRentingSystem.Areas.Admin.Controllers
{
    [Area(Constants.WebConstants.AreaName)]
    [Authorize(Roles = Constants.WebConstants.AdminRoleName)]
    public abstract class AdminController : Controller
    {
       
    }
}
