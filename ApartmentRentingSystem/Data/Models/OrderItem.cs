using System.ComponentModel.DataAnnotations;

namespace ApartmentRentingSystem.Data.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        public int Amount { get; set; }

        public double Price { get; set; }

        public int ApartmentId { get; set; }

        public  Apartment Apartment { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }
    }
}