using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Wilson.Companies.Core.Entities;
using Wilson.Companies.Core.Enumerations;
using Wilson.Companies.Data.DataAccess;
using Wilson.Web.Areas.Admin.Models.ControlPanelViewModels;
using Wilson.Web.Areas.Admin.Models.SharedViewModels;
using Wilson.Web.Events;
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
        // GET: /Admin/Home
        [HttpGet]
        public IActionResult Index(string message)
        {
            ViewData["StatusMessage"] = message ?? "";
            return View();
        }

        //
        // GET: /Admin/Register
        [HttpGet]
        public IActionResult Register(string message)
        {
            ViewData["StatusMessage"] = message ?? "";
            return View(new RegisterViewModel()
            {
                User = new RegisterUserViewModel()
                {
                    EmployeePositions = this.GetEmployeePositions(),
                    Roles = this.GetApplicationRoles()
                }
            });
        }

        //
        // POST: /Admin/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var settings = await this.GetSettings();
                var role = await this.roleManager.FindByNameAsync(model.User.ApplicationRoleName);
                if (role == null)
                {
                    return View("Error");
                }

                // Continue only if we have settings for the database.
                if (settings != null && !string.IsNullOrEmpty(settings.HomeCompanyId) && settings.IsDatabaseInstalled)
                {
                    // Create employee and save it in the database.
                    var employeeAddress = this.Mapper.Map<AddressViewModel, Address>(model.Address);

                    var employee = Employee.Create(
                        model.User.FirstName,
                        model.User.LastName,
                        model.User.Phone,
                        model.User.PrivatePhone,
                        model.User.Email,
                        model.User.EmployeePosition,
                        employeeAddress,
                        settings.HomeCompanyId);                    
                    
                    this.CompanyWorkData.Employees.Add(employee);
                    var success = await this.CompanyWorkData.CompleteAsync();
                    if (success > 0)
                    {
                        this.EventsFactory.Raise(new EmployeeCreated(employee));
                    }

                    // Create user from the employee.
                    var user = ApplicationUser.Create(employee, employee.Email);
                    var result = await this.userManager.CreateAsync(user, model.User.Password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, role.Name);
                        this.logger.LogInformation(3, "Admin created new User account with password.");  
                        return RedirectToAction(nameof(Index), new { Message = Constants.AccountManageMessagesEn.UserCreated });
                    }

                    this.AddErrors(result);
                }
                else
                {
                    return RedirectToAction(nameof(Created), new { Message = Constants.ExceptionMessages.DatabaseNotInstalled });
                }
            }

            // If we got this far, something failed, redisplay form
            model.User.EmployeePositions = this.GetEmployeePositions();
            model.User.Roles = this.GetApplicationRoles();
            return View(model);
        }

        //
        // GET: /Admin/ShowAllUsers
        [HttpGet]
        public async Task<IActionResult> ShowAllUsers(string message)
        {
            ViewData["StatusMessage"] = message ?? "";
            var users = await this.CompanyWorkData.Users.GetAllAsync();
            var models = this.Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<UserViewModel>>(users);

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
                return NotFound();
            }

            var model = this.Mapper.Map<ApplicationUser, UserViewModel>(user);
            var roleNames = await this.userManager.GetRolesAsync(user);
            model.Roles = this.GetApplicationRoles();            
            model.ApplicationRoleName = roleNames.FirstOrDefault();

            return View(model);
        }

        //
        // POST: /Admin/EditUser
        [HttpPost]
        public async Task<IActionResult> EditUser(UserViewModel model)
        {
            var user = await this.userManager.FindByIdAsync(model.Id);
            if (ModelState.IsValid)
            {                
                if (user == null)
                {
                    return NotFound();
                }

                var newRole = await this.roleManager.FindByNameAsync(model.ApplicationRoleName);
                var oldRoles = await this.userManager.GetRolesAsync(user);
                if (newRole == null)
                {
                    return View("Error");
                }

                await this.userManager.RemoveFromRolesAsync(user, oldRoles);
                await this.userManager.AddToRoleAsync(user, newRole.Name);

                user.ChangeNames(model.FirstName, model.LastName);
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;

                var result =  await this.userManager.UpdateAsync(user);
                if (result.Succeeded)
                {       
                    this.logger.LogInformation(3, string.Format("Admin edited User with Id {0}.", user.ToString()));
                    return RedirectToAction(nameof(ShowAllUsers), new { Message = Constants.AccountManageMessagesEn.UserEdit });
                }

                return RedirectToAction(nameof(ShowAllUsers), new { Message = Constants.AccountManageMessagesEn.Error });
            }

            var roleNames = await this.userManager.GetRolesAsync(user);
            model.Roles = this.GetApplicationRoles();
            model.ApplicationRoleName = roleNames.FirstOrDefault();
            return View(model);
        }

        //
        // POST: /Admin/ActivateUser
        [HttpPost]
        public async Task<IActionResult> ActivateUser(string id)
        {
            var user = await this.CompanyWorkData.Users.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Activate();
            var result = await this.CompanyWorkData.CompleteAsync();
            if (result > 0)
            {
                this.logger.LogInformation(3, string.Format("Admin activated User with Id {0}.", id.ToString()));
                return RedirectToAction(nameof(ShowAllUsers), new { Message = Constants.AccountManageMessagesEn.UserActivated });
            }

            return RedirectToAction(nameof(ShowAllUsers), new { Message = Constants.AccountManageMessagesEn.Error });
        }

        //
        // POST: /Admin/DeactivateUser
        [HttpPost]
        public async Task<IActionResult> DeactivateUser(string id)
        {
            var user = await this.CompanyWorkData.Users.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            var isAdmin = await this.userManager.IsInRoleAsync(user, Constants.Roles.Administrator);

            if (isAdmin)
            {
                return RedirectToAction(nameof(ShowAllUsers), new { Message = Constants.AccountManageMessagesEn.Error });
            }

            user.Deactivate();
            var result = await this.CompanyWorkData.CompleteAsync();
            if (result > 0)
            {
                this.logger.LogInformation(3, string.Format("Admin deactivated User with Id {0}.", id.ToString()));
                return RedirectToAction(nameof(ShowAllUsers), new { Message = Constants.AccountManageMessagesEn.UserDeactivated });
            }

            return RedirectToAction(nameof(ShowAllUsers), new { Message = Constants.AccountManageMessagesEn.Error });
        }



        private List<SelectListItem> GetEmployeePositions()
        {
            var positions = Enum.GetValues(typeof(EmployeePosition)).Cast<EmployeePosition>().Select(x => new SelectListItem
            {
                // Try to get the Employee position name from the DisplayAttribute.
                Text = x.GetType()
                        .GetMember(x.ToString())
                        .FirstOrDefault()
                        .GetCustomAttribute<DisplayAttribute>().Name ?? x.ToString(),
                Value = ((int)x).ToString()
            }).ToList();

            return positions;
        }
        private List<SelectListItem> GetApplicationRoles()
        {
            var roles = this.roleManager.Roles;
            return roles.Select(x => new SelectListItem()
            {
                Value = x.Name,
                Text = $"{x.Name} | {x.Description}",
                Selected = false
            })
            .ToList();
        }
    }
}