using System;

namespace Wilson.Accounting.Core.Entities
{
    public class StorehouseItem : Entity, IEquatable<StorehouseItem>
    {
        private StorehouseItem()
        {
        }

        public int Quantity { get; private set; }

        public decimal Price { get; private set; }

        public string StorehouseId { get; private set; }

        public string InvoiceItemId { get; private set; }

        public virtual Storehouse Storehouse { get; private set; }

        public virtual InvoiceItem InvoiceItem { get; private set; }

        public static StorehouseItem Create(int quantity, string storehouseId, string invoiceItemId, decimal invoiceItemPrice)
        {
            return new StorehouseItem() { Quantity = quantity, Price = invoiceItemPrice, StorehouseId = storehouseId, InvoiceItemId = invoiceItemId };
        }

        public void AddQiantity(int quantity)
        {
            this.Quantity += quantity;
        }

        public bool Equals(StorehouseItem other)
        {
            if (other == null)
            {
                return false;
            }

            return this.InvoiceItemId == other.InvoiceItemId && this.Price == other.Price;
        }
    }
}
