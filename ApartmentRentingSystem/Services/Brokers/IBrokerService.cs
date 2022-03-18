using ApartmentRentingSystem.Models.Broker;

namespace ApartmentRentingSystem.Services.Brokers
{
    public interface IBrokerService
    {
        public bool UserIsBroker(string userId);

        public int BrokerId(string userId);

        public void AddBroker(BecomeBrokerFormModel broker,string userId);
    }
}