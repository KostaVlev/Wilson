using System;
using System.Collections.Generic;
using System.Linq;

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

        public override string ToString()
        {
            return this.FirstName + " " + this.LastName;
        }

        public void FilterPaycheksByDate(DateTime from, DateTime to)
        {
            this.Paycheks.Where(p => p.From >= from && p.To <= to);
        }
    }
}
