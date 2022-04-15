using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ApartmentRentingSystem.Utilities;

namespace ApartmentRentingSystem.Models.Apartments
{
    public class ApartmentFormModel
    {
        [Required]
        [StringLength(Constants.ApartmentTypeMaxLength, MinimumLength = Constants.ApartmentTypeMinLength,ErrorMessage = "Wrong input -the apartment type must be between 2 - 50 symbols")]
        public string ApartmentType { get; init; }

        [Required]
        public string Location { get; init; }

        [Required]
        [Display(Name = "Image Url")]
        [Url]
        public string ImageUrl { get; init; }

        [Range(1900,2022)]
        public int Year { get; init; }

        [Required]
        public string Description { get; init; }

        [Display(Name = "Category")]
        public int CategoryId { get; init; }

        public double Price { get; init; }

        public IEnumerable<ApartmentCategoryViewModel> Categories { get; set; }
    }
}