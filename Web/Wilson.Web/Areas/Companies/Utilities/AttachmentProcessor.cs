using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Wilson.Companies.Core.Entities;

namespace Wilson.Web.Areas.Companies.Utilities
{
    public class AttachmentProcessor : IAttachmnetProcessor
    {
        public async Task<IEnumerable<Attachment>> PrepareForUpload(IEnumerable<IFormFile> formFiles)
        {
            long size = formFiles.Sum(f => f.Length);
            var attachments = new List<Attachment>();

            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            foreach (var formFile in formFiles)
            {
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
