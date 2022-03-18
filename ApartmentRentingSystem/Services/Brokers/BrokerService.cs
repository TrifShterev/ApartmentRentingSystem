using System.Linq;
using ApartmentRentingSystem.Data;
using ApartmentRentingSystem.Data.Models;
using ApartmentRentingSystem.Models.Broker;

namespace ApartmentRentingSystem.Services.Brokers
{
    public class BrokerService : IBrokerService
    {
        private readonly ApartmentRentingDbContext _db;

        public BrokerService(ApartmentRentingDbContext db)
        {
            _db = db;
        }

        public bool UserIsBroker(string userId)
        {
            return this._db
                .Brokers
                .Any(broker => broker.UserId == userId);
        }

        public int BrokerId(string userId)
        {
            return this._db
                .Brokers
                .Where(b => b.UserId == userId)
                .Select(b => b.Id)
                .FirstOrDefault();
        }

        public void AddBroker(BecomeBrokerFormModel broker,string userId)
        {
            var newBroker = new Broker
            {
                Name = broker.Name,
                PhoneNumber = broker.PhoneNumber,
                UserId = userId
            };

            _db.Brokers.Add(newBroker);
            _db.SaveChanges();
        }
    }
}