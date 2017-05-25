using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wilson.Scheduler.Core.Enumerations;
using Wilson.Web.Areas.Scheduler.Services;
using System.Linq;

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

        public async static Task<SearchViewModel> Create(
            IScheduleSevice services, 
            IMapper mapper)
        {            
            return new SearchViewModel()
            {
                // Giving some start values for the SearchModel dates.
                To = DateTime.Now,
                From = DateTime.Now.AddDays(-7),
                ProjectOptions = await services.GetShdeduleProjectOptions(),
                ScheduleOptions = services.GetScheduleOptions(),
                EmployeeOptions = await services.GetShdeduleEmployeeOptions(),
                Employees = new HashSet<EmployeeViewModel>()
            };
        }

        public async static Task<SearchViewModel> Create(
            IScheduleSevice services,
            IMapper mapper,
            IEnumerable<EmployeeViewModel> employees)
        {
            return new SearchViewModel()
            {
                // Giving some start values for the SearchModel dates.
                To = DateTime.Now,
                From = DateTime.Now.AddDays(-7),
                ProjectOptions = await services.GetShdeduleProjectOptions(),
                ScheduleOptions = services.GetScheduleOptions(),
                EmployeeOptions = await services.GetShdeduleEmployeeOptions(),
                Employees = employees.Where(e => e.Schedules != null && e.Schedules.Count() > 0)
            };
        }

        public async static Task<SearchViewModel> ReBuild(
            SearchViewModel model,
            IScheduleSevice services,
            IMapper mapper)
        {
            model.To = DateTime.Now;
            model.From = model.To.AddDays(-7);
            model.ProjectOptions = await services.GetShdeduleProjectOptions();
            model.ScheduleOptions = services.GetScheduleOptions();
            model.EmployeeOptions = await services.GetShdeduleEmployeeOptions();
            model.Employees = new HashSet<EmployeeViewModel>();

            return model;
        }
    }
}
