using System.Collections.Generic;

namespace Wilson.Accounting.Core.Entities
{
    public class Employee : Entity
    {
        private Employee()
        {
        }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public bool IsFired { get; private set; }

        public string CompanyId { get; private set; }

        public virtual Company Company { get; private set; }

        public virtual ICollection<Paycheck> Paycheks { get; private set; }
    }
}
