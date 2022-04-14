using System;
using System.Collections.Generic;
using ApartmentRentingSystem.Models.Apartments;

namespace ApartmentRentingSystem.Services.Apartments
{
    public interface IApartmentsService
    {
        AllApartmentsSearchModel GetAll(
            string apartmentType = null,
            string searchTerm = null,
            ApartmentSortingEnum sorting = ApartmentSortingEnum.Year,
            int currentPage = 1,
            int apartmentsPerPage = Int32.MaxValue,
            bool publicOnly = true
            );

        ApartmentDetailsModel Details(int id);

        IEnumerable<ApartmentListingViewModel> ApartmentsOwnedByUser(string userId);

        public int AddApartment(ApartmentFormModel apartment, int brokerId);

        public bool EditApartment(int apartmentId,ApartmentFormModel apartment, int brokerId, bool isPublic);

        void ApproveEstate(int apartmentId);

        public IEnumerable<ApartmentCategoryViewModel> GetApartmentCategories();

        void Delete(int apartmentId);
    }
}