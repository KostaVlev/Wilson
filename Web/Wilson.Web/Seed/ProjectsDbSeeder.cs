using System;
using System.Collections.Generic;
using System.Linq;
using Wilson.Companies.Data;
using Wilson.Projects.Core.Entities;
using Wilson.Projects.Data;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Wilson.Web.Seed
{
    public class ProjectsDbSeeder
    {
        private static IEnumerable<Project> projects;
        private static IEnumerable<Employee> employees;
        private static IEnumerable<Company> companies;

        /// <summary>
        /// Seeds the data for the Projects module.
        /// </summary>
        /// <param name="services">The service factory.</param>
        public static void Seed(IServiceScopeFactory services)
        {
            using (var scope = services.CreateScope())
            {
                var companyDb = scope.ServiceProvider.GetRequiredService<CompanyDbContext>();
                var projectsDb = scope.ServiceProvider.GetRequiredService<ProjectsDbContext>();

                // Keep the following methods in this exact order.
                SeedEmployees(projectsDb, companyDb, !projectsDb.Set<Employee>().Any(), out employees);
                SeedCompanies(projectsDb, companyDb, !projectsDb.Set<Company>().Any(), out companies);
                SeedProjects(projectsDb, !projectsDb.Set<Project>().Any(), out projects);
                

                projectsDb.SaveChanges();
            }            
        }

        private static void SeedProjects(
            ProjectsDbContext projectsDb,
            bool hasProjects, 
            out IEnumerable<Project> projetcs)
        {
            if (hasProjects)
            {
                projetcs = new List<Project>()
                {
                    Project.Create(
                        "Office building - Sofia", 
                        new DateTime(2017, 2, 25),
                        new DateTime(2017, 10, 25), 
                        employees.First(), 
                        companies.First()),

                    Project.Create(
                        "CCTV Museum - Burgas",
                        new DateTime(2016, 12, 20),
                        new DateTime(2017, 03, 25),
                        employees.First(),
                        companies.First())
                };

                projectsDb.Set<Project>().AddRange(projetcs);
            }
            else
            {
                projetcs = projectsDb.Set<Project>().ToList();
            }
        }

        private static void SeedEmployees(
            ProjectsDbContext projectsDb, 
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

                var dbEmployees = companyDb.Employees;
                employees = Mapper.Map<IEnumerable<Companies.Core.Entities.Employee>, IEnumerable<Employee>>(dbEmployees);
                
                projectsDb.Set<Employee>().AddRange(employees);
            }
            else
            {
                employees = projectsDb.Set<Employee>().ToList();
            }
        }

        private static void SeedCompanies(
            ProjectsDbContext projectsDb,
            CompanyDbContext companyDb,
            bool hasCompanies,
            out IEnumerable<Company> companies)
        {
            if (hasCompanies)
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Companies.Core.Entities.Company, Company>();
                });

                var dbCompanies = companyDb.Companies;
                companies = Mapper.Map<IEnumerable<Companies.Core.Entities.Company>, IEnumerable<Company>>(dbCompanies);

                projectsDb.Set<Company>().AddRange(companies);
            }
            else
            {
                companies = projectsDb.Set<Company>().ToList();
            }
        }
    }
}
