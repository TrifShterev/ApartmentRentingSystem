using System.Collections.Generic;
using ApartmentRentingSystem.Data.Models;
using ApartmentRentingSystem.Models.Cart;

namespace ApartmentRentingSystem.Services.Cart
{
    public interface ICartService
    {
        List<ShoppingCartItem> GetShoppingCartItems(ShoppingCartModel model);

        double GetShoppingCartTotal(ShoppingCartModel model);
    }
}