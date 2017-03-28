using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Wilson.Companies.Core.Entities;

namespace Wilson.Web.Database
{
    /// <summary>
    /// Static class that seeds default Admin user and Roles.
    /// </summary>
    public static class AdminAndRolesSeeder
    {
        private static readonly string[] RoleNames = { "Admin", "User", "Accounter" };

        /// <summary>
        /// Seeds Admin user and roles for use of the application.
        /// </summary>
        /// <param name="host">The host <see cref="IWebHost"/> which will provide the required services.</param>
        public static void Seed(IWebHost host)
        {
            var services = (IServiceScopeFactory)host.Services.GetService(typeof(IServiceScopeFactory));
            using (var scope = services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                SeedRoles(RoleNames, roleManager).Wait();
                SeedAdministrator(userManager).Wait();
            }
        }

        private static async Task SeedRoles(string[] roleNames, RoleManager<IdentityRole> roleManager)
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

        private static async Task SeedAdministrator(UserManager<User> userManager)
        {
            // Create a Admin user.
            var user = new User()
            {
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "admin@wilson.com",
                Email = "admin@wilson.com"
            };

            string password = "Q!w2e3r4";
            var admin = await userManager.CreateAsync(user, password);

            // Add Role Admin to the Admin.   
            if (admin.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
