using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Wilson.Companies.Core.Entities;

namespace Wilson.Web.Utilities
{
    public class AttachmentProcessor : IAttachmnetProcessor
    {
        private const int MAX_UPLOAD_SIZE = 15000;

        public async Task<IEnumerable<Attachment>> PrepareForUpload(IEnumerable<IFormFile> formFiles)
        {            
            long size = formFiles.Sum(f => f.Length);
            if (size > MAX_UPLOAD_SIZE)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(formFiles), 
                    string.Format("{0} {1}",Constants.ExceptionMessages.FileToLarge, MAX_UPLOAD_SIZE));
            }

            var attachments = new List<Attachment>();

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            foreach (var formFile in formFiles)
            {
                if (formFile.FileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                {
                    throw new ArgumentException(
                    "formFile",
                    Constants.ExceptionMessages.InvalidFile);
                }

                if (formFile.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await formFile.CopyToAsync(stream);
                        var attachment = new Attachment()
                        {
                            FileName = formFile.FileName,
                            Extention = "jpg",
                            UploadDate = DateTime.Now,  
                            File = stream.ToArray()
                        };
                        
                        attachments.Add(attachment);
                    }
                }
            }

            return attachments;
        }
    }
}
