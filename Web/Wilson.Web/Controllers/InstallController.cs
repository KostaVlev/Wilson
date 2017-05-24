using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Wilson.Accounting.Data.DataAccess;
using Wilson.Companies.Core.Entities;
using Wilson.Companies.Data.DataAccess;
using Wilson.Scheduler.Data.DataAccess;
using Wilson.Web.Events;
using Wilson.Web.Events.Interfaces;
using Wilson.Web.Models.InstallViewModels;
using Wilson.Web.Seed;

namespace Wilson.Web.Controllers
{
    public class InstallController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly ILogger logger;
        private readonly IDatabaseSeeder dataSeeder;
        private readonly IRolesSeder rolesSeeder;
        private readonly IServiceScopeFactory services;
        private readonly IEventsFactory eventsFactory;

        public InstallController(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            ICompanyWorkData companyWorkData,
            ISchedulerWorkData schedulerWorkData,
            IAccountingWorkData accountingWorkData,
            IMapper mapper,
            ILoggerFactory loggerFactory,
            IDatabaseSeeder dataSeeder,
            IRolesSeder rolesSeeder,
            IServiceScopeFactory services,
            IEventsFactory eventsFactory)
            : base(companyWorkData, schedulerWorkData, accountingWorkData, mapper)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.dataSeeder = dataSeeder;
            this.rolesSeeder = rolesSeeder;
            this.services = services;
            this.eventsFactory = eventsFactory;
            this.logger = loggerFactory.CreateLogger<InstallController>();
        }

        //
        // GET: /Install/InstallDatabase
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> InstallDatabase()
        {
            var settings = await this.CompanyWorkData.Settings.SingleOrDefaultAsync(x => x.IsDatabaseInstalled);
            if (settings != null)
            {
                return BadRequest($"The database is already installed");
            }

            return View();
        }

        //
        // POST: /Install/InstallDatabase
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InstallDatabase(InstallDatabaseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(InstallDatabaseViewModel.ReBuild(model));
            }

            var systemUser = ApplicationUser.CreateSystemUser(
                model.User.FirstName, model.User.LastName, model.User.Email, model.User.PhoneNumber);

            var maybeUser = await this.userManager.FindByEmailAsync(systemUser.Email);
            if (maybeUser == null)
            {
                var result = await this.userManager.CreateAsync(systemUser, model.User.Password);
                if (!result.Succeeded)
                {
                    this.logger.LogError(2, $"Failed to create system user.");
                    result.Errors.ToList().ForEach(e => this.logger.LogError(2, $"{e.Code} -- {e.Description}"));
                    this.AddErrors(result);

                    return View(InstallDatabaseViewModel.ReBuild(model));
                }

                this.logger.LogInformation(3, $"System user with Id {systemUser.Id} was crated");
            }
            else
            {
                systemUser = maybeUser;
            }
            
            if (this.roleManager.Roles == null || this.roleManager.Roles.Count() == 0)
            {
                this.rolesSeeder.Seed(this.services);
            }

            // Make the user administrator.
            var addToRoleResult = await userManager.AddToRoleAsync(systemUser, Constants.Roles.Administrator);
            if (!addToRoleResult.Succeeded)
            {
                this.logger.LogError(2, $"Failed to add role {Constants.Roles.Administrator} to user with id {systemUser.Id}");
                addToRoleResult.Errors.ToList().ForEach(e => this.logger.LogError(2, $"{e.Code} -- {e.Description}"));
                this.AddErrors(addToRoleResult);

                return View(InstallDatabaseViewModel.ReBuild(model));
            }

            // Create home company.
            var companyAddress = Accounting.Core.Entities.Address.Create(
                model.Company.Address.Country,
                model.Company.Address.PostCode,
                model.Company.Address.City,
                model.Company.Address.Street,
                model.Company.Address.StreetNumber,
                model.Company.Address.Floor,
                model.Company.Address.UnitNumber,
                model.Company.Address.Note);

            var company = Accounting.Core.Entities.Company.Create(
                model.Company.Name,
                model.Company.RegistrationNumber,
                model.Company.OfficeEmail,
                model.Company.OfficePhone,
                companyAddress,
                model.Company.VatNumber);

            this.AccountingWorkData.Companies.Add(company);
            this.AccountingWorkData.Complete();

            this.eventsFactory.Raise(new CompanyCreated(company));

            // Set company Base Pay Rate
            var payRate = Scheduler.Core.Entities.PayRate.Create(
                model.PayRate.Hour, model.PayRate.ExtraHour, model.PayRate.HoidayHour, model.PayRate.BusinessTripHour, true);

            this.SchedulerWorkData.PayRates.Add(payRate);
            this.SchedulerWorkData.Complete();

            // Set database settings to installed and Save Home Company Id.
            this.CompanyWorkData.Settings.Add(Settings.Initialize(company.Id));
            this.CompanyWorkData.Complete();
            
            // Seed the database. Keep this at the end.
            if (model.SeedData)
            {
                this.dataSeeder.Seed(this.services, this.eventsFactory);
                this.logger.LogInformation(3, "Data was seeded into the database.");
            }

            this.logger.LogInformation(3, "The database was installed successfully.");
            return RedirectToAction(nameof(AccountController.Login), "Account");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
