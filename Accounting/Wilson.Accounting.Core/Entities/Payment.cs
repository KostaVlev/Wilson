using System;

namespace Wilson.Accounting.Core.Entities
{
    public class Payment : Entity
    {
        public DateTime Date { get; set; }

        public Guid InvoiceId { get; set; }

        public Guid PriceId { get; set; }

        public virtual Price Price { get; set; }

        public virtual Invoice Invoice { get; set; }
    }
}
