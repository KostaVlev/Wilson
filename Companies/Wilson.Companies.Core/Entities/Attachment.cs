using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Wilson.Companies.Core.Entities
{
    public class Attachment : Entity
    {    
        private const int MAX_SIZE = 15000;

        private Attachment()
        {
        }

        public string FileName { get; private set; }
        
        public string Extention { get; private set; }

        public DateTime UploadDate { get; private set; }

        public byte[] File { get; private set; }

        public long Size { get; private set; }

        public string ContractId { get; private set; }

        public string InfoRequestId { get; private set; }

        public string InforequestResponseId { get; private set; }

        public string InquiryId { get; private set; }

        public virtual CompanyContract Cotract { get; private set; }

        public virtual InfoRequest InfoRequest { get; private set; }

        public virtual InfoRequest InforequestResponse { get; private set; }

        public virtual Inquiry Inquiry { get; private set; }

        public static Attachment Create(
            IFormFile file, 
            CompanyContract contract = null, 
            InfoRequest infoRequest = null, 
            InfoRequest infoRequestResponse = null,
            Inquiry inquiry = null)
        {
            if (file.Length > MAX_SIZE || file.Length == 0)
            {
                throw new ArgumentOutOfRangeException("file", $"File attachment size can't be 0 or bigger then {MAX_SIZE}");
            }

            if (file.FileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                throw new ArgumentException("file", "File name contains invalid characters.");
            }

            var filePath = Path.GetTempFileName();

            using (var stream = new MemoryStream())
            {
                file.CopyTo(stream);
                var attachment = new Attachment()
                {
                    FileName = file.FileName,
                    Extention = Path.GetExtension(filePath),
                    UploadDate = DateTime.Now,
                    File = stream.ToArray(),
                    ContractId = contract?.Id,
                    InfoRequestId = infoRequest?.Id,
                    InforequestResponseId = infoRequestResponse?.Id,
                    InquiryId = inquiry?.Id
                };

                return attachment;
            }
        }
    }
}
