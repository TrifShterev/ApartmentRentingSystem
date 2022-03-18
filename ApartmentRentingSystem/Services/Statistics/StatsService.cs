using System.Linq;
using ApartmentRentingSystem.Data;
using ApartmentRentingSystem.Models.Home;

namespace ApartmentRentingSystem.Services
{
    public class StatsService : IStatsService
    {
        private readonly ApartmentRentingDbContext _db;

        public StatsService(ApartmentRentingDbContext db)
        {
            _db = db;
        }

        public IndexViewModel GetStatistics()
        {
            var apartments =
                this._db.Apartments
                    .OrderByDescending(a => a.Id)
                    .Select(a => new ApartmentIndexViewModel()
                    {
                        Id = a.Id,
                        ApartmentType = a.ApartmentType,
                        Location = a.Location,
                        ImageUrl = a.ImageUrl,
                        Year = a.Year

                    })
                    .Take(3)
                    .ToList();

            var totalApartments = this._db.Apartments.Count();
            var totalUsers = this._db.Users.Count();


            return new IndexViewModel()
            {
                AllApartments = totalApartments,
                AllUsers = totalUsers,
                Apartments = apartments
                
            };
        }
    }
}