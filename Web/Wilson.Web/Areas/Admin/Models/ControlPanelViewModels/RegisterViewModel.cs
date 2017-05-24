using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wilson.Companies.Core.Entities;
using Wilson.Companies.Data.DataAccess;

namespace Wilson.Web.Areas.Admin.Models.ControlPanelViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Employee")]
        public string EmployeeId { get; set; }

        public RegisterUserViewModel User { get; set; }

        public List<SelectListItem> Employees { get; set; }

        public async static Task<RegisterViewModel> CreateAsync(
            RoleManager<ApplicationRole> roleManager, 
            ICompanyWorkData companyWorkData)
        {
            return new RegisterViewModel()
            {
                User = RegisterUserViewModel.Create(roleManager),
                Employees = await CreateEmployeesDropDownList(companyWorkData)
            };
        }

        public async static Task<RegisterViewModel> ReBuildAsync(
            RegisterViewModel model, 
            RoleManager<ApplicationRole> roleManager, 
            UserManager<ApplicationUser> userManager, 
            ICompanyWorkData companyWorkData)
        {
            model.User = RegisterUserViewModel.ReBuild(model.User, roleManager);
            model.Employees = await CreateEmployeesDropDownList(companyWorkData);

            return model;
        }

        private static async Task<List<SelectListItem>> CreateEmployeesDropDownList(ICompanyWorkData companyWorkData)
        {
            var employees = await companyWorkData.Employees.GetAllAsync();
            return employees.Select(x => new SelectListItem
            {
                Text = x.GetName(),
                Value = x.Id
            }).ToList();
        }
    }
}
