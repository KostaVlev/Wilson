using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Wilson.Accounting.Core.Entities;
using Wilson.Accounting.Data;
using Wilson.Web.Events;
using Wilson.Web.Events.Interfaces;
using Wilson.Accounting.Core.Entities.ValueObjects;

namespace Wilson.Web.Seed
{
    /// <summary>
    /// Contains the data which will be seeded for the Accounting module.
    /// </summary>
    public static class AccountingDbSeeder
    {
        private static IEnumerable<Company> companies;

        /// <summary>
        /// Seeds the data for the Accounting module.
        /// </summary>
        /// <param name="services">The service factory.</param>
        public static void Seed(IServiceScopeFactory services, IEventsFactory eventsFactory)
        {
            using (var scope = services.CreateScope())
            {
                var accountingDb = scope.ServiceProvider.GetRequiredService<AccountingDbContext>();

                SeedCompanies(accountingDb, out companies);

                accountingDb.SaveChanges();
                eventsFactory.Raise(new CompanyCreated(companies));
            }
        }

        private static void SeedCompanies(
            AccountingDbContext accountingDb,
            out IEnumerable<Company> companies)
        {            
            var address = Address.Create("Bulgaria", "1000", "Sofia", "Alexander I", "22A");
            var customerAddres = Address.Create("Bulgaria", "1000", "Sofia", "Ivan Rilski", "15");

            var company = Company.Create("Test Company Ltd", "160084783", "office@comapny.com", "0236659995", address, "BG160084783");
            var customer = Company.Create("Test Customer Ltd", "200325569", "office@customer.com", "0236659995", customerAddres, "BG200325569");

            companies = new List<Company>() { company, customer };

            accountingDb.Set<Company>().AddRange(companies);            
        }
    }
}
