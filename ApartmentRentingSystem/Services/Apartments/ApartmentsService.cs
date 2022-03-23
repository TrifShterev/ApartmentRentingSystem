using System.Collections.Generic;
using System.Linq;
using ApartmentRentingSystem.Data;
using ApartmentRentingSystem.Data.Models;
using ApartmentRentingSystem.Models.Apartments;

namespace ApartmentRentingSystem.Services.Apartments
{
    public class ApartmentsService : IApartmentsService
    {
        private readonly ApartmentRentingDbContext _db;

        public ApartmentsService(ApartmentRentingDbContext db)
        {
            _db = db;
        }

        public ApartmentDetailsModel Details(int id)
            => this._db
                .Apartments
                .Where(a => a.Id == id)
                .Select(a => new ApartmentDetailsModel
                {
                    Id = a.Id,
                    ApartmentType = a.ApartmentType,
                    Location = a.Location,
                    ImageUrl = a.ImageUrl,
                    Description = a.Description,
                    Year = a.Year,
                    CategoryName = a.Category.Name,
                    BrokerId = a.BrokerId,
                    BrokerName = a.Broker.Name,
                    UserId = a.Broker.UserId

                })
                .FirstOrDefault();


        public IEnumerable<ApartmentListingViewModel> ApartmentsOwnedByUser(string userId)
            => GetApartments(this._db.Apartments
                .Where(a => a.Broker.UserId == userId));



        public int AddApartment(ApartmentFormModel apartment, int brokerId)
        {
            var newApartment = new Apartment
            {

                ApartmentType = apartment.ApartmentType,
                Location = apartment.Location,
                ImageUrl = apartment.ImageUrl,
                Year = apartment.Year,
                Description = apartment.Description,
                CategoryId = apartment.CategoryId,
                BrokerId = brokerId

            };

            this._db.Apartments.Add(newApartment);
            this._db.SaveChanges();

            return newApartment.Id;
        }

        public bool EditApartment(int apartmentId, ApartmentFormModel apartment, int brokerId)
        {
            var currentApartmentData = this._db
                .Apartments
                .FirstOrDefault(a => a.Id == apartmentId);

            if (currentApartmentData.BrokerId != brokerId || currentApartmentData == null)
            {
                return false;
            }

            currentApartmentData.ApartmentType = apartment.ApartmentType;
            currentApartmentData.Location = apartment.Location;
            currentApartmentData.ImageUrl = apartment.ImageUrl;
            currentApartmentData.Year = apartment.Year;
            currentApartmentData.Description = apartment.Description;
            currentApartmentData.CategoryId = apartment.CategoryId;


            this._db.SaveChanges();

            return true;


        }

        public AllApartmentsSearchModel GetAll(
            string apartmentType,
            string searchTerm,
            ApartmentSortingEnum sorting,
            int currentPage,
            int apartmentsPerPage)
        {
            var apartmentsQuery = this._db
             .Apartments
             .AsQueryable();

            if (!string.IsNullOrWhiteSpace(apartmentType))
            {
                apartmentsQuery = apartmentsQuery
                    .Where(a => a.ApartmentType == apartmentType);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                apartmentsQuery = apartmentsQuery
                    .Where(a => (a.ApartmentType + " " + a.Location).ToLower()
                        .Contains(searchTerm.ToLower())
                        || a.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            apartmentsQuery = sorting switch
            {
                ApartmentSortingEnum.Year => apartmentsQuery.OrderByDescending(a => a.Year),
                ApartmentSortingEnum.Location => apartmentsQuery.OrderByDescending(a => a.Location),
                ApartmentSortingEnum.ApartmentType => apartmentsQuery.OrderByDescending(a => a.ApartmentType),
                _ => apartmentsQuery.OrderByDescending(a => a.Id)
            };

            var totalApartments = apartmentsQuery.Count();

            var apartments = GetApartments(
                apartmentsQuery
                .Skip((currentPage - 1) * apartmentsPerPage)
                .Take(apartmentsPerPage));
               
                    

            var apartmentTypes = this._db
                .Apartments
                .Select(a => a.ApartmentType)
                .Distinct()
                .ToList();

            return new AllApartmentsSearchModel()
            {
                TotalApartments = totalApartments,
                ApartmentTypes = apartmentTypes,
                Apartments = apartments,
                CurrentPage = currentPage
            };
        }
       
        public IEnumerable<ApartmentCategoryViewModel> GetApartmentCategories()
            => this._db
                .Categories
                .Select(c => new ApartmentCategoryViewModel
                {
                    Id = c.Id,
                    Name = c.Name,
                }).ToList();

        private static IEnumerable<ApartmentListingViewModel> GetApartments(IQueryable<Apartment> apartmentQuery)
        => apartmentQuery
            .Select(a => new ApartmentListingViewModel
            {
                Id = a.Id,
                ApartmentType = a.ApartmentType,
                Location = a.Location,
                ImageUrl = a.ImageUrl,
                Year = a.Year,
                CategoryName = a.Category.Name,
            }).ToList();
    }
}