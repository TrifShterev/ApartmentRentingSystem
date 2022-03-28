using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ApartmentRentingSystem.Data.Models;
using ApartmentRentingSystem.Utilities;


public class Apartment
{
   
    public int Id { get; set; }

    [Required]
    [MaxLength(Constants.ApartmentTypeMaxLength)]
    public string ApartmentType { get; set; }

    [Required]
    public string Location { get; set; }

    [Required]
    public string ImageUrl { get; set; }

    public int Year { get; set; }

    public bool IsPublic { get; set; }

    [Required]
    public string Description { get; set; }

  
    public int CategoryId { get; set; }

    public Category Category { get; init; }

    public int BrokerId { get; set; }

    public Broker Broker { get; set; }


}