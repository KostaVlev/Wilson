namespace Wilson.Accounting.Core.Entities
{
    public class Price : Entity
    {
        public decimal Amount { get; set; }

        public string ItemId { get; set; }

        public string PaymentId { get; set; }

        public virtual Item Item { get; set; }

        public virtual Payment Payment { get; set; }
    }
}
