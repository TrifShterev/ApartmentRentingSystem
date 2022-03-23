using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using static ApartmentRentingSystem.Utilities.Constants.User;

namespace ApartmentRentingSystem.Data.Models
{
    public class User : IdentityUser
    {
        [MaxLength(UserNameMaxLength)]
        public string FullName { get; set; }
    }
}