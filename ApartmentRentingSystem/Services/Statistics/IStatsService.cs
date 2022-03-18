using ApartmentRentingSystem.Models.Home;

namespace ApartmentRentingSystem.Services
{
    public interface IStatsService
    {
        IndexViewModel GetStatistics();
    }
}