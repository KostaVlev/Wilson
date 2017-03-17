using System;

namespace Wilson.Projects.Core.Entities
{
    public class StorehouseItemBill : IEntity
    {
        public Guid StorehouseItemId { get; set; }

        public Guid BillId { get; set; }

        public virtual StorehouseItem StorehouseItem { get; set; }

        public virtual Bill Bill { get; set; }
    }
}
