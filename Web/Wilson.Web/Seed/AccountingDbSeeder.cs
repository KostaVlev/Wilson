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
        private static IEnumerable<Address> addresses;
        private static IEnumerable<Company> companies;
        private static IEnumerable<Employee> employees;
        private static IEnumerable<Project> projects;
        private static IEnumerable<Storehouse> storehouses;
        private static IEnumerable<Invoice> invoices;
        private static IEnumerable<Item> items;
        private static IEnumerable<InvoiceItem> invoiceItems;
        private static IEnumerable<Payment> payments;
        private static IEnumerable<StorehouseItem> storehouseItems;
        private static IEnumerable<Bill> bills;

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
                SeedCompanies(accountingDb, companyDb, !accountingDb.Set<Company>().Any(), out companies);
                SeedProjects(accountingDb, projectsDb, !accountingDb.Set<Project>().Any(), out projects);

                //SeedStorehouses(db, !db.Storehouses.Any(), out storehouses);
                //SeedInvoices(db, !db.Invoices.Any(), out invoices);
                //SeedItems(db, !db.Items.Any(), out items);
                //SeedItemPrices(db, !db.Prices.Any(x => !string.IsNullOrEmpty(x.ItemId)), out itemPrices);
                //SeedInvoiceItems(db, !db.InvoiceItems.Any(), out invoiceItems);
                //SeedPaymets(db, !db.Payments.Any(), out payments);
                //SeedStorehouseItems(db, !db.StorehouseItems.Any(), out storehouseItems);
                //SeedBills(db, !db.Bills.Any(), out bills);

                accountingDb.SaveChanges();
            }
        }

        private static void SeedCompanies(
            AccountingDbContext accountingDb,
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

                accountingDb.Set<Company>().AddRange(companies);
            }
            else
            {
                companies = accountingDb.Set<Company>().ToList();
            }
        }

        private static void SeedProjects(AccountingDbContext accDb, ProjectsDbContext projectsDb, bool hasProjects, out IEnumerable<Project> projetcs)
        {
            if (hasProjects)
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

        //private static void SeedStorehouses(AccountingDbContext db, bool hasPayments, out IEnumerable<Storehouse> storehouses)
        //{
        //    if (hasprojects)
        //    {
        //        storehouses = new List<Storehouse>()
        //        {
        //            new Storehouse()
        //            {
        //                Name = "Base Storehouse"
        //            },
        //            new Storehouse()
        //            {
        //                Name = "Offices",
        //                ProjectId = projects.Take(1).Last().Id
        //            },
        //            new Storehouse()
        //            {
        //                Name = "River View",
        //                ProjectId = projects.Take(2).Last().Id
        //            },
        //            new Storehouse()
        //            {
        //                Name = "Head office",
        //                ProjectId = projects.Take(3).Last().Id
        //            },
        //            new Storehouse()
        //            {
        //                Name = "Warehouse",
        //                ProjectId = projects.Take(4).Last().Id
        //            },
        //        };

        //        db.Storehouses.AddRange(storehouses);
        //    }
        //    else
        //    {
        //        storehouses = db.Storehouses.ToList();
        //    }
        //}

        //private static void SeedInvoices(AccountingDbContext db, bool hasInvoices, out IEnumerable<Invoice> invoices)
        //{
        //    if (hasInvoices)
        //    {
        //        var myCompany = companies.Take(1).Last().Id;
        //        invoices = new List<Invoice>()
        //        {
        //            new Invoice()
        //            {
        //                Number = "0000000001",
        //                InvoiceVariant = InvoiceVariant.Invoice,
        //                InvoiceType = InvoiceType.Sales,
        //                InvoicePaymentType = InvoicePaymentType.BankTransfer,
        //                IssueDate = new DateTime(2017, 2, 14),
        //                DateOfPayment = new DateTime(2017, 3, 12),
        //                DaysOfDelayedPayment = 20,
        //                IsPayed = true,
        //                PayedAmount = 12707.47M,
        //                SellerId = myCompany,
        //                BuyerId = companies.Take(2).Last().Id,
        //                SubTotal = 10589.56M,
        //                Vat = 20,
        //                VatAmount = 2117.91M,
        //                Total = 12707.47M
        //            },
        //            new Invoice()
        //            {
        //                Number = "0000000002",
        //                InvoiceVariant = InvoiceVariant.Invoice,
        //                InvoiceType = InvoiceType.Sales,
        //                InvoicePaymentType = InvoicePaymentType.BankTransfer,
        //                IssueDate = new DateTime(2017, 3, 9),
        //                DaysOfDelayedPayment = 20,
        //                IsPayed = false,
        //                PayedAmount = 1520M,
        //                SellerId = myCompany,
        //                BuyerId = companies.Take(2).Last().Id,
        //                SubTotal = 5988M,
        //                Vat = 20,
        //                VatAmount = 1197.60M,
        //                Total = 7185.60M
        //            },
        //            new Invoice()
        //            {
        //                Number = "0000000003",
        //                InvoiceVariant = InvoiceVariant.Invoice,
        //                InvoiceType = InvoiceType.Sales,
        //                InvoicePaymentType = InvoicePaymentType.BankTransfer,
        //                IssueDate = new DateTime(2017, 4, 26),
        //                DaysOfDelayedPayment = 20,
        //                IsPayed = false,
        //                PayedAmount = 1800M,
        //                SellerId = myCompany,
        //                BuyerId = companies.Take(3).Last().Id,
        //                SubTotal = 4000M,
        //                Vat = 20,
        //                VatAmount = 800M,
        //                Total = 4800M
        //            },
        //            new Invoice()
        //            {
        //                Number = "0200002589",
        //                InvoiceVariant = InvoiceVariant.Invoice,
        //                InvoiceType = InvoiceType.Purchase,
        //                InvoicePaymentType = InvoicePaymentType.BankTransfer,
        //                IssueDate = new DateTime(2017, 4, 26),
        //                DaysOfDelayedPayment = 20,
        //                IsPayed = false,
        //                SellerId = companies.Take(4).Last().Id,
        //                BuyerId = myCompany,
        //                SubTotal = 60300M,
        //                Vat = 20,
        //                VatAmount = 12060M,
        //                Total = 72360M
        //            },
        //            new Invoice()
        //            {
        //                Number = "0200002985",
        //                InvoiceVariant = InvoiceVariant.Invoice,
        //                InvoiceType = InvoiceType.Purchase,
        //                InvoicePaymentType = InvoicePaymentType.Cash,
        //                IssueDate = new DateTime(2017, 4, 26),
        //                DateOfPayment = new DateTime(2017, 4, 26),
        //                DaysOfDelayedPayment = 0,
        //                IsPayed = true,
        //                SellerId = companies.Take(4).Last().Id,
        //                BuyerId = myCompany,
        //                SubTotal = 34250M,
        //                Vat = 20,
        //                VatAmount = 6850M,
        //                Total = 41100M
        //            },
        //        };

        //        db.Invoices.AddRange(invoices);
        //    }
        //    else
        //    {
        //        invoices = db.Invoices.ToList();
        //    }
        //}

        //private static void SeedItems(AccountingDbContext db, bool hasItems, out IEnumerable<Item> items)
        //{
        //    if (hasItems)
        //    {
        //        items = new List<Item>()
        //        {
        //            new Item()
        //            {
        //                Name = "Cable NYY 3x1.5",
        //                Мeasure = Мeasure.Meter,
        //                Quantity = 5000,
        //            },
        //            new Item()
        //            {
        //                Name = "Cable NYY 3x2.5",
        //                Мeasure = Мeasure.Meter,
        //                Quantity = 5000,
        //            },
        //            new Item()
        //            {
        //                Name = "Cable NYY 3x4",
        //                Мeasure = Мeasure.Meter,
        //                Quantity = 5000,
        //            },
        //            new Item()
        //            {
        //                Name = "Cable NYY 5x10",
        //                Мeasure = Мeasure.Meter,
        //                Quantity = 5000,
        //            },
        //            new Item()
        //            {
        //                Name = "Bill #1 25.01.2017 Project: Office Building - Class A",
        //                Мeasure = Мeasure.Pcs,
        //                Quantity = 1,
        //            },
        //            new Item()
        //            {
        //                Name = "Bill #1 14.04.2017 Project: Apartment Complex - River View",
        //                Мeasure = Мeasure.Pcs,
        //                Quantity = 1,
        //            },
        //            new Item()
        //            {
        //                Name = "Bill #2 14.04.2017 Project: Apartment Complex - River View",
        //                Мeasure = Мeasure.Pcs,
        //                Quantity = 1,
        //            },
        //        };

        //        db.Items.AddRange(items);
        //    }
        //    else
        //    {
        //        items = db.Items.ToList();
        //    }
        //}

        //private static void SeedInvoiceItems(AccountingDbContext db, bool hasInvoiceItems, out IEnumerable<InvoiceItem> invoiceItems)
        //{
        //    if (hasInvoiceItems)
        //    {
        //        invoiceItems = new List<InvoiceItem>()
        //        {
        //            new InvoiceItem()
        //            {
        //                Quantity = 1,
        //                InvoiceId = invoices.Take(1).Last().Id,
        //                ItemId = items.Take(5).Last().Id,
        //                PriceId = itemPrices.Where(x => x.ItemId == items.Take(5).Last().Id).First().Id
        //            },
        //            new InvoiceItem()
        //            {
        //                Quantity = 1,
        //                InvoiceId = invoices.Take(2).Last().Id,
        //                ItemId = items.Take(6).Last().Id,
        //                PriceId = itemPrices.Where(x => x.ItemId == items.Take(6).Last().Id).First().Id
        //            },
        //            new InvoiceItem()
        //            {
        //                Quantity = 1,
        //                InvoiceId = invoices.Take(3).Last().Id,
        //                ItemId = items.Take(7).Last().Id,
        //                PriceId = itemPrices.Where(x => x.ItemId == items.Take(7).Last().Id).First().Id
        //            },
        //            new InvoiceItem()
        //            {
        //                Quantity = 5000,
        //                InvoiceId = invoices.Take(4).Last().Id,
        //                ItemId = items.Take(1).Last().Id,
        //                PriceId = itemPrices.Where(x => x.ItemId == items.Take(1).Last().Id).First().Id
        //            },
        //            new InvoiceItem()
        //            {
        //                Quantity = 5000,
        //                InvoiceId = invoices.Take(4).Last().Id,
        //                ItemId = items.Take(2).Last().Id,
        //                PriceId = itemPrices.Where(x => x.ItemId == items.Take(2).Last().Id).First().Id
        //            },
        //            new InvoiceItem()
        //            {
        //                Quantity = 5000,
        //                InvoiceId = invoices.Take(4).Last().Id,
        //                ItemId = items.Take(3).Last().Id,
        //                PriceId = itemPrices.Where(x => x.ItemId == items.Take(3).Last().Id).First().Id
        //            },
        //            new InvoiceItem()
        //            {
        //                Quantity = 5000,
        //                InvoiceId = invoices.Take(5).Last().Id,
        //                ItemId = items.Take(4).Last().Id,
        //                PriceId = itemPrices.Where(x => x.ItemId == items.Take(4).Last().Id).First().Id
        //            },
        //        };

        //        db.InvoiceItems.AddRange(invoiceItems);
        //    }
        //    else
        //    {
        //        invoiceItems = db.InvoiceItems.ToList();
        //    }
        //}

        //private static void SeedPaymets(AccountingDbContext db, bool hasPayments, out IEnumerable<Payment> payments)
        //{
        //    if (hasPayments)
        //    {
        //        var price = new Price() { Amount = 12707.47M };
        //        payments = new List<Payment>()
        //        {
        //            new Payment()
        //            {
        //                Date = new DateTime(2017, 3, 12),
        //                InvoiceId = invoices.Take(1).Last().Id,
        //                PriceId = price.Id
        //            },
        //        };

        //        price.PaymentId = payments.Take(1).Last().Id;
        //        db.Prices.Add(price);
        //        db.Payments.AddRange(payments);
        //    }
        //    else
        //    {
        //        payments = db.Payments.ToList();
        //    }
        //}

        //private static void SeedStorehouseItems(AccountingDbContext db, bool hasItems, out IEnumerable<StorehouseItem> storehouseItems)
        //{
        //    if (hasItems)
        //    {
        //        storehouseItems = new List<StorehouseItem>()
        //        {
        //            new StorehouseItem()
        //            {
        //                StorehouseId = storehouses.Take(2).Last().Id,
        //                InvoiceItemId = invoiceItems.Take(4).Last().Id,
        //            },
        //            new StorehouseItem()
        //            {
        //                StorehouseId = storehouses.Take(3).Last().Id,
        //                InvoiceItemId = invoiceItems.Take(5).Last().Id,
        //            },
        //            new StorehouseItem()
        //            {
        //                StorehouseId = storehouses.Take(4).Last().Id,
        //                InvoiceItemId = invoiceItems.Take(6).Last().Id,
        //            },
        //            new StorehouseItem()
        //            {
        //                StorehouseId = storehouses.Take(5).Last().Id,
        //                InvoiceItemId = invoiceItems.Take(7).Last().Id,
        //            },
        //        };

        //        db.StorehouseItems.AddRange(storehouseItems);
        //    }
        //    else
        //    {
        //        storehouseItems = db.StorehouseItems.ToList();
        //    }
        //}

        //private static void SeedBills(AccountingDbContext db, bool hasBills, out IEnumerable<Bill> bills)
        //{
        //    if (hasBills)
        //    {
        //        bills = new List<Bill>()
        //        {
        //            new Bill()
        //            {
        //                Date = new DateTime(2017, 2, 14),
        //                Amount = 10589.56M,
        //                HtmlContent = "Not implemented",
        //                InvoiceId = invoices.Where(x => x.SubTotal == 10589.56M).First().Id,
        //                ProjectId = projects.Where(x => x.Name.Equals("Office Building - Class A")).First().Id,
        //            },
        //            new Bill()
        //            {
        //                Date = new DateTime(2017, 3, 9),
        //                Amount = 5988M,
        //                HtmlContent = "Not implemented",
        //                InvoiceId = invoices.Where(x => x.SubTotal == 5988M).First().Id,
        //                ProjectId = projects.Where(x => x.Name.Equals("Apartment Complex - River View")).First().Id,
        //            },
        //            new Bill()
        //            {
        //                Date = new DateTime(2017, 4, 26),
        //                Amount = 4000M,
        //                HtmlContent = "Not implemented",
        //                InvoiceId = invoices.Where(x => x.SubTotal == 4000M).First().Id,
        //                ProjectId = projects.Where(x => x.Name.Equals("Apartment Complex - River View")).First().Id,
        //            },
        //            new Bill()
        //            {
        //                Date = new DateTime(2017, 4, 18),
        //                Amount = 8500M,
        //                HtmlContent = "Not implemented",
        //                ProjectId = projects.Where(x => x.Name.Equals("Head office renovation")).First().Id,
        //            },
        //        };

        //        db.Bills.AddRange(bills);
        //    }
        //    else
        //    {
        //        bills = db.Bills.ToList();
        //    }
        //}
    }
}
