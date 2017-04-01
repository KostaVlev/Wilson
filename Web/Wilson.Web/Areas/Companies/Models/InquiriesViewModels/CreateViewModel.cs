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
        
        [DataType(DataType.Date)]
        [Display(Name = "Closed At")]
        public DateTime ClosedAt { get; set; }
        
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; }
                
        [StringLength(36)]
        public CompanyViewModel CustomerId { get; set; }

        public string[] AssigneesIds { get; set; }

        [Display(Name = "Customer")]
        public IEnumerable<CompanyViewModel> Customers { get; set; }

        [Display(Name = "Assign Employees")]
        public IEnumerable<EmployeeViewModel> Assignees { get; set; }
    }
}
