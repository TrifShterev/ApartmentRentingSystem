using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApartmentRentingSystem.Data;
using ApartmentRentingSystem.Data.Models;
using ApartmentRentingSystem.Models.Cart;
using ApartmentRentingSystem.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ApartmentRentingSystem.Services.Cart
{
    public class ShoppingCart : ICartService
    {
        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        private readonly ApartmentRentingDbContext _db;
        

        public ShoppingCart(ApartmentRentingDbContext db)
        {
            _db = db;
           
        }

        //Configures Sessions and Shopping Card as a Service
        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<ApartmentRentingDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context)
            {
                ShoppingCartId = cartId
            };
        }

        public void AddItemToCart(Apartment apartment)
        {
            var shoppingCartItem = _db.ShoppingCartItems.FirstOrDefault(s =>
                s.Apartment.Id == apartment.Id);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Apartment = apartment,
                    Amount = 1
                };

                _db.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }

            _db.SaveChanges();
        }

        public void RemoveItemFromCart(Apartment apartment)
        {
            var shoppingCartItem = _db.ShoppingCartItems.FirstOrDefault(s =>
                s.Apartment.Id == apartment.Id && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _db.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _db.SaveChanges();
        }

        public async Task ClearShoppingCartAsync()
        {
            var items = await _db.ShoppingCartItems
                .Where(x => x.ShoppingCartId == ShoppingCartId)
                .ToListAsync();

            _db.ShoppingCartItems.RemoveRange(items);
            await _db.SaveChangesAsync();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
          
           return ShoppingCartItems ?? (ShoppingCartItems = _db.ShoppingCartItems
               .Where(x => x.ShoppingCartId == ShoppingCartId)
               .Include(n => n.Apartment).ToList());

        }

        public double GetShoppingCartTotal()
        {
            return _db.ShoppingCartItems.Where(x => x.ShoppingCartId == ShoppingCartId)
                .Select(n => n.Apartment.Price * n.Amount).Sum();

            
        }
    }
}