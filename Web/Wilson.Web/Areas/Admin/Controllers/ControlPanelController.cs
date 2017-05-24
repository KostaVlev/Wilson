using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Wilson.Companies.Core.Entities;
using Wilson.Companies.Data.DataAccess;
using Wilson.Web.Areas.Admin.Models.ControlPanelViewModels;
using Wilson.Web.Events.Interfaces;

namespace Wilson.Web.Areas.Admin.Controllers
{
    public class ControlPanelController : AdminBaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private ILogger logger;

        public ControlPanelController(
            ICompanyWorkData companyWorkData,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            ILoggerFactory loggerFactory,
            IMapper mapperr,
            IEventsFactory eventsFactory)
            : base(companyWorkData, mapperr, eventsFactory)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.logger = loggerFactory.CreateLogger<ControlPanelController>();
        }

        //
        // GET: /Admin/Index
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //
        // GET: /Admin/Register
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View(await RegisterViewModel.CreateAsync(this.roleManager, this.CompanyWorkData));
        }

        //
        // POST: /Admin/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(await RegisterViewModel.ReBuildAsync(model, this.roleManager, this.userManager, this.CompanyWorkData));
            }

            var settings = await this.GetSettings();
            if (settings == null && string.IsNullOrEmpty(settings.HomeCompanyId) && !settings.IsDatabaseInstalled)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Application settings can not be found. Try again and if the problem persist contact the administrator.");

                ModelState.Values.ToList()
                    .ForEach(m => m.Errors.ToList()
                    .ForEach(e => this.logger.LogError(2, e.ErrorMessage)));

                return View(await RegisterViewModel.ReBuildAsync(model, this.roleManager, this.userManager, this.CompanyWorkData));
            }

            var role = await this.roleManager.FindByNameAsync(model.User.ApplicationRoleName);
            if (role == null)
            {
                ModelState.AddModelError(nameof(model.User.ApplicationRoleName), "Application role not found!");
                ModelState.Values.ToList()
                    .ForEach(m => m.Errors.ToList()
                    .ForEach(e => this.logger.LogError(2, e.ErrorMessage)));

                return View(await RegisterViewModel.ReBuildAsync(model, this.roleManager, this.userManager, this.CompanyWorkData));
            }

            var employee = await this.CompanyWorkData.Employees.GetById(model.EmployeeId);
            if (employee == null)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Employee not found. Please try again.");

                ModelState.Values.ToList()
                    .ForEach(m => m.Errors.ToList()
                    .ForEach(e => this.logger.LogError(2, e.ErrorMessage)));

                return View(await RegisterViewModel.ReBuildAsync(model, this.roleManager, this.userManager, this.CompanyWorkData));
            }

            // Create user from the employee.
            var user = ApplicationUser.Create(employee, employee.Email);
            var result = await this.userManager.CreateAsync(user, model.User.Password);
            if (!result.Succeeded)
            {
                this.logger.LogError(2, "Failed to create user.");
                result.Errors.ToList().ForEach(e => this.logger.LogError(2, $"{e.Code} -- {e.Description}"));
                this.AddErrors(result);

                return View(await RegisterViewModel.ReBuildAsync(model, this.roleManager, this.userManager, this.CompanyWorkData));
            }

            var addToRoleResult = await userManager.AddToRoleAsync(user, role.Name);
            if (!addToRoleResult.Succeeded)
            {
                this.logger.LogError(2, $"Failed to add user to role. {role.Name}");
                addToRoleResult.Errors.ToList().ForEach(e => this.logger.LogError(2, $"{e.Code} -- {e.Description}"));
                this.AddErrors(addToRoleResult);

                return View(await RegisterViewModel.ReBuildAsync(model, this.roleManager, this.userManager, this.CompanyWorkData));
            }

            this.logger.LogInformation(3, "Admin created new User account with password.");
            return RedirectToAction(nameof(ShowAllUsers));
        }

        //
        // GET: /Admin/ShowAllUsers
        [HttpGet]
        public async Task<IActionResult> ShowAllUsers()
        {
            var users = await this.CompanyWorkData.Users.GetAllAsync();
            var models = this.Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ShortUserViewModel>>(users);

            return View(models);
        }

        //
        // GET: /Admin/EditUser
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await this.CompanyWorkData.Users.GetById(id);
            if (user == null)
            {
                this.logger.LogError(2, $"User with Id {id} not found.");
                return NotFound($"User with Id {id} not found.");
            }

            return View(await EditUserViewModel.Create(user, this.roleManager, this.userManager, this.Mapper));
        }

        //
        // POST: /Admin/EditUser
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await this.userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                this.logger.LogError(2, $"User with Id {model.Id} not found.");
                return NotFound($"User with Id {model.Id} not found.");
            }

            if (!ModelState.IsValid)
            {
                return View(await EditUserViewModel.Create(user, this.roleManager, this.userManager, this.Mapper));
            }

            if (!(await this.userManager.IsInRoleAsync(user, model.ApplicationRoleName)))
            {
                var newRole = await this.roleManager.FindByNameAsync(model.ApplicationRoleName);
                if (newRole == null)
                {
                    ModelState.AddModelError(
                        string.Empty,
                        $"Application role {model.ApplicationRoleName} not found.");

                    ModelState.Values.ToList()
                        .ForEach(m => m.Errors.ToList()
                        .ForEach(e => this.logger.LogError(2, e.ErrorMessage)));

                    return View(await EditUserViewModel.Create(user, this.roleManager, this.userManager, this.Mapper));
                }

                var oldRoles = await this.userManager.GetRolesAsync(user);
                await this.userManager.RemoveFromRolesAsync(user, oldRoles);
                await this.userManager.AddToRoleAsync(user, newRole.Name);
            }

            user.UpdatePhone(model.PhoneNumber);
            var result = await this.userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                this.AddErrors(result);
                return View(await EditUserViewModel.Create(user, this.roleManager, this.userManager, this.Mapper));
            }

            this.logger.LogInformation(3, string.Format("Admin edited User with Id {0}.", user.Id));
            return RedirectToAction(nameof(ShowAllUsers));
        }

        //
        // POST: /Admin/ActivateUser
        [HttpPost]
        public async Task<IActionResult> ActivateUser(string id)
        {
            var user = await this.CompanyWorkData.Users.GetById(id);
            if (user == null)
            {
                this.logger.LogError(2, $"User with Id {id} not found.");
                return NotFound($"User with Id {id} not found.");
            }

            user.Activate();
            await this.CompanyWorkData.CompleteAsync();
            this.logger.LogInformation(3, string.Format("Admin activated User with Id {0}.", id));

            return RedirectToAction(nameof(ShowAllUsers));
        }

        //
        // POST: /Admin/DeactivateUser
        [HttpPost]
        public async Task<IActionResult> DeactivateUser(string id)
        {
            var user = await this.CompanyWorkData.Users.GetById(id);
            if (user == null)
            {
                this.logger.LogError(2, $"User with Id {id} not found.");
                return NotFound($"User with Id {id} not found.");
            }

            var isAdmin = await this.userManager.IsInRoleAsync(user, Constants.Roles.Administrator);
            if (isAdmin)
            {
                return BadRequest($"Can not deactivate Administrator.");
            }

            user.Deactivate();
            await this.CompanyWorkData.CompleteAsync();
            this.logger.LogInformation(3, string.Format("Admin deactivated User with Id {0}.", id.ToString()));

            return RedirectToAction(nameof(ShowAllUsers));
        }
    }
}