using ApartmentRentingSystem.Models.Home;
using ApartmentRentingSystem.Services;
using Moq;

namespace ApartmentRentingSystemTests.Mocks
{
    public static class StatisticsServiceMock
    {
        public static IStatsService InstanceService
        {
            get
            {
                var statsServiceMock = new Mock<IStatsService>();

                statsServiceMock
                    .Setup(s => s.GetStatistics())
                    .Returns(new IndexViewModel
                    {
                        AllApartments = 20,
                        AllRents = 15,
                        AllUsers = 7
                    });

                return statsServiceMock.Object;
            }

          
        }
    }
}