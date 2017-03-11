using System;

namespace Wilson.Accounting.Core.Entities
{
    public class StorehouseItem : IEntity
    {
        public Guid StorehouseId { get; set; }

        public Guid ItemId { get; set; }

        public virtual Storehouse Storehouse { get; set; }

        public virtual Item Item { get; set; }
    }
}
