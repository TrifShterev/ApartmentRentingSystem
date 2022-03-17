using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ApartmentRentingSystem.Utilities;
using static ApartmentRentingSystem.Utilities.Constants.Broker;

namespace ApartmentRentingSystem.Data.Models
{
    public class Broker
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(BrokerNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(BrokerPhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        [Required]
        public string UserId { get; set; }

        public IEnumerable<Apartment> Apartments { get; set; } = new List<Apartment>();
    }
}