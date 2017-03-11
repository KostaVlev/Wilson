using System.Collections.Generic;
using Wilson.Accounting.Core.Enumerations;

namespace Wilson.Accounting.Core.Entities
{
    public class Item : Entity
    {
        public string Name { get; set; }

        public int Quantity { get; set; }

        public Мeasure Мeasure { get; set; }
        
        public virtual ICollection<InvoiceItem> Invoices { get; set; } = new HashSet<InvoiceItem>();

        public virtual ICollection<Price> Prices { get; set; } = new HashSet<Price>();

        public virtual ICollection<StorehouseItem> Storehouses { get; set; } = new HashSet<StorehouseItem>();
    }
}
