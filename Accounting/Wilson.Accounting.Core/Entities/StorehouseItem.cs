namespace Wilson.Accounting.Core.Entities
{
    public class StorehouseItem : IEntity
    {
        public string StorehouseId { get; set; }

        public string InvoiceItemId { get; set; }

        public virtual Storehouse Storehouse { get; set; }

        public virtual InvoiceItem InvoiceItem { get; set; }
    }
}
