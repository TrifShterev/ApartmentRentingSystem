using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApartmentRentingSystem.Data.Models
{
    public class Order
    {
        [Key] 
        public int Id { get; set; }

        public string Email { get; set; }

        public string UserId { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}