using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApartmentRentingSystem.Models.Apartments
{
    public class AllApartmentsSearchModel
    {
        [Display(Name = "Select Apartment Type:")]
        public string ApartmentType { get; set; }

        public IEnumerable<string> ApartmentTypes { get; set; }

        [Display(Name = "Search by text:")]
        public string SearchTerm { get; set; }

        [Display(Name = "Sort by:")]
        public ApartmentSortingEnum ApartmentSorting { get; set; }

        public IEnumerable<ApartmentListingViewModel> Apartments { get; set; }
    }
}