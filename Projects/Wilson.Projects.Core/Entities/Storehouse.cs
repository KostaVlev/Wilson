using System;
using System.Collections.Generic;

namespace Wilson.Projects.Core.Entities
{
    public class Storehouse : Entity
    {
        public string Name { get; set; }

        public Guid ProjectId { get; set; }

        public virtual Project Project { get; set; }

        public virtual ICollection<StorehouseItem> Items { get; set; } = new HashSet<StorehouseItem>();
    }
}
