using System;
using System.Linq;

namespace Wilson.Accounting.Core.Entities
{
    public class Bill : Entity
    {
        protected Bill()
        {
        }

        public DateTime Date { get; private set; }

        public DateTime RevisionDate { get; private set; }

        public string BillItems { get; private set; }

        public decimal Amount { get; private set; }

        public bool HasInvoice { get; private set; }

        public string HtmlContent { get; private set; }

        public string ProjectId { get; private set; }

        public string InvoiceId { get; private set; }

        public virtual Project Project { get; private set; }

        public virtual Invoice Invoice { get; private set; }

        public static Bill Create(DateTime date, ListOfBillItems billItems, string projectId)
        {
            return new Bill()
            {
                Date = date,
                RevisionDate = date,
                BillItems = billItems,
                Amount = CalculateAmount(billItems),
                ProjectId = projectId,
                HasInvoice = false
            };
        }

        public void Revise(DateTime revisionDate, ListOfBillItems billItems)
        {
            if (this.HasInvoice)
            {
                throw new InvalidOperationException("Invoice has been issued and the bill can not be changed.");
            }

            if (this.Date > revisionDate)
            {
                throw new ArgumentOutOfRangeException("revisionDate", "Revision date can't be prior then the creation date.");
            }

            if (this.RevisionDate > revisionDate)
            {
                throw new ArgumentOutOfRangeException("revisionDate", "Revision date can't be prior then the last revision date.");
            }

            this.RevisionDate = revisionDate;
            this.BillItems = billItems;
            this.Amount = CalculateAmount(billItems);
        }

        public void AttachInvoice(string invoiceId)
        {
            if (this.HasInvoice)
            {
                throw new InvalidOperationException("Invoice has been already issued.");
            }

            this.HasInvoice = true;
            this.InvoiceId = invoiceId;
        }

        public void AddBillItem(BillItem billItem)
        {
            if (this.HasInvoice)
            {
                throw new InvalidOperationException("Invoice has been issued and the bill can not be changed.");
            }

            var billItems = this.GetBillItems();
            if (!billItems.Contains(billItem))
            {
                this.BillItems = billItems.Add(billItem); 
            }
            else
            {
                var billItemToUpdate = billItems.FirstOrDefault(x => x.Equals(billItem));
                billItemToUpdate.AddQuantity(billItem.Quantity);
                this.BillItems = billItems;
            }
        }

        public ListOfBillItems GetBillItems()
        {
            if (string.IsNullOrEmpty(this.BillItems))
            {
                return ListOfBillItems.Create();
            }

            return (ListOfBillItems)this.BillItems;
        }

        private static decimal CalculateAmount(ListOfBillItems billItems)
        {
            return billItems.Sum(i => i.Price * i.Quantity);
        }
    }
}
