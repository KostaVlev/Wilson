using System;
using System.Collections.Generic;
using System.Linq;
using Wilson.Accounting.Core.Entities.ValueObjects;

namespace Wilson.Accounting.Core.Entities
{
    public class Storehouse : Entity
    {
        private Storehouse()
        {
        }

        public string Name { get; private set; }

        public string BillItems { get; private set; }

        public string ProjectId { get; private set; }

        public virtual Project Project { get; private set; }

        public virtual ICollection<StorehouseItem> StorehouseItems { get; set; }        

        public static Storehouse Create(string name, string projectId)
        {
            return new Storehouse() { Name = name, ProjectId = projectId, StorehouseItems = new HashSet<StorehouseItem>() };
        }

        public void AddItem(int quantity, InvoiceItem item)
        {
            if (quantity > item.Quantity)
            {
                throw new ArgumentOutOfRangeException("quantity", "Not enough quantity available of the selected item.");
            }

            var storehouseItem = StorehouseItem.Create(quantity, this.Id, item.Id, item.Price);
            var itemToUpdate = this.StorehouseItems.FirstOrDefault(x => x.Equals(storehouseItem));
            if (itemToUpdate == null)
            {
                this.StorehouseItems.Add(storehouseItem);
            }
            else
            {                
                itemToUpdate.AddQiantity(quantity);
            }
        }

        public ListOfBillItems GetBillItems()
        {
            return (ListOfBillItems)this.BillItems;
        }

        public ListOfBillItems AddBilledItems(Bill bill)
        {
            var billedItems = bill.GetBillItems();
            billedItems.AddRange(bill.GetBillItems());

            return ListOfBillItems.Create(billedItems);
        }
    }
}
