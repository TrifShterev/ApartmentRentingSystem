using System.Collections.Generic;
using System.Threading.Tasks;
using ApartmentRentingSystem.Data.Models;

namespace ApartmentRentingSystem.Services.Orders
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);

        Task<List<Order>> GetOrdersByUserIdAsync(string userId);
    }
}