using System.ComponentModel.DataAnnotations;

namespace Wilson.Web.Models.SharedViewModels
{
    public class AddressViewModel
    {
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
