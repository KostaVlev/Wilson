namespace Wilson.Accounting.Core.Enumerations
{
    /// <summary>
    /// Defining the Invoice variant.
    /// </summary>
    public enum InvoiceVariant
    {
        /// <summary>
        /// The actual document issued by the seller related to sell transaction.
        /// </summary>
        Invoice = 0,

        /// <summary>
        /// Document that states a commitment from the seller to provide goods or service to
        /// the buyer and it is issued before the Invoice.
        /// </summary>
        ProformaInvoice = 1,

        /// <summary>
        /// Document that is issued when the buyer returns goods or the price of issued Invoice 
        /// is higher then what actually the buyer has to pay.
        /// </summary>
        CreditMemo = 2,

        /// <summary>
        /// Document that issued when the price of issued Invoice is lower the what actually
        /// the buyer has to pay.
        /// </summary>
        DebitMemo = 3
    }
}
