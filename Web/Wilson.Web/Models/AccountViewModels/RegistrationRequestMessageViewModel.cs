using System.ComponentModel.DataAnnotations;
using Wilson.Web.Models.SharedViewModels;

namespace Wilson.Web.Models.AccountViewModels
{
    public class RegistrationRequestMessageViewModel
    {
        [Required]
        [StringLength(70, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(70, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Enter valid phone number.")]
        [Phone]
        [Display(Name = "Phone")]
        public string PrivatePhone { get; set; }

        public AddressViewModel Address { get; set; }
    }
}
