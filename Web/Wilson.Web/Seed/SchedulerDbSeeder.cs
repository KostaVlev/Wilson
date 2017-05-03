using System.Collections.Generic;
using System.Linq;
using Wilson.Companies.Data;
using Wilson.Projects.Data;
using Wilson.Scheduler.Core.Entities;
using Wilson.Scheduler.Data;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Wilson.Web.Seed
{
    public class SchedulerDbSeeder
    {
        private static IEnumerable<Project> projects;
        private static IEnumerable<Employee> employees;

        /// <summary>
        /// Seeds the data for the Scheduler module.
        /// </summary>
        /// <param name="services">The service factory.</param>
        public static void Seed(IServiceScopeFactory services)
        {
            using (var scope = services.CreateScope())
            {
                var schedulerDb = scope.ServiceProvider.GetRequiredService<SchedulerDbContext>();
                var projectsDb = scope.ServiceProvider.GetRequiredService<ProjectsDbContext>();
                var companyDb = scope.ServiceProvider.GetRequiredService<CompanyDbContext>();

                // Keep the following methods in this exact order.
                SeedEmployees(schedulerDb, companyDb, !schedulerDb.Set<Employee>().Any(), out employees);
                SeedProjects(schedulerDb, projectsDb, !schedulerDb.Set<Project>().Any(), out projects);

                schedulerDb.SaveChanges();
            }
        }

        private static void SeedProjects(
            SchedulerDbContext schedulerDb,
            ProjectsDbContext projectsDb,
            bool hasProjects,
            out IEnumerable<Project> projetcs)
        {
            if (hasProjects)
            {
                var dbProjects = projectsDb.Projects.ToList();
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Projects.Core.Entities.Project, Project>();
                });

                projetcs = Mapper.Map<IEnumerable<Projects.Core.Entities.Project>, IEnumerable<Project>>(dbProjects);
                foreach (var project in projects)
                {
                    project.SetShortName();
                }

                schedulerDb.Set<Project>().AddRange(projetcs);
            }
            else
            {
                projetcs = schedulerDb.Set<Project>().ToList();
            }
        }

        private static void SeedEmployees(
            SchedulerDbContext schedulerDb,
            CompanyDbContext companyDb,
            bool hasEmplyees,
            out IEnumerable<Employee> employees)
        {
            if (hasEmplyees)
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Companies.Core.Entities.Employee, Employee>();
                });

                var payRate = schedulerDb.PayRates.FirstOrDefault();
                var dbEmployees = companyDb.Employees;
                employees = Mapper.Map<IEnumerable<Companies.Core.Entities.Employee>, IEnumerable<Employee>>(dbEmployees);
                foreach (var employee in employees)
                {
                    employee.ApplayPayRate(payRate);
                }

                schedulerDb.Set<Employee>().AddRange(employees);
            }
            else
            {
                employees = schedulerDb.Set<Employee>().ToList();
            }
        }
    }
}
