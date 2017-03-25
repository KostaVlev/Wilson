using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Wilson.Accounting.Data;
using Wilson.Accounting.Data.DataAccess;
using Wilson.Companies.Core.Entities;
using Wilson.Companies.Data;
using Wilson.Companies.Data.DataAccess;
using Wilson.Web.Configurations;
using Wilson.Web.Services;

namespace Wilson.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services
                .AddDbContext<CompanyDbContext>(options => 
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")))
                .AddDbContext<AccountingDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<CompanyDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddTransient<AutoMapper.IConfigurationProvider, MapperConfiguration>();

            services.AddScoped<IMapper>(sp =>
                new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperProfileConfiguration()))));

            // Add application work dbContexts
            services.AddTransient<ICompanyWorkData, CompanyWorkData>();
            services.AddTransient<IAccountingWorkData, AccountingWorkData>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see https://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                // Areas support
                routes.MapRoute(
                  name: "areaRoute",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}");
            });

            // Add default Admin user and roles.
            this.CreateRolesAndAdmin(app).Wait();
        }

        // This method creates default Admin user and roles.
        private async Task CreateRolesAndAdmin(IApplicationBuilder app)
        {
            var roleManager = app.ApplicationServices.GetService<RoleManager<IdentityRole>>();
            var userManager = app.ApplicationServices.GetService<UserManager<User>>();


            // Create Admin role and Admin if not exist.    
            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                // Create Admin role.   
                var role = new IdentityRole()
                {
                    Name = "Admin"
                };

                await roleManager.CreateAsync(role);

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

            // Create User role.    
            if (await roleManager.FindByNameAsync("User") == null)
            {   
                var role = new IdentityRole()
                {
                    Name = "User"
                };

                await roleManager.CreateAsync(role);                
            }

            // Create Accouter role.    
            if (await roleManager.FindByNameAsync("Accouter") == null)
            {
                var role = new IdentityRole()
                {
                    Name = "Accouter"
                };

                await roleManager.CreateAsync(role);
            }
        }
    }
}
