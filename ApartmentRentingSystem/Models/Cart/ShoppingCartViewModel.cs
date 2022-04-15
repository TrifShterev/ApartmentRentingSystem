using ApartmentRentingSystem.Services.Cart;

namespace ApartmentRentingSystem.Models.Cart
{
    public class ShoppingCartViewModel
    {
        public ShoppingCart ShoppingCart { get; set; }

        public double ShoppingCartTotal { get; set; }
    }
}