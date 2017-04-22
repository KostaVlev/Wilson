using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wilson.Scheduler.Data.DataAccess;
using System;

namespace Wilson.Web.Areas.Scheduler.Services
{
    public abstract class Service : IService
    {
        public Service(IMapper mapper)
        {
            this.Mapper = mapper;
        }

        public ISchedulerWorkData SchedulerWorkData { get; set; }

        public IMapper Mapper { get; set; }

        /// <summary>
        /// Asynchronous method that creates Collection of Project as options for drop-down lists.
        /// </summary>
        /// <returns><see cref="List{T}"/> where {T} is <see cref="SelectListItem"/> with 
        /// value the <see cref="ProjectViewModel.Id"/> and text <see cref="ProjectViewModel.ShortName"/></returns>
        /// <example>await GetProjectOptions(...)</example>
        public async Task<List<SelectListItem>> GetProjectOptions()
        {
            var projects = await this.SchedulerWorkData.Projects.FindAsync(x => x.IsActive);
            return projects.Select(x => new SelectListItem() { Value = x.Id, Text = x.ShortName }).ToList();
        }

        /// <summary>
        /// Asynchronous method that creates Collection of Employees as options for drop-down lists.
        /// </summary>
        /// <returns><see cref="List{T}"/> where {T} is <see cref="SelectListItem"/> with 
        /// value the <see cref="EmployeeConciseViewModel.Id"/> and text <see cref="EmployeeConciseViewModel.ToString()"/></returns>
        /// <example>await GetEmployeeOptions(...)</example>
        public async Task<List<SelectListItem>> GetEmployeeOptions()
        {
            var employees = await this.SchedulerWorkData.Employees.GetAllAsync();
            return employees.Select(x => new SelectListItem() { Value = x.Id, Text = x.ToString() }).ToList();
        }

        /// <summary>
        /// Creates Collection of periods in format MM/YYYY.
        /// </summary>
        /// <returns><see cref="List{T}"/> where {T} is <see cref="SelectListItem"/> with 
        /// value the MM/YYYY and text MM/YYYY</returns>
        public List<SelectListItem> GetPeriodsOptions()
        {
            var options = new List<SelectListItem>();
            var years = Enumerable.Range(DateTime.Today.AddYears(-5).Year, 6).Reverse();
            var months = Enumerable.Range(1, 12);

            foreach (var year in years)
            {
                foreach (var month in months)
                {
                    string optionText = this.CrteatePeriodOptionText(month, year);                   
                    var option = new SelectListItem()
                    {
                        Text = optionText,
                        Value = optionText
                    };

                    options.Add(option);
                }
            }

            return options;
        }

        private string CrteatePeriodOptionText(int month, int year)
        {
            string monthText = month.ToString().Length == 1 ? month.ToString().PadLeft(2, '0') : month.ToString();
            string yearText = year.ToString();

            return string.Format("{0}/{1}", monthText, yearText);
        }
    }
}
