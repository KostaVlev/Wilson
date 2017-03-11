using System;
using System.Collections.Generic;
using System.Linq;
using Wilson.Accounting.Core.Entities;

namespace Wilson.Accounting.Core.Aggregates
{
    /// <summary>
    /// Provides way to manage <see cref="Invoice"/>.
    /// </summary>
    public class InvoiceAggregate : Aggregate<Invoice>
    {
        private Invoice Invoice
        {
            get { return this.Entity as Invoice; }
        }

        /// <summary>
        /// Adds item in the Invoice. If the item is already in the Invoice then
        /// the item quantity is increase.
        /// </summary>
        /// <param name="item">The item that will be added.</param>
        /// <param name="price">The price for the item.</param>
        public void AddItem(Item item, Price price)
        {
            item.Prices.Add(price);
            var newInvoiceItem = new InvoiceItem()
            {
                ItemId = item.Id,
                InvoiceId = this.Invoice.Id,
                PriceId = price.Id,
                Quantity = 1,
                Item = item,
                Price = price
            };

            var invoiceItem = this.Invoice.Items.Where(i => i.Equals(newInvoiceItem)).FirstOrDefault();
            if (invoiceItem == null)
            {
                this.Invoice.Items.Add(newInvoiceItem);
            }
            else
            {
                invoiceItem.Quantity++;
            }
        }

        /// <summary>
        /// Lists the items which are added in the Invoice.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<InvoiceItem> ListInvoiceItems()
        {
            return this.Invoice.Items;
        }

        /// <summary>
        /// Calculates sub total for all the items in the Invoice.
        /// </summary>
        public void CalculateSubTotal()
        {
            foreach (var invoiceItem in this.Invoice.Items)
            {
                this.Invoice.SubTotal += invoiceItem.Quantity * invoiceItem.Price.Amount;
            }
        }

        /// <summary>
        /// Sets the VAT percentage which will be used for VAT calculation.
        /// </summary>
        /// <param name="vatPercentage">The VAT percentage %.</param>
        public void SetVatPercentage(int vatPercentage)
        {
            this.Invoice.Vat = vatPercentage;
        }

        /// <summary>
        /// Calculates the VAT amount.
        /// </summary>
        public void CalculateVatAmount()
        {
            this.Invoice.VatAmount = this.Invoice.SubTotal * (this.Invoice.Vat / 100);
        }

        /// <summary>
        /// Calculates the Invoice total amount.
        /// </summary>
        public void CalculateInvoiceTotal()
        {
            this.Invoice.Total = this.Invoice.SubTotal + this.Invoice.VatAmount;
        }

        /// <summary>
        /// Sets delayed payment.
        /// </summary>
        /// <param name="days">The number of days.</param>
        public void SetDelayedPayement(int days)
        {
            this.Invoice.DaysOfDelayedPayment = days;
        }

        /// <summary>
        /// Exports Invoice as PDF file.
        /// </summary>
        /// <param name="htmlInvoice">HTML content that will be converted to PDF.</param>
        public void ExportAsPdf(string htmlInvoice)
        {
            // TODO Implement PDF export. 
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks if the Invoice is valid.
        /// </summary>
        /// <returns></returns>
        public override bool Validate()
        {
            // TODO Validate the Invoice according the business rules which applies. 
            throw new NotImplementedException();
        }
    }
}
