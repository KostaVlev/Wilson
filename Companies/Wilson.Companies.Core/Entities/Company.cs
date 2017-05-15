using System.Collections.Generic;
using Wilson.Companies.Core.Enumerations;

namespace Wilson.Companies.Core.Entities
{
    public class Company : Entity
    {
        private Company()
        {
        }

        public string Name { get; private set; }

        public string RegistrationNumber { get; private set; }

        public string VatNumber { get; private set; }

        public string OfficeEmail { get; private set; }

        public string OfficePhone { get; private set; }

        public string Address { get; private set; }

        public string ShippingAddress { get; private set; }

        public bool HasVatRegistration { get; private set; }

        public ICollection<Employee> Employees { get; private set; }

        public ICollection<Project> Projects { get; private set; }

        public void ChangeShippingAddress(Address shippingAddress)
        {
            this.Address = Address;
        }

        public Address GetAddress()
        {
            return (Address)this.Address;
        }

        public Address GetShippingAddress()
        {
            return (Address)this.ShippingAddress;
        }

        public void AddEmployee(
            string firstName,
            string lastName,
            string phone,
            string privatePhone,
            string email,
            EmployeePosition position,
            Address address)
        {
            var employee = Employee.Create(firstName, lastName, phone, privatePhone, email, position, address, this.Id);
            this.Employees.Add(employee);
        }
    }
}
