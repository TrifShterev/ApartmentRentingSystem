using System.Collections.Generic;
using ApartmentRentingSystem.Data;
using ApartmentRentingSystem.Data.Models;

namespace ApartmentRentingSystem.Models.Cart
{
    public class ShoppingCartModel
    {
        private readonly  ApartmentRentingDbContext _context;

        public ShoppingCartModel(ApartmentRentingDbContext context)
        {
            _context = context;
        }

        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}