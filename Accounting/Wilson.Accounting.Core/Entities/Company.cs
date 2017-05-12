using System;
using System.Collections.Generic;

namespace Wilson.Accounting.Core.Entities
{
    public class Company : Entity
    {
        private Company()
        {
        }

        public string Name { get; private set; }

        public string RegistrationNumber { get; private set; }

        public string VatNumber { get; private set; }

        public bool HasVatRegistration { get; private set; }

        public string Address { get; private set; }

        public virtual ICollection<Employee> Employees { get; private set; }

        public virtual ICollection<Invoice> SaleInvoices { get; private set; }

        public virtual ICollection<Invoice> BuyInvoices { get; private set; }

        public static Company Create(string name, string registrationNumber, Address address, string vatNumber = null)
        {
            var company = new Company() { Name = name, RegistrationNumber = registrationNumber, Address = address };
            if (!string.IsNullOrEmpty(vatNumber))
            {
                company.VatNumber = vatNumber;
                company.HasVatRegistration = true;
            }

            return company;
        }

        public bool HasVat()
        {
            return this.HasVatRegistration;
        }        

        public void SetVatRegistration(string vatNumber)
        {
            if (string.IsNullOrEmpty(vatNumber))
            {
                throw new ArgumentNullException("vatNumber", "VAT registration number can't be empty or null");
            }

            this.VatNumber = vatNumber;
            this.HasVatRegistration = true;
        }

        public void RemoveVatRegistration()
        {
            this.VatNumber = null;
            this.HasVatRegistration = false;
        }

        public void AddSaleInvoice(Invoice invoice)
        {
            if (this.SaleInvoices.Contains(invoice))
            {
                throw new ArgumentException("Only new invoices can be added.", "invoice");
            }

            this.SaleInvoices.Add(invoice);
        }

        public void AddBuyInvoice(Invoice invoice)
        {
            if (this.BuyInvoices.Contains(invoice))
            {
                throw new ArgumentException("Only new invoices can be added.", "invoice");
            }

            this.BuyInvoices.Add(invoice);
        }

        public Address GetAddress()
        {
            return (Address)this.Address;
        }
    }
}
