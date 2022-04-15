using System.ComponentModel;

namespace ApartmentRentingSystem.Models.Apartments
{
    public class ApartmentDetailsModel :ApartmentListingViewModel
    {
        public string Description { get; set; }

        public double Price { get; set; }

        public int BrokerId { get; set; }

        public int CategoryId { get; set; }


        public string BrokerName { get; set; }

        public string UserId { get; set; }
    }
}