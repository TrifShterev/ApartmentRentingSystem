using System.Collections.Generic;
using System.Linq;
using ApartmentRentingSystem.Data;
using ApartmentRentingSystem.Data.Models;
using ApartmentRentingSystem.Models.Cart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApartmentRentingSystem.Services.Cart
{
    public class CartService : ICartService
    {
        private readonly ApartmentRentingDbContext _db;

        public CartService(ApartmentRentingDbContext db)
        {
            _db = db;
        }

        public List<ShoppingCartItem> GetShoppingCartItems(ShoppingCartModel model)
        {
            //var model = new ShoppingCartModel();

           var listItems = new List<ShoppingCartItem>();

           return listItems ??= _db.ShoppingCartItems
               .Where(x => x.ShoppingCartId == model.ShoppingCartId)
               .Include(n => n.Apartment).ToList();


        }

        public double GetShoppingCartTotal(ShoppingCartModel model)
        {
            //return _db.ShoppingCartItems.Where(x => x.ShoppingCartId == model.ShoppingCartId)
            //    .Select(n => n.Apartment.Price * n.Amount).Sum();

            return 250.50;
        }
    }
}