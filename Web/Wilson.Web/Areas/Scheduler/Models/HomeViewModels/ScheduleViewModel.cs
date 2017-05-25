using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wilson.Scheduler.Core.Entities;
using Wilson.Scheduler.Core.Enumerations;
using Wilson.Web.Areas.Scheduler.Models.SharedViewModels;
using Wilson.Web.Areas.Scheduler.Services;

namespace Wilson.Web.Areas.Scheduler.Models.HomeViewModels
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

        public EmployeeConciseViewModel Employee { get; set; }

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

        public async static Task<IEnumerable<ScheduleViewModel>> CreateCollectionForEditAsync(
            IEnumerable<Schedule> schedules,
            IScheduleSevice services,
            IMapper mapper)
        {
            var scheduleModels = mapper.Map<IEnumerable<Schedule>, IEnumerable<ScheduleViewModel>>(schedules);
            var projectOptions = await services.GetShdeduleProjectOptions();
            var scheduleOptions = services.GetScheduleOptions();

            scheduleModels.ToList().ForEach(x =>
                { x.ProjectOptions = projectOptions; x.ScheduleOptions = scheduleOptions; });

            return scheduleModels;
        }

        public async static Task<IEnumerable<ScheduleViewModel>> ReBuildCollectionForEditAsync(
            IEnumerable<ScheduleViewModel> models,
            IScheduleSevice services)
        {
            var projectOptions = await services.GetShdeduleProjectOptions();
            var scheduleOptions = services.GetScheduleOptions();

            models.ToList().ForEach(x =>
            { x.ProjectOptions = projectOptions; x.ScheduleOptions = scheduleOptions; });

            return models;
        }

        public async static Task<ScheduleViewModel> CreateForEditAsync(
            Schedule schedule,
            IScheduleSevice services,
            IMapper mapper)
        {
            var model = mapper.Map<Schedule, ScheduleViewModel>(schedule);
            model.ProjectOptions = await services.GetShdeduleProjectOptions();
            model.ScheduleOptions = services.GetScheduleOptions();
            
            return model;
        }

        public async static Task<ScheduleViewModel> ReBuildForEditAsync(
            ScheduleViewModel model,
            IScheduleSevice services)
        {
            model.ProjectOptions = await services.GetShdeduleProjectOptions();
            model.ScheduleOptions = services.GetScheduleOptions();

            return model;
        }
    }
}
