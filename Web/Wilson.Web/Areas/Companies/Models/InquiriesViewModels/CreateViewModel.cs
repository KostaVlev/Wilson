using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Wilson.Web.Areas.Companies.Models.InquiriesViewModels
{
    public class CreateViewModel
    {
        [DataType(DataType.Date)]
        [Display(Name = "Received At")]
        public DateTime ReceivedAt { get; set; } = DateTime.Today;

        [Required]
        [StringLength(900, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 70)]
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Choose Customer.")]
        [StringLength(36)]
        public string CustomerId { get; set; }

        [Required(ErrorMessage = "Assign at least one employee.")]
        public string[] AssigneesIds { get; set; }
        
        [Display(Name = "Customer")]
        public IEnumerable<CompanyViewModel> Customers { get; set; }

        [Display(Name = "Assign Employees")]
        public IEnumerable<EmployeeViewModel> Assignees { get; set; }

        [Display(Name = "Attach Files")]
        public IEnumerable<IFormFile> Attachments { get; set; }
    }
}
