using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wilson.Scheduler.Data.DataAccess;

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
                
        public async Task<List<SelectListItem>> GetProjectOptions()
        {
            var projects = await this.SchedulerWorkData.Projects.FindAsync(x => x.IsActive);
            return projects.Select(x => new SelectListItem() { Value = x.Id, Text = x.ShortName }).ToList();
        }
        
        public async Task<List<SelectListItem>> GetEmployeeOptions()
        {
            var employees = await this.SchedulerWorkData.Employees.GetAllAsync();
            return employees.Select(x => new SelectListItem() { Value = x.Id, Text = x.ToString() }).ToList();
        }
        
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
