using System;

namespace Wilson.Accounting.Core.Entities
{
    public class Employee : Entity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Guid CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}
