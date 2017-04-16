using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Wilson.Companies.Core.Entities;

namespace Wilson.Web.Utilities
{
    /// <summary>
    /// Takes care of files/Attachments that will be uploaded to the database.
    /// </summary>
    public interface IAttachmnetProcessor
    {
        /// <summary>
        /// Asyc method which gets ready Files/Attachments to be uploaded to the database. 
        /// Not suitable for large files.
        /// </summary>
        /// <param name="formFiles">Collection of <see cref="IFormFile"/> which will be processed.</param>
        /// <returns>Collection of <see cref="Attachment"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If the size of the files expressed the allowed size.</exception>
        /// <exception cref="ArgumentException">If one of the files is invalid.</exception>
        /// <example>await PrepareForUpload(...)</example>
        Task<IEnumerable<Attachment>> PrepareForUpload(IEnumerable<IFormFile> formFiles);
    }
}
