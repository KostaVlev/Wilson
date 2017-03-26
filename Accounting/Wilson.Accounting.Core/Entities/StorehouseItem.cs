using System;

namespace Wilson.Accounting.Core.Entities
{
    public class StorehouseItem : IEntity
    {
        public string StorehouseId { get; set; }

        public string ItemId { get; set; }

        public virtual Storehouse Storehouse { get; set; }

        public virtual Item Item { get; set; }
    }
}
