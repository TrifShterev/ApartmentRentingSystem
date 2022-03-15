using System.Collections.Generic;

namespace ApartmentRentingSystem.Models.Home
{
    public class IndexViewModel
    {
        public int AllApartments { get; init; }

        public int AllUsers { get; init; }

        public int AllRents { get; init; }

        public List<ApartmentIndexViewModel> Apartments { get; init; }
    }
}