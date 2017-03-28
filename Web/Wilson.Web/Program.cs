using System.IO;
using Microsoft.AspNetCore.Hosting;
using Wilson.Web.Database;

namespace Wilson.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            // Seed in the database default Admin user and Roles.
            AdminAndRolesSeeder.Seed(host);

            // Seed some data in the database.
            DatabaseSeeder.Seed(host);

            host.Run();
        }
    }
}
