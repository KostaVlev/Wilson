using System;

namespace Wilson.Companies.Core.Entities
{
    public class Attachment : Entity
    {
        public string FileName { get; set; }
        
        public string Extention { get; set; }

        public DateTime UploadDate { get; set; }

        public byte[] File { get; set; }

        public Guid? ContractId { get; set; }

        public Guid? InfoRequestId { get; set; }

        public Guid? InforequestResponseId { get; set; }

        public Guid? InquiryId { get; set; }

        public virtual CompanyContract Cotract { get; set; }

        public virtual InfoRequest InfoRequest { get; set; }

        public virtual InfoRequest InforequestResponse { get; set; }

        public virtual Inquiry Inquiry { get; set; }
    }
}
