using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Wilson.Accounting.Core.Entities;
using Wilson.Accounting.Core.Enumerations;
using Wilson.Accounting.Data;

namespace Wilson.Web.Database
{
    /// <summary>
    /// Contains the data which will be seeded for the Accounting module.
    /// </summary>
    public static class AccountingDbSeeder
    {
        private static IEnumerable<Address> addresses;
        private static IEnumerable<Company> companies;
        private static IEnumerable<Employee> employees;
        private static IEnumerable<Project> projects;
        private static IEnumerable<Invoice> invoices;

        /// <summary>
        /// Seeds the data for the Accounting module.
        /// </summary>
        /// <param name="services">The service factory.</param>
        public static void Seed(IServiceScopeFactory services)
        {
            using (var scope = services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AccountingDbContext>();
                SeedAddresses(db, !db.Addresses.Any(), out addresses);
                SeedCompanies(db, !db.Companies.Any(), out companies);
                SeedEmployees(db, !db.Employees.Any(), out employees);
                SeedProjects(db, !db.Projects.Any(), out projects);
                SeedInvoices(db, !db.Invoices.Any(), out invoices);

                db.SaveChanges();
            }
        }

        private static void SeedAddresses(AccountingDbContext db, bool hasAddresses, out IEnumerable<Address> addresses)
        {
            if (hasAddresses)
            {
                addresses = new List<Address>()
                {
                    new Address()
                    {
                        Country = "Bulgaria",
                        Town = "Plovdiv",
                        Street = "Porto Lagos",
                        StreetNumber = 43,
                        Floor = 4,
                        PostCode = "4700",
                        UnitNumber = "1A",
                        Note = "The building next to the Post Office."
                    },
                    new Address()
                    {
                        Country = "Bulgaria",
                        Town = "Sofia",
                        Street = "Vitoshka",
                        StreetNumber = 12,
                        Floor = 1,
                        PostCode = "1000",
                        UnitNumber = "2",
                        Note = "Ring the bell two times."
                    },
                    new Address()
                    {
                        Country = "USA",
                        Town = "Miami",
                        Street = "NE 25th Rd",
                        StreetNumber = 3215,
                        Floor = 0,
                        PostCode = "25111",
                        UnitNumber = "8",
                        Note = "Entrance from the swimming pool."
                    },
                    new Address()
                    {
                        Country = "Bulgaria",
                        Town = "Plovdiv",
                        Street = "Maria Luisa",
                        StreetNumber = 252,
                        Floor = 0,
                        PostCode = "4000",
                        UnitNumber = "0",
                        Note = "In the Commercial zone North."
                    },
                };

                db.Addresses.AddRange(addresses);
            }
            else
            {
                addresses = db.Addresses.ToList();
            }
        }

        private static void SeedCompanies(AccountingDbContext db, bool hasComapanies, out IEnumerable<Company> companies)
        {
            if (hasComapanies)
            {
                companies = new List<Company>()
                {
                    new Company()
                    {
                        Name = "MyCompany",
                        IndetificationNumber = "16084783",
                        VatNumber = "BG160084783",
                        HasVatRegistration = true,
                        AddressId = addresses.Take(1).Last().Id
                    },
                    new Company()
                    {
                        Name = "Prefect Building LTD",
                        IndetificationNumber = "256369520",
                        VatNumber = "BG256369520",
                        HasVatRegistration = true,
                        AddressId = addresses.Take(2).Last().Id
                    },
                    new Company()
                    {
                        Name = "Extreme Electrics LTD",
                        IndetificationNumber = "369654852",
                        VatNumber = "US369654852",
                        HasVatRegistration = true,
                        AddressId = addresses.Take(3).Last().Id
                    },
                    new Company()
                    {
                        Name = "Top Supplies LTD",
                        IndetificationNumber = "589896320",
                        VatNumber = "BG589896320",
                        HasVatRegistration = true,
                        AddressId = addresses.Take(4).Last().Id
                    },
                };

                db.Companies.AddRange(companies);
            }
            else
            {
                companies = db.Companies.ToList();
            }
        }

        private static void SeedEmployees(AccountingDbContext db, bool hasEmployees, out IEnumerable<Employee> employees)
        {
            if (hasEmployees)
            {
                employees = new List<Employee>()
                {
                    new Employee()
                    {
                        FirstName = "Kostadin",
                        LastName = "Vasilev",
                        CompanyId = companies.Take(1).Last().Id
                    },
                    new Employee()
                    {
                        FirstName = "John",
                        LastName = "Smith",
                        CompanyId = companies.Take(2).Last().Id
                    },
                    new Employee()
                    {
                        FirstName = "Barbara",
                        LastName = "Johnson",
                        CompanyId = companies.Take(3).Last().Id
                    },
                };

                db.Employees.AddRange(employees);
            }
            else
            {
                employees = db.Employees.ToList();
            }
        }

