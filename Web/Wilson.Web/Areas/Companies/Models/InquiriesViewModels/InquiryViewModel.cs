using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Wilson.Web.Areas.Companies.Models.SharedViewModels;

namespace Wilson.Web.Areas.Companies.Models.InquiriesViewModels
{
    public class InquiryViewModel
    {
        public string Id { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = @"{0:dd/MM/yyyy}", NullDisplayText = "")]
        [DataType(DataType.Date)]
        [Display(Name = "Received At")]
        public DateTime ReceivedAt { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = true, DataFormatString = @"{0:dd/MM/yyyy}", NullDisplayText = "")]
        [DataType(DataType.Date)]
        [Display(Name = "Closed At")]
        public DateTime ClosedAt { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public EmployeeViewModel RecivedBy { get; set; }

        public CompanyViewModel Customer { get; set; }

        public IEnumerable<AttachmentViewModel> Attachmnets { get; set; }

        public IEnumerable<InfoRequestViewModel> InfoRequests { get; set; }

        public IEnumerable<InquiryEmployeeViewModel> Assignees { get; set; }

        public IEnumerable<OfferViewModel> Offers { get; set; }
    }
}
