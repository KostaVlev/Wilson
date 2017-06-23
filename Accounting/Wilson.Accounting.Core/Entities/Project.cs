using System.Collections.Generic;

namespace Wilson.Accounting.Core.Entities
{
    public class Project : Entity
    {
        private Project()
        {
        }

        public string Name { get; private set; }

        public string CustomerId { get; private set; }

        public virtual Company Customer { get; private set; }

        public virtual ICollection<Bill> Bills { get; private set; }

        public Storehouse CreateStorehouse(string name)
        {
            return Storehouse.Create(name, this.Id);
        }
    }
}
