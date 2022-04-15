using ApartmentRentingSystem.Models.Cart;
using ApartmentRentingSystem.Services.Apartments;
using ApartmentRentingSystem.Services.Cart;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentRentingSystem.Controllers
{
    public class OrderController : Controller
    {
        private readonly IApartmentsService _apartmentsService;
        private readonly ShoppingCart _cartService;

        public OrderController(IApartmentsService apartmentsService, ShoppingCart cartService)
        {
            _apartmentsService = apartmentsService;
            _cartService = cartService;
        }

        public IActionResult All()
        {
            var items = _cartService.GetShoppingCartItems();
            _cartService.ShoppingCartItems = items;

            var response = new ShoppingCartViewModel
            {
               ShoppingCart =  _cartService,
                ShoppingCartTotal = _cartService.GetShoppingCartTotal()
            };

            return View(response);
        }

        public IActionResult AddToCart(int id)
        {
            var item = _apartmentsService.GetApartmentById(id);

            if (item != null)
            {
                _cartService.AddItemToCart(item);
            }

            return RedirectToAction(nameof(All));
        }

        public IActionResult RemoveItemFromShoppingCart(int id)
        {
            var item = _apartmentsService.GetApartmentById(id);

            if (item != null)
            {
                _cartService.RemoveItemFromCart(item);
            }

            return RedirectToAction(nameof(All));
        }
    }
}
