using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
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

        public List<SelectListItem> EmployeePositions { get; set; } = GetEmployeePositions();

        private static List<SelectListItem> GetEmployeePositions()
        {
            var positions = Enum.GetValues(typeof(EmployeePosition)).Cast<EmployeePosition>().Select(x => new SelectListItem
            {
                // Try to get the Employee position name from the DisplayAttribute.
                Text = x.GetType()
                        .GetMember(x.ToString())
                        .FirstOrDefault()
                        .GetCustomAttribute<DisplayAttribute>().Name ?? x.ToString(),
                Value = ((int)x).ToString()
            }).ToList();

            return positions;
        }
    }
}
