using System.Collections.Generic;
using System.Linq;

namespace Wilson.Accounting.Core.Entities
{
    public class InvoiceItem : Entity, IValueObject<InvoiceItem>
    {
        private InvoiceItem()
        {
        }

        public int Quantity { get; private set; }

        public decimal Price { get; private set; }

        public string InvoiceId { get; private set; }

        public string ItemId { get; private set; }

        public virtual Invoice Invoice { get; private set; }

        public virtual Item Item { get; private set; }        

        public ICollection<StorehouseItem> StorehouseItems { get; private set; }

        public static InvoiceItem Create(
            int quantity, decimal price, Item item, Invoice invoice, Storehouse storehouse = null)
        {
            var invoiceItem = new InvoiceItem()
            {
                Quantity = quantity,
                Price = price,
                ItemId = item.Id,
                InvoiceId = invoice.Id,
                StorehouseItems = new HashSet<StorehouseItem>()
            };

            if (storehouse != null)
            {
                invoiceItem.AddToStorehouse(storehouse, invoiceItem.Quantity);
            }

            return invoiceItem;
        }

        public static InvoiceItem Create(
           Dictionary<Storehouse, int> quantities, decimal price, Item item, Invoice invoice)
        {
            var invoiceItemQuantity = quantities.Sum(x => x.Value);
            var invoiceItem = new InvoiceItem()
            {
                Quantity = invoiceItemQuantity,
                Price = price,
                ItemId = item.Id,
                InvoiceId = invoice.Id,
                StorehouseItems = new HashSet<StorehouseItem>()
            };

            foreach (var storehouse in quantities.Keys)
            {
                var storehouseItem = StorehouseItem.Create(quantities[storehouse], storehouse, invoiceItem);
                invoiceItem.AddToStorehouse(storehouse, quantities[storehouse]);
            }

            return invoiceItem;
        }

        public void AddQuantity(int quantity)
        {
            this.Quantity += quantity;
        }

        public void AddToStorehouse(Storehouse storehouse, int quantity)
        {
            var storehouseItem = StorehouseItem.Create(this.Quantity, storehouse, this);
            var storehouseItemToUpdate = this.StorehouseItems.FirstOrDefault(x => x.Equals(storehouseItem));
            if (storehouseItemToUpdate == null)
            {
                this.StorehouseItems.Add(storehouseItem);
            }
            else
            {
                storehouseItemToUpdate.AddQiantity(this.Quantity);
            }
        }

        public bool Equals(InvoiceItem other)
        {
            if (other == null)
            {
                return false;
            }

            return this.ItemId == other.ItemId && this.Price == other.Price;
        }
    }
}
