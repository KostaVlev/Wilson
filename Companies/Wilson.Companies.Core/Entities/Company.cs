using System.Collections.Generic;

namespace Wilson.Companies.Core.Entities
{
    public class Company : Entity
    {
        public string Name { get; set; }

        public string RegistrationNumber { get; set; }

        public string VatNumber { get; set; }

        public string OfficeEmail { get; set; }

        public string OfficePhone { get; set; }

        public string AddressId { get; set; }

        public string ShippingAddressId { get; set; }

        public virtual Address Address { get; set; }

        public virtual Address ShippingAddress { get; set; }

        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();

        public ICollection<Project> Projects { get; set; } = new HashSet<Project>();
    }
}
