using System;
using System.Collections.Generic;
using Wilson.Accounting.Core.Entities;
using Wilson.Web.Events.Interfaces;

namespace Wilson.Web.Events
{
    public class CompanyCreated : IDomainEvent
    {
        public CompanyCreated(Company company)
        {
            this.Company = company;
            this.DateOccurred = DateTime.Now;
        }

        public CompanyCreated(IEnumerable<Company> companies)
        {
            this.Companies = companies;
            this.DateOccurred = DateTime.Now;
        }

        public DateTime DateOccurred { get; private set; }

        public Company Company { get; set; }

        public IEnumerable<Company> Companies { get; set; }
    }
}
