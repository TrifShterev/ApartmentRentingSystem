namespace ApartmentRentingSystem.Models.Apartments
{
    public class ApartmentListingViewModel
    {
        public int Id { get; set; }

        public string ApartmentType { get; set; }

        public double Price { get; set; }

        public string Location { get; set; }

        public int Year { get; set; }

        public string ImageUrl { get; set; }

        public string CategoryName { get; set; }

        public bool IsPublic { get; set; }


    }
}