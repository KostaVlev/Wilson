using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Wilson.Companies.Core.Entities;
using Wilson.Companies.Data.DataAccess;
using Wilson.Web.Areas.Admin.Models.ControlPanelViewModels;

namespace Wilson.Web.Areas.Admin.Controllers
{
    public class ControlPanelController : AdminBaseController
    {
        private readonly UserManager<User> userManager;
        private ILogger logger;

        public ControlPanelController(
            ICompanyWorkData companyWorkData, 
            UserManager<User> userManager,
            ILoggerFactory loggerFactory, 
            IMapper mapperr)
            : base(companyWorkData, mapperr)
        {
            this.userManager = userManager;
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
        public IActionResult Register()
        {
            return View();
        }

        //
        // POST: /Admin/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new User { FirstName = model.FirstName, LastName = model.LastName, UserName = model.Email, Email = model.Email };
                var result = await this.userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    this.logger.LogInformation(3, "Admin created new User account with password.");
                    return RedirectToAction(nameof(Index), new { Message = Constants.AccountManageMessagesEn.UserCreated });
                }

                this.AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Admin/ShowAllUsers
        [HttpGet]
        public async Task<IActionResult> ShowAllUsers(string message)
        {
            ViewData["StatusMessage"] = message ?? "";
            var users = await this.CompanyWorkData.Users.GetAllAsync();
            var models = this.Mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(users);

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

            var model = this.Mapper.Map<User, UserViewModel>(user);

            return View(model);
        }

        //
        // POST: /Admin/EditUser
        [HttpPost]
        public async Task<IActionResult> EditUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await this.CompanyWorkData.Users.GetById(model.Id);
                if (user == null)
                {
                    return NotFound();
                }

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;

                var result = await this.CompanyWorkData.CompleteAsync();
                if (result > 0)
                {
                    this.logger.LogInformation(3, string.Format("Admin edited User with Id {0}.", user.ToString()));
                    return RedirectToAction(nameof(ShowAllUsers), new { Message = Constants.AccountManageMessagesEn.UserActivated });
                }

                return RedirectToAction(nameof(ShowAllUsers), new { Message = Constants.AccountManageMessagesEn.Error });
            }

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

            user.IsActive = true;
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

            user.IsActive = false;
            var result = await this.CompanyWorkData.CompleteAsync();
            if (result > 0)
            {
                this.logger.LogInformation(3, string.Format("Admin deactivated User with Id {0}.", id.ToString()));
                return RedirectToAction(nameof(ShowAllUsers), new { Message = Constants.AccountManageMessagesEn.UserDeactivated });
            }

            return RedirectToAction(nameof(ShowAllUsers), new { Message = Constants.AccountManageMessagesEn.Error });
        }
    }
}