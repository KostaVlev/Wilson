using System;
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

        public DateTime DateOccurred { get; private set; }

        public Company Company { get; set; }
    }
}
