using ApartmentRentingSystem.Data.Models;
using ApartmentRentingSystem.Services.Brokers;
using ApartmentRentingSystemTests.Mocks;
using Xunit;

namespace ApartmentRentingSystemTests.Services
{
    public class BrokerServiceTest
    {
        [Fact]
        public void IsBrokerShouldReturnTrueWhenUserIsBroker()
        {

            //Arrange
            const string userId = "TestUserId";

            //using InMemoryDatabase framework to Mock DataBase
            using var data = DataBaseMock.instance;

            data.Brokers.Add(new Broker { UserId = userId });
            data.SaveChanges();


            //Fastest way of Mocking without using InMemoryDatabase framework
            // var brokerService = new BrokerService(Mock.Of<ApartmentRentingDbContext>());


            var brokerService = new BrokerService(data);

            //Act
            var result = brokerService.UserIsBroker(userId);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void IsBrokerShouldReturnFalseWhenUserIsNotBroker()
        {

            //Arrange
            using var data = DataBaseMock.instance;

            data.Brokers.Add(new Broker { UserId = "TestUserId" });
            data.SaveChanges();

            var brokerService = new BrokerService(data);

            //Act
            var result = brokerService.UserIsBroker("AnotherUserId");

            //Assert
            Assert.False(result);
        }
    }
}