using System;
using ApartmentRentingSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace ApartmentRentingSystemTests.Mocks
{
    public static class DataBaseMock
    {
        public static ApartmentRentingDbContext instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<ApartmentRentingDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new ApartmentRentingDbContext(dbContextOptions);
            }
        }
    }
}