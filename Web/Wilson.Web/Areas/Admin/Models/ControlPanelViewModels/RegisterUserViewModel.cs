using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wilson.Companies.Core.Entities;

namespace Wilson.Web.Areas.Admin.Models.ControlPanelViewModels
{
    public class RegisterUserViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Role")]
        public string ApplicationRoleName { get; set; }

        public IEnumerable<SelectListItem> ApplicationRoles { get; set; }

        public static RegisterUserViewModel Create(RoleManager<ApplicationRole> roleManager)
        {
            return new RegisterUserViewModel() { ApplicationRoles = GetApplicationRoles(roleManager) };
        }
        
        public static RegisterUserViewModel ReBuild(RegisterUserViewModel model, RoleManager<ApplicationRole> roleManager)
        {
            model.ApplicationRoles = GetApplicationRoles(roleManager);

            return model;
        }

        private static List<SelectListItem> GetApplicationRoles(RoleManager<ApplicationRole> roleManager)
        {
            return roleManager.Roles.Select(x => new SelectListItem()
            {
                Value = x.Name,
                Text = $"{x.Name} | {x.Description}",
                Selected = false
            }).ToList();
        }
    }
}
