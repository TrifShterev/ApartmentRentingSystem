using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ApartmentRentingSystem.Utilities;

namespace ApartmentRentingSystem.Models.Apartments
{
    public class AddApartmentFormModel
    {
        [Required]
        [StringLength(Constants.ApartmentTypeMaxLength, MinimumLength = Constants.ApartmentTypeMinLength,ErrorMessage = "Wrong input -the apartment type must be between 2 - 50 symbols")]
        public string ApartmentType { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [DisplayName("Image Url")]
        public string ImageUrl { get; set; }

        [Range(1900,2022)]
        public int Year { get; set; }

        [Required]
        public string Description { get; set; }

        [DisplayName("Category")]
        public int CategoryId { get; set; }

        public IEnumerable<ApartmentCategoryViewModel> Categories { get; set; }
    }
}