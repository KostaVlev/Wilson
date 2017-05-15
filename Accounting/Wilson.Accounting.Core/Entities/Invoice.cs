using System;
using System.Collections.Generic;
using System.Linq;
using Wilson.Accounting.Core.Enumerations;

namespace Wilson.Accounting.Core.Entities
{
    public class Invoice : Entity
    {
        private Invoice()
        {
        }

        public string Number { get; private set; }

        public InvoiceVariant InvoiceVariant { get; private set; }

        public InvoiceType InvoiceType { get; private set; }

        public InvoicePaymentType InvoicePaymentType { get; private set; }

        public DateTime IssueDate { get; private set; }

        public DateTime PaymentDate { get; private set; }

        public DateTime? DateOfPayment { get; private set; }

        public int DaysOfDelayedPayment { get; private set; }

        public int Vat { get; private set; }

        public decimal SubTotal { get; private set; }

        public decimal VatAmount { get; private set; }

        public decimal Total { get; private set; }

        public decimal PayedAmount { get; private set; }

        public bool IsPayed { get; private set; }

        public string BuyerId { get; private set; }

        public string SellerId { get; private set; }

        public string BillId { get; private set; }

        public virtual Company Buyer { get; private set; }

        public virtual Company Seller { get; private set; }

        public virtual Bill Bill { get; private set; }

        public string Payments { get; private set; }

        public virtual ICollection<InvoiceItem> InvoiceItems { get; private set; }

        public static Invoice Create(
            string number,
            string buyerId,
            string sellerId,
            DateTime issueDate,
            InvoiceType invoiceType = InvoiceType.Purchase,
            InvoiceVariant invoiceVariant = InvoiceVariant.Invoice,              
            InvoicePaymentType invoicePaymentType = InvoicePaymentType.BankTransfer,
            int daysOfDelayedPayment = 0,
            int vat = 20)
        {
            var invoice = new Invoice()
            {
                Number = number,
                IssueDate = issueDate,
                BuyerId = buyerId,
                SellerId = sellerId,
                InvoiceType = invoiceType,
                InvoiceVariant = invoiceVariant,
                InvoicePaymentType = invoicePaymentType,
                DaysOfDelayedPayment = daysOfDelayedPayment,
                Vat = vat,
                IsPayed = invoicePaymentType == InvoicePaymentType.BankTransfer ? false : true,
                InvoiceItems = new HashSet<InvoiceItem>()
            };

            Validate(invoice);

            if (daysOfDelayedPayment == 0)
            {
                invoice.PaymentDate = DateTime.Now;                
            }
            else
            {
                invoice.PaymentDate = DateTime.Now.AddDays(daysOfDelayedPayment);                
            }

            if (invoicePaymentType != InvoicePaymentType.BankTransfer)
            {
                invoice.DateOfPayment = DateTime.Now;
            }

            return invoice;
        }

        public static Invoice CreateInvoiceFromBill(
            string number,
            string buyerId,
            string sellerId,
            DateTime issueDate,
            Bill bill,
            InvoiceType invoiceType = InvoiceType.Purchase,
            InvoiceVariant invoiceVariant = InvoiceVariant.Invoice,
            InvoicePaymentType invoicePaymentType = InvoicePaymentType.BankTransfer,
            int daysOfDelayedPayment = 0)
        {
            var invoice = Invoice.Create(
                number,
                buyerId,
                sellerId,
                issueDate,
                invoiceType,
                invoiceVariant,
                invoicePaymentType,
                daysOfDelayedPayment);

            var invoiceItem = InvoiceItem.Create(
                1,
                bill.Amount,
                Item.Create(
                    string.Format("Bill from {0} for Project: {0}", bill.Date, bill.Project.Name), Мeasure.Pcs), invoice);

            bill.AttachInvoice(invoice);

            return invoice;
        }

