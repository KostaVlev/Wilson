using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Wilson.Accounting.Core.Entities;
using Wilson.Accounting.Core.Enumerations;
using Wilson.Accounting.Data;
using Wilson.Companies.Data;
using Wilson.Projects.Data;
using AutoMapper;

namespace Wilson.Web.Seed
{
    /// <summary>
    /// Contains the data which will be seeded for the Accounting module.
    /// </summary>
    public static class AccountingDbSeeder
    {
        private static IEnumerable<Company> companies;
        private static IEnumerable<Employee> employees;
        private static IEnumerable<Project> projects;
        private static IEnumerable<Storehouse> storehouses;

        /// <summary>
        /// Seeds the data for the Accounting module.
        /// </summary>
        /// <param name="services">The service factory.</param>
        public static void Seed(IServiceScopeFactory services)
        {
            using (var scope = services.CreateScope())
            {
                var companyDb = scope.ServiceProvider.GetRequiredService<CompanyDbContext>();
                var accountingDb = scope.ServiceProvider.GetRequiredService<AccountingDbContext>();
                var projectsDb = scope.ServiceProvider.GetRequiredService<ProjectsDbContext>();

                // Keep the following methods in this exact order.
                SeedEmployees(accountingDb, companyDb, accountingDb.Set<Employee>().Any(), out employees);
                SeedCompanies(accountingDb, companyDb, accountingDb.Set<Company>().Any(), out companies);
                SeedProjects(accountingDb, projectsDb, accountingDb.Set<Project>().Any(), out projects);
                SeedStorehouses(accountingDb, accountingDb.Set<Storehouse>().Any(), out storehouses);

                accountingDb.SaveChanges();
            }
        }

        private static void SeedCompanies(
            AccountingDbContext accountingDb,
            CompanyDbContext companyDb,
            bool hasCompanies,
            out IEnumerable<Company> companies)
        {
            if (!hasCompanies)
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Companies.Core.Entities.Company, Company>()
                       .ForMember(x => x.Employees, opt => opt.Ignore())
                       .ForMember(x => x.SaleInvoices, opt => opt.Ignore())
                       .ForMember(x => x.Address, opt => opt.Ignore())
                       .ForMember(x => x.BuyInvoices, opt => opt.Ignore());
                });

                var dbCompanies = companyDb.Companies;
                companies = Mapper.Map<IEnumerable<Companies.Core.Entities.Company>, IEnumerable<Company>>(dbCompanies);

                accountingDb.Set<Company>().AddRange(companies);
            }
            else
            {
                companies = accountingDb.Set<Company>().ToList();
            }
        }

        private static void SeedProjects(
            AccountingDbContext accDb,
            ProjectsDbContext projectsDb,
            bool hasProjects, 
            out IEnumerable<Project> projetcs)
        {
            if (!hasProjects)
            {
                var dbProjects = projectsDb.Projects.ToList();
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Projects.Core.Entities.Project, Project>().ForMember(x => x.Customer, opt => opt.Ignore());
                });

                projetcs = Mapper.Map<IEnumerable<Projects.Core.Entities.Project>, IEnumerable<Project>>(dbProjects);

                accDb.Set<Project>().AddRange(projetcs);
            }
            else
            {
                projetcs = accDb.Set<Project>().ToList();
            }
        }

        private static void SeedStorehouses(
            AccountingDbContext accDb,
            bool hasStorehouses,
            out IEnumerable<Storehouse> storehouses)
        {
            if (!hasStorehouses)
            {
                var project = projects.FirstOrDefault();
                var otherProject = projects.Skip(1).FirstOrDefault();

                storehouses = new List<Storehouse>()
                {
                    project.CreateStorehouse(project.Name),
                    otherProject.CreateStorehouse(otherProject.Name)
                };

                accDb.Set<Storehouse>().AddRange(storehouses);
            }
            else
            {
                storehouses = accDb.Set<Storehouse>().ToList();
            }
        }

        private static void SeedEmployees(
            AccountingDbContext accDb,
            CompanyDbContext companyDb,
            bool hasEmplyees,
            out IEnumerable<Employee> employees)
        {
            if (!hasEmplyees)
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<Companies.Core.Entities.Employee, Employee>().ForMember(x => x.Company, opt => opt.Ignore());
                });

                var dbEmployees = companyDb.Employees;
                employees = Mapper.Map<IEnumerable<Companies.Core.Entities.Employee>, IEnumerable<Employee>>(dbEmployees);

                accDb.Set<Employee>().AddRange(employees);
            }
            else
            {
                employees = accDb.Set<Employee>().ToList();
            }
        }
    }
}
