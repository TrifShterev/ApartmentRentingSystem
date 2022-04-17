using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApartmentRentingSystem.Data;
using ApartmentRentingSystem.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ApartmentRentingSystem.Services.Orders
{
    public class OrdersService : IOrdersService
    {
        private readonly ApartmentRentingDbContext _db;

        public OrdersService(ApartmentRentingDbContext db)
        {
            _db = db;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order
            {
                UserId = userId,
                Email = userEmailAddress

            }; 
          await _db.Orders.AddAsync(order);
          await  _db.SaveChangesAsync();

          foreach (var item in items)
          {
              var orderItem = new OrderItem()
              {
                  Amount = item.Amount,
                  ApartmentId = item.Apartment.Id,
                  OrderId = order.Id,
                  Price = item.Apartment.Price
              };
              await _db.OrderItems.AddAsync(orderItem);
          }
          await _db.SaveChangesAsync();
        }


        public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
        {
            var orders = await _db.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Apartment)
                .Where(n => n.UserId == userId).ToListAsync();

            return orders;
        }
    }
}