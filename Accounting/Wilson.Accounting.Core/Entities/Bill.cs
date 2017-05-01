using System;
using System.Collections.Generic;
using Wilson.Accounting.Core.Enumerations;

namespace Wilson.Accounting.Core.Entities
{
    public class Bill : Entity
    {
        public DateTime Date { get; private set; }

        public decimal Amount { get; private set; }

        public bool HasInvoice { get; private set; }

        public string HtmlContent { get; private set; }

        public string ProjectId { get; private set; }

        public string InvoiceId { get; private set; }

        public virtual Project Project { get; private set; }

        public virtual Invoice Invoice { get; private set; }
        
        public void AttachInvoice(Invoice invoice)
        {
            this.HasInvoice = true;
            this.InvoiceId = invoice.Id;
        }
    }
}
