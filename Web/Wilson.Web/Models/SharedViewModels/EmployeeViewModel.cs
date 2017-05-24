using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wilson.Companies.Core.Entities;
using Wilson.Companies.Core.Enumerations;

namespace Wilson.Web.Models.SharedViewModels
{
    public class EmployeeViewModel
    {
        public string Id { get; set; }

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
        [Phone(ErrorMessage = "Enter valid phone number.")]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Phone(ErrorMessage = "Enter valid phone number.")]
        [Display(Name = "Private Phone")]
        public string PrivatePhone { get; set; }

        [Display(Name = "Employee Position")]
        public EmployeePosition EmployeePosition { get; set; }

        public List<SelectListItem> EmployeePositions { get; set; }

        public AddressViewModel Address { get; set; }

        public static EmployeeViewModel Create()
        {
            return new EmployeeViewModel() { EmployeePositions = GetEmployeePositions() };
        }

        public static EmployeeViewModel CreateForEdit(Employee employee, IMapper mapper)
        {
            var model = mapper.Map<Employee, EmployeeViewModel>(employee);
            model.EmployeePositions = GetEmployeePositions();

            return model;
        }

        public static EmployeeViewModel ReBuild(EmployeeViewModel model)
        {
            model.EmployeePositions = GetEmployeePositions();

            return model;
        }

        private static List<SelectListItem> GetEmployeePositions()
        {
            return Enum.GetValues(typeof(EmployeePosition)).Cast<EmployeePosition>().Select(x => new SelectListItem
            {
                // Try to get the Employee position name from the DisplayAttribute.
                Text = x.GetType()
                        .GetMember(x.ToString())
                        .FirstOrDefault()
                        .GetCustomAttribute<DisplayAttribute>().Name ?? x.ToString(),
                Value = ((int)x).ToString()
            }).ToList();
        }
    }
}
