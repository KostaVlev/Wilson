using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wilson.Companies.Core.Entities;
using Wilson.Companies.Data.DataAccess;
using Wilson.Scheduler.Data.DataAccess;

namespace Wilson.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger logger;

        public HomeController(
            ICompanyWorkData companyWorkData, 
            ISchedulerWorkData schedulerWorkData, 
            IMapper mapper,
            ILoggerFactory loggerFactory)
            : base(companyWorkData, schedulerWorkData, mapper)
        {
            this.logger = loggerFactory.CreateLogger<HomeController>();
        }

        public IActionResult Index(string message)
        {
            ViewData["StatusMessage"] = message ?? "";

            return View();
        }

        public async Task<IActionResult> Sync()
        {
            string message = string.Empty;            
            try
            {
                await this.SyncCompanySchedulerEmployees();
                await this.SyncCompanySchedulerProjects();
                await this.SaveChangesInAllDbContexts();
                message = Constants.SuccessMessages.DatabaseUpdateSuccess;
            }
            catch (DbUpdateException e)
            {
                logger.LogError(e.Message);
                message = Constants.ExceptionMessages.DatabaseUpdateError;
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                message = Constants.ExceptionMessages.DatabaseUpdateError;
            }

            return RedirectToAction(nameof(Index), new { Message = message});
        }

        public IActionResult Error()
        {
            return View();
        }        

        private async Task SaveChangesInAllDbContexts()
        {
            await this.SchedulerWorkData.CompleteAsync();
            await this.CompanyWorkData.CompleteAsync();
        }

        private async Task SyncCompanySchedulerEmployees()
        {
            var companyEmployees = await this.CompanyWorkData.Employees.GetAllAsync();
            var schedulerEmployees = await this.SchedulerWorkData.Employees.GetAllAsync();

            // Get the Base Pay Rate that will be assigned to each new employee.
            var basePayRate = await this.SchedulerWorkData.PayRates.FindAsync(x => x.IsBaseRate);
            var basePayRateId = basePayRate.FirstOrDefault().Id;

            // Get the new employees.
            var employeesToAddToSheduler = companyEmployees.Where(x => !schedulerEmployees.Any() || !schedulerEmployees.Any(e => e.Id == x.Id));
            var newSchedulerEmployees =
                this.Mapper.Map<IEnumerable<Employee>, IEnumerable<Scheduler.Core.Entities.Employee>>(employeesToAddToSheduler);

            // Assigned Base Pay Rate for each new employee.
            foreach (var emp in newSchedulerEmployees)
            {
                emp.PayRateId = basePayRateId;
            }

            this.SchedulerWorkData.Employees.AddRange(newSchedulerEmployees);
        }

        private async Task SyncCompanySchedulerProjects()
        {
            var companyProjects = await this.CompanyWorkData.Projects.FindAsync(x => x.IsActive);
            var schedulerpProjects = await this.SchedulerWorkData.Projects.FindAsync(x => x.IsActive);

            var projectsToAddToScheduler = companyProjects.Where(x => !schedulerpProjects.Any() || !schedulerpProjects.Any(e => e.Id == x.Id));
            var newSchedulerProjects =
                this.Mapper.Map<IEnumerable<Project>, IEnumerable<Scheduler.Core.Entities.Project>>(projectsToAddToScheduler);

            var maxDatabaseColumnLength = 4;
            foreach (var project in newSchedulerProjects)
            {                
                project.ShortName = project.Name.Substring(0, maxDatabaseColumnLength).ToUpper();
            }

            this.SchedulerWorkData.Projects.AddRange(newSchedulerProjects);
        }
    }
}
