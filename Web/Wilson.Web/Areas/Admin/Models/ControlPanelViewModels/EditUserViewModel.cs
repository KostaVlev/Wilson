using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Wilson.Companies.Core.Entities;

namespace Wilson.Web.Areas.Admin.Models.ControlPanelViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }
        
        [Required]
        [Display(Name = "Role")]
        public string ApplicationRoleName { get; set; }

        public bool IsActive { get; set; }

        public IEnumerable<SelectListItem> ApplicationRoles { get; set; }
        
        public static async Task<EditUserViewModel> Create(
            ApplicationUser user,
            RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            var userRoles = await userManager.GetRolesAsync(user);

            return new EditUserViewModel()
            {
                Id = user.Id,
                FullName = user.GetName(),
                PhoneNumber = user.PhoneNumber,
                ApplicationRoleName = userRoles.FirstOrDefault(),
                ApplicationRoles = GetApplicationRoles(roleManager)
            };
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
