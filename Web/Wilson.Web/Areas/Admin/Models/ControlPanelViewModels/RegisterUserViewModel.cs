using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wilson.Companies.Core.Enumerations;

namespace Wilson.Web.Areas.Admin.Models.ControlPanelViewModels
{
    public class RegisterUserViewModel
    {
        [Required]
        [StringLength(70, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(70, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
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

        [Required(ErrorMessage = "Enter valid phone number.")]
        [Phone]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Phone]
        [Display(Name = "Private Phone")]
        public string PrivatePhone { get; set; }

        [Display(Name = "Employee Position")]
        public EmployeePosition EmployeePosition { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string ApplicationRoleName { get; set; }

        public List<SelectListItem> EmployeePositions { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
