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
using Wilson.Web.Models.SharedViewModels;
using Wilson.Web.Seed;

namespace Wilson.Web.Controllers
{

    public class InstallController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILogger logger;
        private readonly IDatabaseSeeder dataSeeder;
        private readonly IRolesSeder rolesSeeder;
        private readonly IServiceScopeFactory services;
        private readonly IEventsFactory eventsFactory;

        public InstallController(
            UserManager<ApplicationUser> userManager, 
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
            // Check again if a database was previously installed and return Error if true because in this case we should not
            // be here and don't give explanations about the error.
            var query = await this.CompanyWorkData.Settings.GetAllAsync();
            var settings = query.FirstOrDefault();
            if (settings != null)
            {
                if (!settings.IsDatabaseInstalled)
                {
                    return View("Error");
                }                
            }

            return View();
        }

        //
        // POST: /Install/InstallDatabase
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> InstallDatabase(InstallDatabaseViewModel model)
        {
            if (ModelState.IsValid)
            {
                var systemUser = this.Mapper.Map<UserViewModel, ApplicationUser>(model.User);

                // Set the Username!
                systemUser.UserName = model.User.Email;

                // Create User.
                var result = await this.userManager.CreateAsync(systemUser, model.User.Password);
                if (result.Succeeded)
                {
                    this.logger.LogInformation(3, "Admin was created.");
                    this.rolesSeeder.Seed(this.services);
                    this.logger.LogInformation(3, "Roles were seeded into the database.");

                    // Make the user administrator.
                    await userManager.AddToRoleAsync(systemUser, Constants.Roles.Administrator);

                    // Create home company.
                    var companyAddress = this.Mapper.Map<AddressViewModel, Accounting.Core.Entities.Address>(model.Company.Address);
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
                    model.PayRate.IsBaseRate = true;
                    var payRate = this.Mapper.Map<PayRateViewModel, Scheduler.Core.Entities.PayRate>(model.PayRate);

                    this.SchedulerWorkData.PayRates.Add(payRate);

                    // Set database settings to installed and Save Home Company Id.
                    this.CompanyWorkData.Settings.Add(new Settings() { IsDatabaseInstalled = true, HomeCompanyId = company.Id });
                    
                    // Save all changes. Don't use async here because the Data seeder might access the db before they to be completed.
                    this.CompanyWorkData.Complete();
                    this.SchedulerWorkData.Complete();
                     
                    // Seed the database. Keep this at the end.
                    if (model.SeedData)
                    {
                        this.dataSeeder.Seed(this.services, this.eventsFactory);
                        this.logger.LogInformation(3, "Data was seeded into the database.");
                    }                    

                    this.logger.LogInformation(3, "The database was installed successfully.");
                    return RedirectToAction(nameof(AccountController.Login), "Account");
                }

                this.AddErrors(result);
            }

            return View(model);
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
