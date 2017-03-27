using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Wilson.Companies.Data;
using Wilson.Accounting.Data;
using Wilson.Companies.Core.Entities;
using Wilson.Companies.Core.Enumerations;

namespace Wilson.Web.Database
{
    /// <summary>
    /// Contains the data which will be seeded for the Company module. Before seeding the Company module seed
    /// first Accounting Module.
    /// </summary>
    public static class CompaniesDbSeeder
    {
        private static IEnumerable<Address> addresses;
        private static IEnumerable<Company> companies;
        private static IEnumerable<Employee> employees;
        private static IEnumerable<ProjectLocation> projectLocations;
        private static IEnumerable<CompanyContract> companyContracts;
        private static IEnumerable<Project> projects;

        /// <summary>
        /// Seeds the data for the Company module.
        /// </summary>
        /// <param name="services">The service factory.</param>
        public static void Seed(IServiceScopeFactory services)
        {
            using (var scope = services.CreateScope())
            {
                var companyDb = scope.ServiceProvider.GetRequiredService<CompanyDbContext>();
                var accountingDb = scope.ServiceProvider.GetRequiredService<AccountingDbContext>();

                // Keep the following methods in this exact order.
                SeedAddresses(companyDb, accountingDb, companyDb.Addresses.Any(), out addresses);
                SeedCompanies(companyDb, accountingDb, companyDb.Companies.Any(), out companies);
                SeedEmployees(companyDb, accountingDb, companyDb.Employees.Any(), out employees);
                SeedProjectLocations(companyDb, companyDb.ProjectLocations.Any(), out projectLocations);
                SeedCompanyContracts(companyDb, companyDb.CompanyContracts.Any(), out companyContracts);
                SeedProjects(companyDb, accountingDb, companyDb.Projects.Any(), out projects);

                companyDb.SaveChanges();
            }
        }
        

        private static void SeedAddresses(CompanyDbContext db, AccountingDbContext accDb, bool hasAddresses, out IEnumerable<Address> addresses)
        {
            if (!hasAddresses)
            {
                var accAddresses = accDb.Addresses.ToList();
                addresses = PropertyCopy.CopyCollection<Address, Accounting.Core.Entities.Address>(accAddresses);

                db.Addresses.AddRange(addresses);
            }
            else
            {
                addresses = db.Addresses.ToList();
            }
        }

        private static void SeedCompanies(CompanyDbContext db, AccountingDbContext accDb, bool hasCompanies, out IEnumerable<Company> companies)
        {
            if (!hasCompanies)
            {
                var accCompanies = accDb.Companies.ToList();
                companies = PropertyCopy.CopyCollection<Company, Accounting.Core.Entities.Company>(accCompanies);
                foreach (var company in companies)
                {
                    company.ShippingAddressId = company.AddressId;
                    company.OfficeEmail = "office@mail.com";
                    company.OfficePhone = "0000000000";
                }

                db.Companies.AddRange(companies);
            }
            else
            {
                companies = db.Companies.ToList();
            }
        }

        private static void SeedEmployees(CompanyDbContext db, AccountingDbContext accDb, bool hasEmployees, out IEnumerable<Employee> employees)
        {
            if (!hasEmployees)
            {
                var accEmployees = accDb.Employees.ToList();
                employees = PropertyCopy.CopyCollection<Employee, Accounting.Core.Entities.Employee>(accEmployees).ToArray();

                var first = employees.Take(1).Last();
                var address = new Address()
                {
                    Country = "Bulgaria",
                    City = "Plovdiv",
                    PostCode = "4000",
                    Street = "Tzar Boris III",
                    StreetNumber = 25
                };

                first.EmployeePosition = EmployeePosition.Manager;
                first.Phone = "07588899665";
                first.PrivatePhone = "1111225333";
                first.Email = string.Format("{0}@mail.com", first.FirstName);
                first.AddressId = address.Id;

                var second = employees.Take(2).Last();
                second.EmployeePosition = EmployeePosition.OfficeSaff;
                second.Phone = "08789522568";
                second.PrivatePhone = "0789563214";
                second.Email = string.Format("{0}@gmail.com", second.FirstName);

                var third = employees.Take(3).Last();
                third.EmployeePosition = EmployeePosition.OfficeSaff;
                third.Phone = "0999856369";
                third.PrivatePhone = "0700258963";
                third.Email = string.Format("{0}@mail.com", third.LastName);

                db.Employees.AddRange(employees);
                db.Addresses.Add(address);
            }
            else
            {
                employees = db.Employees.ToList();
            }
        }

