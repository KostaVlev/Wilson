using System;
using System.Collections.Generic;
using System.Linq;
using Wilson.Projects.Core.Entities;
using Wilson.Projects.Data;
using Microsoft.Extensions.DependencyInjection;
using Wilson.Web.Events;
using Wilson.Web.Events.Interfaces;

namespace Wilson.Web.Seed
{
    public class ProjectsDbSeeder
    {
        private static IEnumerable<Project> projects;

        /// <summary>
        /// Seeds the data for the Projects module.
        /// </summary>
        /// <param name="services">The service factory.</param>
        public static void Seed(IServiceScopeFactory services, IEventsFactory eventsFactory)
        {
            using (var scope = services.CreateScope())
            {
                var projectsDb = scope.ServiceProvider.GetRequiredService<ProjectsDbContext>();

                // Keep the following methods in this exact order.
                SeedProjects(projectsDb, out projects);

                projectsDb.SaveChanges();
                eventsFactory.Raise(new ProjectCreated(projects));
            }
        }

        private static void SeedProjects(
            ProjectsDbContext projectsDb,
            out IEnumerable<Project> projetcs)
        {
            var customer = projectsDb.Companies.Skip(1).FirstOrDefault();
            if (customer == null)
            {
                throw new ArgumentNullException(
                    "company",
                    "Accounting module must be seeded first. Make sure the even CompaniesCreated is triggered and at least two companies are seeded.");
            }

            var employees = projectsDb.Set<Employee>().ToList();
            if (employees == null || employees.Count() < 2)
            {
                throw new ArgumentNullException(
                    "employees",
                    "Company module must be seeded first. Make sure the even EmployeesCreated is triggered and at least two employees are seeded.");
            }

            var ivan = employees.First();
            var project = Project.Create(
                "Apartment complex - Sofia",
                new DateTime(2017, 2, 25),
                new DateTime(2017, 10, 25),
                ivan.Id,
                customer.Id);

            projetcs = new List<Project>()
            {
                project
            };

            projectsDb.Set<Project>().AddRange(projetcs);
        }
    }
}
