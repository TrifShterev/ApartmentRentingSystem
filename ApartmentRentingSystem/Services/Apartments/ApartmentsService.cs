using System;
using System.Collections.Generic;
using System.Linq;
using ApartmentRentingSystem.Data;
using ApartmentRentingSystem.Data.Models;
using ApartmentRentingSystem.Models.Apartments;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authentication;

namespace ApartmentRentingSystem.Services.Apartments
{
    public class ApartmentsService : IApartmentsService
    {
        private readonly ApartmentRentingDbContext _db;
        private readonly IMapper _mapper;

        public ApartmentsService(ApartmentRentingDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public ApartmentDetailsModel Details(int id)
            => this._db
                .Apartments
                .Where(a => a.Id == id)
                .ProjectTo<ApartmentDetailsModel>(this._mapper.ConfigurationProvider)
                .FirstOrDefault();


        public IEnumerable<ApartmentListingViewModel> ApartmentsOwnedByUser(string userId)
            => GetApartments(this._db.Apartments
                .Where(a => a.Broker.UserId == userId));



        public int AddApartment(ApartmentFormModel apartment, int brokerId)
        {
            var newApartment = this._mapper.Map<Apartment>(apartment);
            newApartment.BrokerId = brokerId;
            newApartment.IsPublic = false;
              

            this._db.Apartments.Add(newApartment);
            this._db.SaveChanges();

            return newApartment.Id;
        }

        public bool EditApartment(int apartmentId, ApartmentFormModel apartment, int brokerId, bool isPublic)
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
            currentApartmentData.Price = apartment.Price;
            currentApartmentData.Description = apartment.Description;
            currentApartmentData.CategoryId = apartment.CategoryId;
            currentApartmentData.IsPublic = isPublic;


            this._db.SaveChanges();

            return true;


        }

        public void ApproveEstate(int apartmentId)
        {
            var apartment = this._db.Apartments.Find(apartmentId);

            apartment.IsPublic = !apartment.IsPublic;

            this._db.SaveChanges();
        }

        public AllApartmentsSearchModel GetAll(
            string apartmentType = null,
            string searchTerm = null,
            ApartmentSortingEnum sorting = ApartmentSortingEnum.Year,
            int currentPage = 1 ,
            int apartmentsPerPage = Int32.MaxValue,
            bool publicOnly = true)
        {
            var apartmentsQuery = this._db
                .Apartments
                .Where(a => publicOnly ? a.IsPublic : true);
            // .AsQueryable();

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

        public void Delete(int id)
        {
          var apartment =   _db.Apartments.Find(id);

          _db.Apartments.Remove(apartment);
          _db.SaveChanges();
        }

        private  IEnumerable<ApartmentListingViewModel> GetApartments(IQueryable<Apartment> apartmentQuery)
        => apartmentQuery
            .ProjectTo<ApartmentListingViewModel>(_mapper.ConfigurationProvider)
            .ToList();
    }
}