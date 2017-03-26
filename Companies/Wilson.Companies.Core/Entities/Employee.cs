using System.Collections.Generic;
using Wilson.Companies.Core.Enumerations;

namespace Wilson.Companies.Core.Entities
{
    public class Employee : Entity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string PrivatePhone { get; set; }

        public string Email { get; set; }

        public EmployeePosition EmployeePosition { get; set; }

        public string CompanyId { get; set; }

        public string AddressId { get; set; }

        public virtual Company Company { get; set; }

        public virtual Address Address { get; set; }

        public ICollection<InfoRequest> InfoRequests { get; set; } = new HashSet<InfoRequest>();
    }
}
