using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wilson.Web.Areas.Scheduler.Models.SharedViewModels;
using Wilson.Scheduler.Core.Enumerations;

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

        [Display(Name = "Scheduled As")]
        public ScheduleOption? ScheduleOption { get; set; }

        public IEnumerable<SelectListItem> EmployeeOptions { get; set; }

        public IEnumerable<SelectListItem> ProjectOptions { get; set; }

        public IEnumerable<SelectListItem> ScheduleOptions { get; set; }

        public IEnumerable<EmployeeViewModel> Employees { get; set; }
    }
}
