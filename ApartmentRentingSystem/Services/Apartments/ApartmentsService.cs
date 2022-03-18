﻿using System.Linq;
using ApartmentRentingSystem.Data;
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

            var apartments =
                apartmentsQuery
                    .Skip((currentPage - 1) * apartmentsPerPage)
                    .Take(apartmentsPerPage)
                    .Select(a => new ApartmentListingViewModel
                    {
                        Id = a.Id,
                        ApartmentType = a.ApartmentType,
                        Location = a.Location,
                        ImageUrl = a.ImageUrl,
                        Year = a.Year,
                        Category = a.Category.Name,
                    }).ToList();

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
    }
}