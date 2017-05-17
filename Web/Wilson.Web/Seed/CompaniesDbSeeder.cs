using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Wilson.Accounting.Data;
using Wilson.Companies.Core.Entities;
using Wilson.Companies.Core.Enumerations;
using Wilson.Companies.Data;
using Wilson.Web.Events;
using Wilson.Web.Events.Interfaces;

namespace Wilson.Web.Seed
{
    /// <summary>
    /// Contains the data which will be seeded for the Company module. Before seeding the Company module seed
    /// first Accounting Module.
    /// </summary>
    public static class CompaniesDbSeeder
    {
        private static IEnumerable<Employee> employees;
        private static IEnumerable<Inquiry> inquiries;
        private static IEnumerable<Offer> offers;

        /// <summary>
        /// Seeds the data for the Company module.
        /// </summary>
        /// <param name="services">The service factory.</param>
        public static void Seed(IServiceScopeFactory services, IEventsFactory eventsFactory)
        {
            using (var scope = services.CreateScope())
            {
                var companyDb = scope.ServiceProvider.GetRequiredService<CompanyDbContext>();
                var accountingDb = scope.ServiceProvider.GetRequiredService<AccountingDbContext>();

                // Keep the following methods in this exact order.
                SeedEmployees(companyDb, out employees);
                SeedInquiries(companyDb, out inquiries);
                SeedOffers(companyDb, out offers);

                companyDb.SaveChanges();
                eventsFactory.Raise(new EmployeeCreated(employees));
            }
        }

        private static void SeedEmployees(CompanyDbContext companyDb, out IEnumerable<Employee> employees)
        {
            var company = companyDb.Companies.FirstOrDefault();
            if (company == null)
            {
                throw new ArgumentNullException(
                    "company",
                    "Accounting module must be seeded first. Make sure the even CompaniesCreated is triggered.");
            }

            var ivan = Employee.Create(
                "Ivan",
                "Petrov",
                "0125669874",
                "0135698552",
                "j.smith@mail.com",
                EmployeePosition.OfficeSaff,
                Address.Create("Bulgaria", "1000", "Sofia", "Vasil Levski", "11"),
                company.Id);

            var maria = Employee.Create(
                "Maria",
                "Popinska",
                "0526668822",
                "0329555645",
                "m.popinska@mail.com",
                EmployeePosition.OfficeSaff,
                Address.Create("Bulgaria", "1000", "Sofia", "Ivan Vazov", "2B"),
                company.Id);

            employees = new List<Employee> { ivan, maria };
            companyDb.Employees.AddRange(employees);
        }

        private static void SeedInquiries(CompanyDbContext companyDb, out IEnumerable<Inquiry> inquiries)
        {
            var company = companyDb.Companies.Skip(1).FirstOrDefault();
            if (company == null)
            {
                throw new ArgumentNullException(
                    "company",
                    "Accounting module must be seeded first. Make sure the even CompaniesCreated is triggered and at least two companies are seeded.");
            }

            if (employees == null || employees.Count() < 2)
            {
                throw new ArgumentNullException(
                    "employees",
                    "Run SeedCompanyEmployees() first and make sure at least two Employees are seeded.");
            }

            var ivan = employees.First();
            var maria = employees.Skip(1).First();
            var inquiry = Inquiry.Create("Offer for part Electrical - Apartment Complex", ivan.Id, company.Id);
            inquiry.AddAssignee(maria.Id);
            inquiry.AddInfoRequest("We need the electrical plans to make offer.", maria.Id);

            inquiries = new List<Inquiry>()
                {
                    inquiry
                };

            companyDb.Inquiries.AddRange(inquiries);
        }

        private static void SeedOffers(CompanyDbContext companyDb, out IEnumerable<Offer> offers)
        {
            if (employees == null || employees.Count() < 2)
            {
                throw new ArgumentNullException(
                    "employees",
                    "Run SeedCompanyEmployees() first and make sure at least two Employees are seeded.");
            }

            if (inquiries == null || inquiries.Count() == 0)
            {
                throw new ArgumentNullException(
                    "offers",
                    "Run SeedInquiries() first and make sure at least one Offer is seeded.");
            }

            var maria = employees.Skip(1).First();
            var inquiry = inquiries.First();
            var offer = Offer.Create("No html yet.", inquiry.Id, maria.Id);
            offer.Send(maria.Id);

            offers = new List<Offer>()
                {
                    offer
                };

            companyDb.Set<Offer>().AddRange(offers);
        }
    }
}
