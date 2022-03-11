using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ApartmentRentingSystem.Utilities;


public class Category
{
    public int Id { get; set; }

    [Required]
    [MaxLength(Constants.CategoryNameMaxLength)]
    public string Name { get; set; }

    public IEnumerable<Apartment> Apartments { get; set; } = new List<Apartment>();
}