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
    }
}