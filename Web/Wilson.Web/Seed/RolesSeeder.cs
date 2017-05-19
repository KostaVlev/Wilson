using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Wilson.Companies.Core.Entities;

namespace Wilson.Web.Seed
{
    /// <summary>
    /// This class that seeds default Roles.
    /// </summary>
    public class RolesSeeder : IRolesSeder
    {
        /// <summary>
        /// Seeds roles for use of the application.
        /// </summary>
        public void Seed(IServiceScopeFactory services)
        {
            using (var scope = services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

                SeedRoles(this.GetDefaulteRoles(), roleManager).Wait();
            }
        }

        private async Task SeedRoles(ApplicationRole[] roles, RoleManager<ApplicationRole> roleManager)
        {
            foreach (var role in roles)
            {
                if (await roleManager.FindByIdAsync(role.Id) == null)
                {
                    await roleManager.CreateAsync(role);
                }
                else
                {
                    await roleManager.UpdateAsync(role);
                }
            }
        }

        private ApplicationRole[] GetDefaulteRoles()
        {
            var admin = ApplicationRole.Create(
                Constants.Roles.Administrator, 
                "Administrator account gives the user full rights and access over the application.");

            var user = ApplicationRole.Create(
                Constants.Roles.User,
                "Standard user account gives access to the common application futures - Recommended.");

            var accounter = ApplicationRole.Create(
                Constants.Roles.Accouter,
                "Accouter account have all the rights like the User account but also have access to the Accounting module.");

            return new ApplicationRole[] { admin, user, accounter };
        }
    }
}
