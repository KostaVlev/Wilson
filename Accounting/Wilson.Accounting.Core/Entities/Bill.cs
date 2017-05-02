using System;
using System.Linq;

namespace Wilson.Accounting.Core.Entities
{
    public class Bill : Entity
    {
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

        public static Bill Create(DateTime date, Project project)
        {
            return new Bill() { Date = date, RevisionDate = date, Project = project, ProjectId = project.Id, HasInvoice = false };
        }

        public void SetRevisionDate(DateTime date)
        {
            this.RevisionDate = date;
        }
        
        public void AttachInvoice(Invoice invoice)
        {
            this.HasInvoice = true;
            this.InvoiceId = invoice.Id;
        }

        public ListOfBillItems AddBillItem(BillItem billItem)
        {
            var billItems = this.GetBillItems();
            if (!billItems.Contains(billItem))
            {
                billItems.Add(billItem);
            }
            else
            {
                var billItemToUpdate = billItems.FirstOrDefault(x => x.Equals(billItem));
                billItemToUpdate.AddQuantity(billItem.Quantity);
            }

            return ListOfBillItems.Create(billItems);
        }

        public ListOfBillItems GetBillItems()
        {
            return (ListOfBillItems)this.BillItems;
        }
    }
}
