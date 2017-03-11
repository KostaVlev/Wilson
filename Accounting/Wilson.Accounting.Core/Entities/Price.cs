using System;

namespace Wilson.Accounting.Core.Entities
{
    public class Price : Entity
    {
        public decimal Amount { get; set; }

        public Guid? ItemId { get; set; }

        public Guid? PaymentId { get; set; }

        public virtual Item Item { get; set; }

        public virtual Payment Payment { get; set; }
    }
}
