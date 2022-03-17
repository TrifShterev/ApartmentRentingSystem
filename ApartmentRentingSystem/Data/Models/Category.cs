using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ApartmentRentingSystem.Utilities;
using static ApartmentRentingSystem.Utilities.Constants;


public class Category
{
    
    public int Id { get; init; }

    [Required]
    [MaxLength(CategoryNameMaxLength)]
    public string Name { get; set; }

    public IEnumerable<Apartment> Apartments { get; init; } = new List<Apartment>();
}