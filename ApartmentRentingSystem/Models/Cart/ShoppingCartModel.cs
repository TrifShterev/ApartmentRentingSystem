using System.Collections.Generic;
using ApartmentRentingSystem.Data.Models;

namespace ApartmentRentingSystem.Models.Cart
{
    public class ShoppingCartModel
    {
        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}