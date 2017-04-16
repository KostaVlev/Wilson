using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wilson.Scheduler.Core.Enumerations;
using Wilson.Web.Areas.Scheduler.Models.HomeViewModels;

namespace Wilson.Web.Areas.Scheduler.Models.SharedViewModels
{
    public class ScheduleViewModel
    {
        public string Id { get; set; }

        public DateTime Date { get; set; }

        [Display(Name = "Schedule Option")]
        public ScheduleOption ScheduleOption { get; set; }

        public string ScheduleOptionName { get; set; }

        [Range(2, 8, ErrorMessage = Constants.ValidationMessages.Range)]
        [Display(Name = "Hours")]
        public int WorkHours { get; set; } = 8;

        [Range(0, 6, ErrorMessage = Constants.ValidationMessages.Range)]
        [Display(Name = "Extra Hours")]
        public int ExtraWorkHours { get; set; }

        [Required(ErrorMessage = Constants.ValidationMessages.Error)]
        public string EmployeeId { get; set; }

        [Display(Name = "Project")]
        public string ProjectId { get; set; }

        public ProjectViewModel Project { get; set; }

        public EmployeeConciseViewModel Employee { get; set; }

        public IEnumerable<SelectListItem> ProjectOptions { get; set; }

        public IEnumerable<SelectListItem> ScheduleOptions { get; set; }
    }
}
