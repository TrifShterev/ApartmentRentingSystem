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
            int apartmentsPerPage);

        public int AddApartment(AddApartmentFormModel apartment, int brokerId);

        public IEnumerable<ApartmentCategoryViewModel> GetApartmentCategories();
    }
}