        public void AddItem(int quantity, decimal price, Item item, Storehouse storehouse)
        {
            var invoiceItem = InvoiceItem.Create(quantity, price, item, this, storehouse);
            var itemToUpdate = this.InvoiceItems.FirstOrDefault(x => x.Equals(invoiceItem));
            this.UpdateInvoiceItems(invoiceItem);
            this.UpdateSums();            
        }

        public void AddItem(Dictionary<Storehouse, int> quantities, decimal price, Item item, Storehouse storehouse)
        {
            var invoiceItem = InvoiceItem.Create(quantities, price, item, this);            
            this.UpdateInvoiceItems(invoiceItem);
            this.UpdateSums();
        }

        public void AddPayment(DateTime date, decimal amount)
        {
            decimal paidAmount = this.GetPaidAmount();
            decimal diffrenceToPay = this.Total - paidAmount;
            if (diffrenceToPay == amount)
            {
                this.IsPayed = true;
            }

            if (diffrenceToPay < amount)
            {
                throw new ArgumentOutOfRangeException("amount", "Payment amount can't be more then the amount that have to be paid.");
            }

            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount", "Payment amount can't be negative number.");
            }

            this.Payments = this.GetPayments().Add(Payment.Create(date, amount));
        }

        public decimal GetPaidAmount()
        {
            return this.GetPayments().Sum();
        }

        public ListOfPayments GetPayments()
        {
            return (ListOfPayments)this.Payments;
        }

        public void SetSeller(Company company)
        {
            this.SellerId = company.Id;
            this.Seller = company;
        }

        public void SetBuyer(Company company)
        {
            this.BuyerId = company.Id;
            this.Buyer = company;
        }

        private void UpdateSums()
        {
            this.SubTotal = CalculateSubTotal(this.InvoiceItems);
            this.VatAmount = CalculateVat(this, this.Vat);
            this.Total = CalculateTotal(this);
        }

        private void UpdateInvoiceItems(InvoiceItem invoiceItem)
        {
            var itemToUpdate = this.InvoiceItems.FirstOrDefault(x => x.Equals(invoiceItem));
            if (itemToUpdate == null)
            {
                this.InvoiceItems.Add(invoiceItem);
            }
            else
            {
                itemToUpdate.AddQuantity(invoiceItem.Quantity);
            }
        }

        private static decimal CalculateSubTotal(IEnumerable<InvoiceItem> items)
        {
            return items.Sum(x => x.Price * x.Quantity);
        }

        private static decimal CalculateVat(Invoice invoice, int vat)
        {
            return invoice.SubTotal * (vat / 100);
        }

        private static decimal CalculateTotal(Invoice invoice)
        {
            return invoice.SubTotal + invoice.VatAmount;
        }

        private static void Validate(Invoice invoice)
        {
            int numberLenght = 10;
            if (string.IsNullOrEmpty(invoice.Number) || string.IsNullOrWhiteSpace(invoice.Number))
            {
                throw new ArgumentNullException("number", "Invoice number can't be null or empty");
            }

            if (invoice.Number.Length != numberLenght)
            {
                throw new ArgumentOutOfRangeException("number", string.Format("Invoice number must be {0} characters long.", numberLenght));
            }

            if (!Guid.TryParse(invoice.BuyerId, out Guid buyerId))
            {
                throw new ArgumentException("Invalid buyer identity", "buyerId");
            }

            if (!Guid.TryParse(invoice.SellerId, out Guid sellerId))
            {
                throw new ArgumentException("Invalid buyer identity", "buyerId");
            }

            if (string.IsNullOrEmpty(invoice.BillId) && !Guid.TryParse(invoice.BillId, out Guid billId))
            {
                throw new ArgumentException("Invalid buyer identity", "buyerId");
            }

            if (invoice.InvoiceItems == null || invoice.InvoiceItems.Count <= 0)
            {
                throw new ArgumentException("Invoice items can't be empty.", "items");
            }

            if (invoice.Vat < 0 || invoice.Vat < 100)
            {
                throw new ArgumentOutOfRangeException("VAT can't be negative number or more then 100%.", "vat");
            }
        }
    }
}
