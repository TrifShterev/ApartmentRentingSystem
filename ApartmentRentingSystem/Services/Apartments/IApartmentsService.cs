using System.Collections.Generic;
using ApartmentRentingSystem.Models.Apartments;

namespace ApartmentRentingSystem.Services.Apartments
{
    public interface IApartmentsService
    {
        AllApartmentsSearchModel GetAll(string apartmentType,
            string searchTerm,
            ApartmentSortingEnum sorting,
            int currentPage,
            int apartmentsPerPage
            );

        ApartmentDetailsModel Details(int id);

        IEnumerable<ApartmentListingViewModel> ApartmentsOwnedByUser(string userId);

        public int AddApartment(ApartmentFormModel apartment, int brokerId);

        public bool EditApartment(int apartmentId,ApartmentFormModel apartment, int brokerId);

        public IEnumerable<ApartmentCategoryViewModel> GetApartmentCategories();
    }
}