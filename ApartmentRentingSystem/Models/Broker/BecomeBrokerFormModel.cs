using System.ComponentModel.DataAnnotations;
using static ApartmentRentingSystem.Utilities.Constants.Broker;

namespace ApartmentRentingSystem.Models.Broker
{
    public class BecomeBrokerFormModel
    {
       

        [Required]
        [StringLength(BrokerNameMaxLength, MinimumLength = BrokerNameMinLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(BrokerPhoneNumberMaxLength, MinimumLength = BrokerPhoneNumberMinLength)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

       
    }
}