        private static void SeedProjectLocations(CompanyDbContext db, bool hasLocations, out IEnumerable<ProjectLocation> projectLocations)
        {
            if (!hasLocations)
            {
                projectLocations = new List<ProjectLocation>()
                {
                    new ProjectLocation()
                    {
                        Country = "Bulgaria",
                        City = "Burgas",
                        Street = "Anry Bardos",
                        StreetNumber = 25
                    },
                    new ProjectLocation()
                    {
                        Country = "Bulgaria",
                        City = "Sofia",
                        Street = "Mary Lorain",
                        Note = "Next to the Mall Grand Shopping"
                    },
                    new ProjectLocation()
                    {
                        Country = "Bulgaria",
                        City = "Plovdiv",
                        Street = "Ivan II",
                        Note = "After the tunnel."
                    },
                    new ProjectLocation()
                    {
                        Country = "Bulgaria",
                        City = "Sofia",
                        Street = "Boris Borisov"
                    },
                };

                db.ProjectLocations.AddRange(projectLocations);
            }
            else
            {
                projectLocations = db.ProjectLocations.ToList();
            }
        }

        private static void SeedCompanyContracts(CompanyDbContext db, bool hasContracts, out IEnumerable<CompanyContract> companyContracts)
        {
            if (!hasContracts)
            {
                companyContracts = new List<CompanyContract>()
                {
                    new CompanyContract()
                    {
                        Date = new DateTime(2016, 10, 5),
                        Revision = 1,
                        LastRevisedAt = new DateTime(2016, 10, 4),
                        IsApproved = true,
                        HtmlContent = "Not implemented",
                        CretedById = employees.Take(1).Last().Id,
                    },
                    new CompanyContract()
                    {
                        Date = new DateTime(2016, 11, 12),
                        IsApproved = true,
                        HtmlContent = "Not implemented",
                        CretedById = employees.Take(1).Last().Id
                    },
                    new CompanyContract()
                    {
                        Date = new DateTime(2016, 11, 25),
                        IsApproved = true,
                        HtmlContent = "Not implemented",
                        CretedById = employees.Take(2).Last().Id
                        
                    },
                    new CompanyContract()
                    {
                        Date = new DateTime(2016, 12, 8),
                        LastRevisedAt = new DateTime(2016, 12, 8),
                        IsApproved = false,
                        HtmlContent = "Not implemented",
                        CretedById = employees.Take(3).Last().Id
                    },
                };

                db.CompanyContracts.AddRange(companyContracts);
            }
            else
            {
                companyContracts = db.CompanyContracts.ToList();
            }
        }

        private static void SeedProjects(CompanyDbContext db, AccountingDbContext accDb, bool hasProjects, out IEnumerable<Project> projects)
        {
            if (!hasProjects)
            {
                var accProjects = accDb.Projects.ToList();
                projects = PropertyCopy.CopyCollection<Project, Accounting.Core.Entities.Project>(accProjects).ToArray();

                var first = projects.Take(1).Last();
                var firstContract = companyContracts.Where(x => x.IsApproved).Take(1).Last();
                first.ContractId = firstContract.Id;
                first.LocationId = projectLocations.Take(1).Last().Id;

                // Update the relation CompanyContract -> Project!!!
                firstContract.ProjectId = first.Id;         
                
                var second = projects.Take(2).Last();
                var secondContract = companyContracts.Where(x => x.IsApproved).Take(2).Last();
                second.ContractId = secondContract.Id;
                second.LocationId = projectLocations.Take(2).Last().Id;

                // Update the relation CompanyContract -> Project!!!
                secondContract.ProjectId = second.Id;

                var third = projects.Take(3).Last();
                var thirdContract = companyContracts.Where(x => x.IsApproved).Take(3).Last();
                third.ContractId = thirdContract.Id;
                third.LocationId = projectLocations.Take(3).Last().Id;

                // Update the relation CompanyContract -> Project!!!
                thirdContract.ProjectId = third.Id;

                var forth = projects.Take(4).Last();
                var forthContract = companyContracts.Where(x => !x.IsApproved).Take(1).Last();
                forth.ContractId = forthContract.Id;
                forth.LocationId = projectLocations.Take(4).Last().Id;

                // Update the relation CompanyContract -> Project!!!
                forthContract.ProjectId = forth.Id;

                db.Projects.AddRange(projects);
            }
            else
            {
                projects = db.Projects.ToList();
            }
        }
    }
}
