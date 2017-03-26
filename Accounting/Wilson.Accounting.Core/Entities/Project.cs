using System.Collections.Generic;

namespace Wilson.Accounting.Core.Entities
{
    public class Project : Entity
    {
        public string Name { get; set; }

        public string CustomerId { get; set; }

        public Company Customer { get; set; }

        public virtual ICollection<Bill> Bills { get; set; } = new HashSet<Bill>();
    }
}
