using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wilson.Scheduler.Core.Entities;
using Wilson.Scheduler.Core.Enumerations;
using Wilson.Web.Areas.Scheduler.Models.HomeViewModels;
using Wilson.Web.Areas.Scheduler.Models.SharedViewModels;
using Wilson.Scheduler.Data.DataAccess;

namespace Wilson.Web.Areas.Scheduler.Services
{
    public interface IScheduleSevice
    {
        /// <summary>
        /// Scheduler context that will be used for Db Operations.
        /// </summary>
        ISchedulerWorkData SchedulerWorkData { get; set; }

        /// <summary>
        /// Asynchronous method that finds Schedule by given ID.
        /// </summary>
        /// <param name="id">ID to search.</param>
        /// <returns><see cref="Schedule"/></returns>
        Task<Schedule> FindScheduleById(string id);

        /// <summary>
        /// Gets the schedule options form <see cref="ScheduleOption"/> which are used for drop-down lists.
        /// </summary>
        /// <returns><see cref="List{T}"/> where {T} is <see cref="SelectListItem"/> with value equal to the
        /// <see cref="ScheduleOption"/> value and text taken the <see cref="DisplayAttribute"/></returns>
        List<SelectListItem> GetScheduleOptions();

        /// <summary>
        /// Gets the <see cref="ScheduleOption"/> name from the <see cref="DisplayAttribute"/>.
        /// </summary>
        /// <param name="scheduleOption"></param>
        /// <returns>The name as <see cref="string"/>.</returns>
        string GetShceduleOptionName(ScheduleOption scheduleOption);        

        /// <summary>
        /// Asynchronous method that populates <see cref="EmployeeViewModel.NewSchedule"/> property.
        /// </summary>
        /// <param name="employeeModels">Collection of <see cref="EmployeeViewModel"/> which will be processed.</param>
        Task SetupEmployeeNewSchedule(IEnumerable<EmployeeViewModel> employeeModels);

        /// <summary>
        /// Asynchronous method that prepares <see cref="ScheduleViewModel"/> for displaying.
        /// </summary>
        /// <param name="schedules">Collection of <see cref="Schedule"/> which will be processed.</param>
        /// <returns>Collection of <see cref="ScheduleViewModel"/></returns>
        Task<IEnumerable<ScheduleViewModel>> SetupScheduleModelForEdit(IEnumerable<Schedule> schedules);

        /// <summary>
        /// Asynchronous method that prepares <see cref="ScheduleViewModel"/> for displaying.
        /// </summary>
        /// <param name="scheduleModels">Collection of <see cref="Schedule"/> which will be processed.</param>
        Task SetupScheduleModelForEdit(IEnumerable<ScheduleViewModel> scheduleModels);

        /// <summary>
        /// Asynchronous method that prepares <see cref="ScheduleViewModel"/> for displaying.
        /// </summary>
        /// <param name="employeeModel"><see cref="Schedule"/> model which will be processed.</param>
        /// <returns><see cref="ScheduleViewModel"/></returns>
        Task<ScheduleViewModel> SetupScheduleModelForEdit(Schedule schedule);

        /// <summary>
        /// Asynchronous method that prepares <see cref="ScheduleViewModel"/> for displaying.
        /// </summary>
        /// <param name="employeeModel"><see cref="ScheduleViewModel"/> model which will be processed.</param>
        Task SetupScheduleModelForEdit(ScheduleViewModel schedule);

        /// <summary>
        /// Asynchronous method that creates <see cref="EmployeesShceduleViewModel"/> ready for displaying.
        /// </summary>
        /// <example>await PrepareEmployeesShceduleViewModel()</example>
        Task<EmployeesShceduleViewModel> PrepareEmployeesShceduleViewModel();

        /// <summary>
        /// Asynchronous method that creates collection of <see cref="EmployeeViewModel"/> with required schedules.
        /// </summary>
        /// <param name="from">Start date. Default start date is 7 days ago. Null will select all dates.</param>
        /// <param name="to">End date. Default end date is Today. Null will select all dates.</param>
        /// <param name="employeeId">The Employee ID. Null will select all Employees.</param>
        /// <param name="projectId">The Project ID. Null will select all Projects.</param>
        /// <param name="scheduleOption"><see cref="ScheduleOption"/>. Null will select all Schedule Options.</param>
        /// <returns><see cref="IEnumerable{T}"/> where {T} is <see cref="EmployeeViewModel"/>.</returns>
        Task<IEnumerable<EmployeeViewModel>> FindEmployeeSchedules(
            DateTime? from = null, DateTime? to = null, string employeeId = null,
            string projectId = null, ScheduleOption? scheduleOption = null);

        /// <summary>
        /// Asynchronous method that creates <see cref="SearchViewModel"/>.
        /// </summary>
        /// <param name="employees">Collection which contains the found employees.</param>
        /// <returns><see cref="SearchViewModel"/></returns>
        /// <example>await SetupSearchModel()</example>
        Task<SearchViewModel> SetupSearchModel(IEnumerable<EmployeeViewModel> employees = null);

        /// <summary>
        /// Asynchronous method that finds all the schedules for a given date.
        /// </summary>
        /// <param name="dateTime">The date to search for.</param>
        /// <returns>Collection of <see cref="Schedule"/></returns>
        Task<IEnumerable<Schedule>> FindAllSchedulesForDate(string dateTime);
    }
}