        private static void SeedProjects(AccountingDbContext db, bool hasProjects, out IEnumerable<Project> projects)
        {
            if (hasProjects)
            {
                projects = new List<Project>()
                {
                    new Project()
                    {
                        Name = "Office Building - Class A",
                        CustomerId = companies.Take(2).Last().Id
                    },
                    new Project()
                    {
                        Name = "Apartment Complex - River View",
                        CustomerId = companies.Take(2).Last().Id
                    },
                    new Project()
                    {
                        Name = "Head office renovation",
                        CustomerId = companies.Take(2).Last().Id
                    },
                    new Project()
                    {
                        Name = "Warehouse Lightning protection system and CCTV",
                        CustomerId = companies.Take(3).Last().Id
                    },
                };

                db.Projects.AddRange(projects);
            }
            else
            {
                projects = db.Projects.ToList();
            }
        }

        private static void SeedInvoices(AccountingDbContext db, bool hasInvoices, out IEnumerable<Invoice> invoices)
        {
            if (hasInvoices)
            {
                var myCompany = companies.Take(1).Last().Id;
                invoices = new List<Invoice>()
                {
                    new Invoice()
                    {
                        Number = "0000000001",
                        InvoiceVariant = InvoiceVariant.Invoice,
                        InvoiceType = InvoiceType.Sales,
                        InvoicePaymentType = InvoicePaymentType.BankTransfer,
                        IssueDate = new DateTime(2017, 2, 14),
                        DateOfPayment = new DateTime(2017, 3, 12),
                        DaysOfDelayedPayment = 20,
                        IsPayed = true,
                        PayedAmount = 223.25M,
                        SellerId = myCompany,
                        BuyerId = companies.Take(2).Last().Id,
                        SubTotal = 223.25M,
                        Vat = 20,
                        VatAmount = 44.65M,
                        Total = 267.90M 
                    },
                    new Invoice()
                    {
                        Number = "0000000002",
                        InvoiceVariant = InvoiceVariant.Invoice,
                        InvoiceType = InvoiceType.Sales,
                        InvoicePaymentType = InvoicePaymentType.BankTransfer,
                        IssueDate = new DateTime(2017, 3, 9),
                        DaysOfDelayedPayment = 20,
                        IsPayed = false,
                        PayedAmount = 1520M,
                        SellerId = myCompany,
                        BuyerId = companies.Take(2).Last().Id,
                        SubTotal = 3250.36M,
                        Vat = 20,
                        VatAmount = 650.07M,
                        Total = 3900.432M
                    },
                    new Invoice()
                    {
                        Number = "0000000003",
                        InvoiceVariant = InvoiceVariant.Invoice,
                        InvoiceType = InvoiceType.Sales,
                        InvoicePaymentType = InvoicePaymentType.BankTransfer,
                        IssueDate = new DateTime(2017, 4, 26),
                        DaysOfDelayedPayment = 20,
                        IsPayed = false,
                        PayedAmount = 1520M,
                        SellerId = myCompany,
                        BuyerId = companies.Take(3).Last().Id,
                        SubTotal = 1000M,
                        Vat = 20,
                        VatAmount = 200M,
                        Total = 1200M
                    },
                    new Invoice()
                    {
                        Number = "0200002589",
                        InvoiceVariant = InvoiceVariant.Invoice,
                        InvoiceType = InvoiceType.Purchase,
                        InvoicePaymentType = InvoicePaymentType.BankTransfer,
                        IssueDate = new DateTime(2017, 4, 26),
                        DaysOfDelayedPayment = 20,
                        IsPayed = false,
                        SellerId = companies.Take(4).Last().Id,
                        BuyerId = myCompany,
                        SubTotal = 2000M,
                        Vat = 20,
                        VatAmount = 400M,
                        Total = 2400M
                    },
                    new Invoice()
                    {
                        Number = "0200002985",
                        InvoiceVariant = InvoiceVariant.Invoice,
                        InvoiceType = InvoiceType.Purchase,
                        InvoicePaymentType = InvoicePaymentType.Cash,
                        IssueDate = new DateTime(2017, 4, 26),
                        DateOfPayment = new DateTime(2017, 4, 26),
                        DaysOfDelayedPayment = 0,
                        IsPayed = true,
                        SellerId = companies.Take(4).Last().Id,
                        BuyerId = myCompany,
                        SubTotal = 2500M,
                        Vat = 20,
                        VatAmount = 500M,
                        Total = 3000M
                    },
                };

                db.Invoices.AddRange(invoices);
            }
            else
            {
                invoices = db.Invoices.ToList();
            }
        }
    }
}
