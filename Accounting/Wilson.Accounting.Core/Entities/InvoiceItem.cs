using System;

namespace Wilson.Accounting.Core.Entities
{
    public class InvoiceItem : IEntity, IValueObject<InvoiceItem>
    {
        public int Quantity { get; set; }

        public Guid InvoiceId { get; set; }

        public Guid ItemId { get; set; }

        public Guid PriceId { get; set; }

        public virtual Invoice Invoice { get; set; }

        public virtual Item Item { get; set; }

        public virtual Price Price { get; set; }

        public bool Equals(InvoiceItem other)
        {
            if (this.ItemId == other.ItemId && this.PriceId == other.PriceId && this.InvoiceId == other.InvoiceId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
