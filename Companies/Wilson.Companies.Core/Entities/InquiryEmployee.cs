namespace Wilson.Companies.Core.Entities
{
    public class InquiryEmployee : IEntity
    {
        private InquiryEmployee()
        {
        }

        public string InquiryId { get; private set; }

        public string EmployeeId { get; private set; }

        public virtual Inquiry Inquiry { get; private set; }

        public virtual Employee Employee { get; private set; }

        public static InquiryEmployee Create(string assigneeId, string inquiryId)
        {
            return new InquiryEmployee()
            {
                InquiryId = assigneeId,
                EmployeeId = assigneeId,
            };
        }
    }
}
