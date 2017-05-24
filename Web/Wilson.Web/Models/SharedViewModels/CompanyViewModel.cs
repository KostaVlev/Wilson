using System.ComponentModel.DataAnnotations;

namespace Wilson.Web.Models.SharedViewModels
{
    public class CompanyViewModel
    {
        [Required]
        [StringLength(70, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "Company Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; }

        [StringLength(12, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "VAT Number")]
        public string VatNumber { get; set; }

        [Required(ErrorMessage = "Enter valid email address number.")]
        [EmailAddress]
        [Display(Name = "Office Email")]
        public string OfficeEmail { get; set; }

        [Required(ErrorMessage = "Enter valid phone number.")]
        [Phone]
        [Display(Name = "Office Phone")]
        public string OfficePhone { get; set; }

        public AddressViewModel Address { get; set; }

        public static CompanyViewModel Create()
        {
            return new CompanyViewModel() { Address = AddressViewModel.Create() };
        }

        public static CompanyViewModel ReBuild(CompanyViewModel model)
        {
            model.Address = AddressViewModel.ReBuild(model.Address);
            return model;
        }
    }
}
