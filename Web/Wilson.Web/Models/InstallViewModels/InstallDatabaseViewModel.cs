using System.ComponentModel.DataAnnotations;

namespace Wilson.Web.Models.InstallViewModels
{
    public class InstallDatabaseViewModel
    {
        // User input fields.
        [Required]
        [StringLength(70, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(70, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(70, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        // Database seed.
        [Display(Name = "Would you like to seed data into the database?")]
        public bool SeedData { get; set; }

        // Company input fields.
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

        // Company Address input fields.
        [Required]
        [StringLength(70, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "Post Code")]
        public string PostCode { get; set; }

        [Required]
        [StringLength(70, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [StringLength(70, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "Street")]
        public string Street { get; set; }

        [Required]
        [StringLength(6, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [Display(Name = "Street Number")]
        public string StreetNumber { get; set; }

        [Display(Name = "Floor")]
        public int? Floor { get; set; }
        
        [StringLength(6, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 1)]
        [Display(Name = "Apartment Number")]
        public string UnitNumber { get; set; }

        [StringLength(70, ErrorMessage = "The {0} must be max {1} characters long.", MinimumLength = 0)]
        [Display(Name = "Note")]
        public string Note { get; set; }
    }
}
