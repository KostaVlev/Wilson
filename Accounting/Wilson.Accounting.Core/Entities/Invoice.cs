using System;
using System.Collections.Generic;
using Wilson.Accounting.Core.Enumerations;

namespace Wilson.Accounting.Core.Entities
{
    public class Invoice : Entity
    {
        public string Number { get; set; }

        public InvoiceVariant InvoiceVariant { get; set; }

        public InvoiceType InvoiceType { get; set; }

        public InvoicePaymentType InvoicePaymentType { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime? DateOfPayment { get; set; }

        public int DaysOfDelayedPayment { get; set; }

        public int Vat { get; set; }

        public decimal SubTotal { get; set; }

        public decimal VatAmount { get; set; }

        public decimal Total { get; set; }

        public decimal PayedAmount { get; set; }

        public bool IsPayed { get; set; }

        public Guid BuyerId { get; set; }

        public Guid SellerId { get; set; }

        public Guid? BillId { get; set; }

        public virtual Company Buyer { get; set; }

        public virtual Company Seller { get; set; }

        public virtual Bill Bill { get; set; }

        public virtual ICollection<InvoiceItem> Items { get; set; } = new HashSet<InvoiceItem>();

        public virtual ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();
    }
}
