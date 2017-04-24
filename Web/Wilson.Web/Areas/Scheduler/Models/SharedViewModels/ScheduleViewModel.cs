using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wilson.Scheduler.Core.Enumerations;
using System.Reflection;
using System.Linq;

namespace Wilson.Web.Areas.Scheduler.Models.SharedViewModels
{
    public class ScheduleViewModel
    {
        public string Id { get; set; }

        public DateTime Date { get; set; }

        [Display(Name = "Schedule Option")]
        public ScheduleOption ScheduleOption { get; set; }       

        [Range(2, 8, ErrorMessage = Constants.ValidationMessages.Range)]
        [Display(Name = "Hours")]
        public int WorkHours { get; set; } = 8;

        [Range(0, 6, ErrorMessage = Constants.ValidationMessages.Range)]
        [Display(Name = "Extra Hours")]
        public int ExtraWorkHours { get; set; }
        
        public string EmployeeId { get; set; }

        [StringLength(36, ErrorMessage = Constants.ValidationMessages.Error)]
        [Display(Name = "Project")]
        public string ProjectId { get; set; }

        public string ScheduleOptionName { get { return this.GetShceduleOptionName(); } }

        public ProjectViewModel Project { get; set; }

        public IEnumerable<SelectListItem> ProjectOptions { get; set; }

        public IEnumerable<SelectListItem> ScheduleOptions { get; set; }

        private string GetShceduleOptionName()
        {
            string name = this.ScheduleOption
                .GetType()
                .GetMember(this.ScheduleOption.ToString())
                .First()
                .GetCustomAttribute<DisplayAttribute>().Name;

            return name;
        }
    }
}
