namespace Wilson.Companies.Core.Entities
{
    public class InquiryEmployee : IEntity
    {
        public string InquiryId { get; set; }

        public string EmployeeId { get; set; }

        public virtual Inquiry Inquiry { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
