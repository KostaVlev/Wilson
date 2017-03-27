using System.Collections.Generic;

namespace Wilson.Accounting.Core.Entities
{
    public class Company : Entity
    {
        public string Name { get; set; }

        public string RegistrationNumber { get; set; }

        public string VatNumber { get; set; }

        public bool HasVatRegistration { get; set; }

        public string AddressId { get; set; }

        public Address Address { get; set; }

        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();

        public virtual ICollection<Invoice> SaleInvoices { get; set; } = new HashSet<Invoice>();

        public virtual ICollection<Invoice> BuyInvoices { get; set; } = new HashSet<Invoice>();
    }
}
