using System;

namespace Wilson.Accounting.Core.Entities
{
    public class Payment : Entity
    {
        public DateTime Date { get; set; }

        public string InvoiceId { get; set; }

        public string PriceId { get; set; }

        public virtual Price Price { get; set; }

        public virtual Invoice Invoice { get; set; }
    }
}
