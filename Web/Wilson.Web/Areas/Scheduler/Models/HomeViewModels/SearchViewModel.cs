using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wilson.Web.Areas.Scheduler.Models.SharedViewModels;

namespace Wilson.Web.Areas.Scheduler.Models.HomeViewModels
{
    public class SearchViewModel
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }

        [StringLength(36)]
        [Display(Name = "Employee")]
        public string EmployeeId { get; set; }

        [StringLength(36)]
        [Display(Name = "Project")]
        public string ProjectId { get; set; }

        public IEnumerable<SelectListItem> EmployeeOptions { get; set; }

        public IEnumerable<SelectListItem> ProjectOptions { get; set; }

        public IDictionary<EmployeeConciseViewModel, List<ScheduleViewModel>> EmployeesShcedules { get; set; }
    }
}
