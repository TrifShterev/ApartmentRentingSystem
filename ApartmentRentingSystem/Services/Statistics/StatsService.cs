using System.Linq;
using ApartmentRentingSystem.Data;
using ApartmentRentingSystem.Models.Home;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace ApartmentRentingSystem.Services
{
    public class StatsService : IStatsService
    {
        private readonly ApartmentRentingDbContext _db;
       private readonly IMapper _mapper;

        public StatsService(ApartmentRentingDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public IndexViewModel GetStatistics()
        {
            var apartments =
                this._db.Apartments
                    .OrderByDescending(a => a.Id)
                    .ProjectTo<ApartmentIndexViewModel>(this._mapper.ConfigurationProvider)
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