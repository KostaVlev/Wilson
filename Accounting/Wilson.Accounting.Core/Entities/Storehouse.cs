using System.Collections.Generic;

namespace Wilson.Accounting.Core.Entities
{
    public class Storehouse : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<StorehouseItem> Items { get; set; } = new HashSet<StorehouseItem>();
    }
}
