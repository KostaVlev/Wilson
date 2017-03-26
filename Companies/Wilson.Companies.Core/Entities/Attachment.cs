using System;

namespace Wilson.Companies.Core.Entities
{
    public class Attachment : Entity
    {
        public string FileName { get; set; }
        
        public string Extention { get; set; }

        public DateTime UploadDate { get; set; }

        public byte[] File { get; set; }

        public string ContractId { get; set; }

        public string InfoRequestId { get; set; }

        public string InforequestResponseId { get; set; }

        public string InquiryId { get; set; }

        public virtual CompanyContract Cotract { get; set; }

        public virtual InfoRequest InfoRequest { get; set; }

        public virtual InfoRequest InforequestResponse { get; set; }

        public virtual Inquiry Inquiry { get; set; }
    }
}
