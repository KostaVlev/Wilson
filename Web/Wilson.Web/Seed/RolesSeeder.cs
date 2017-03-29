using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Wilson.Web.Seed
{
    /// <summary>
    /// This class that seeds default Roles.
    /// </summary>
    public class RolesSeeder : IRolesSeder
    {
        private readonly string[] RoleNames = { "Admin", "User", "Accounter" };

        /// <summary>
        /// Seeds roles for use of the application.
        /// </summary>
        public void Seed(IServiceScopeFactory services)
        {
            using (var scope = services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                SeedRoles(RoleNames, roleManager).Wait();
            }
        }

        private async Task SeedRoles(string[] roleNames, RoleManager<IdentityRole> roleManager)
        {
            foreach (var roleName in roleNames)
            {
                if (await roleManager.FindByNameAsync(roleName) == null)
                {
                    var role = new IdentityRole()
                    {
                        Name = roleName
                    };

                    await roleManager.CreateAsync(role);
                }
            }
        }
    }
}
