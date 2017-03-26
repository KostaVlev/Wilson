namespace Wilson.Projects.Core.Entities
{
    public class StorehouseItemBill : IEntity
    {
        public string StorehouseItemId { get; set; }

        public string BillId { get; set; }

        public virtual StorehouseItem StorehouseItem { get; set; }

        public virtual Bill Bill { get; set; }
    }
}
