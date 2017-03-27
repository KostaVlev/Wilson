using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Wilson.Companies.Data;
using Wilson.Accounting.Data;
using Wilson.Companies.Core.Entities;

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
                //SeedEmployees(db, db.Employees.Any(), out employees);
                //SeedProjects(db, db.Projects.Any(), out projects);
                //SeedStorehouses(db, db.Storehouses.Any(), out storehouses);
                //SeedInvoices(db, db.Invoices.Any(), out invoices);
                //SeedItems(db, db.Items.Any(), out items);
                //SeedItemPrices(db, db.Prices.Any(x => !string.IsNullOrEmpty(x.ItemId)), out itemPrices);
                //SeedInvoiceItems(db, db.InvoiceItems.Any(), out invoiceItems);
                //SeedPaymets(db, db.Payments.Any(), out payments);
                //SeedStorehouseItems(db, db.StorehouseItems.Any(), out storehouseItems);
                //SeedBills(db, db.Bills.Any(), out bills);

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
    }
}
