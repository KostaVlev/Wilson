using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Wilson.Web.Areas.Companies.Models.InquiriesViewModels
{
    public class FilterViewModel
    {
        [DataType(DataType.Date)]
        [Display(Name = "Form")]
        public DateTime From { get; set; } = DateTime.Now.AddMonths(-1);

        [DataType(DataType.Date)]
        [Display(Name = "To")]
        public DateTime To { get; set; } = DateTime.Now;
        
        public IEnumerable<CompanyViewModel> Customers { get; set; }

        public IEnumerable<EmployeeViewModel> Assignees { get; set; }
    }
}
