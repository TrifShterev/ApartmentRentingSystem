using System.ComponentModel.DataAnnotations;

namespace ApartmentRentingSystem.Data.Models
{
    public class ShoppingCartItem
    {
        
        [Key]
        public int Id { get; set; }

        public Apartment Apartment { get; set; }

        public int Amount { get; set; }

        public string ShoppingCartId { get; set; }
    }
}