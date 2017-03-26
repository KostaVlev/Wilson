using System;
using System.Collections.Generic;

namespace Wilson.Projects.Core.Entities
{
    public class StorehouseItem : Entity
    {
        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string StorehouseId { get; set; }

        public string ItemId { get; set; }        

        public virtual Storehouse Storehouse { get; set; }

        public virtual Item Item { get; set; }

        public virtual ICollection<StorehouseItemBill> Bills { get; set; } = new HashSet<StorehouseItemBill>();
    }
}
