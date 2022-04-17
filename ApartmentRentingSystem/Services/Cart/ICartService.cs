using System.Collections.Generic;
using System.Threading.Tasks;
using ApartmentRentingSystem.Data.Models;
using ApartmentRentingSystem.Models.Cart;

namespace ApartmentRentingSystem.Services.Cart
{
    public interface ICartService
    {
        void AddItemToCart(Apartment apartment);

        void RemoveItemFromCart(Apartment apartment);

        List<ShoppingCartItem> GetShoppingCartItems();

        double GetShoppingCartTotal();

        public Task ClearShoppingCartAsync();
    }
}