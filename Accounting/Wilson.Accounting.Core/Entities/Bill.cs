using System;

namespace Wilson.Accounting.Core.Entities
{
    public class Bill : Entity
    {
        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public string HtmlContent { get; set; }

        public Guid ProjectId { get; set; }

        public Guid InvoiceId { get; set; }

        public Project Project { get; set; }

        public Invoice Invoice { get; set; }
    }
}
