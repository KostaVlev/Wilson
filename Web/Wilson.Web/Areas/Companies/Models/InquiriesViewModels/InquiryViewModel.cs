using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Wilson.Web.Areas.Companies.Models.SharedViewModels;

namespace Wilson.Web.Areas.Companies.Models.InquiriesViewModels
{
    public class InquiryViewModel
    {
        public string Id { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = @"{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        [Display(Name = "Received At")]
        public DateTime ReceivedAt { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = @"{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        [Display(Name = "Closed At")]
        public DateTime ClosedAt { get; set; }

        [Required]
        [StringLength(900, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 70)]
        [DataType(DataType.Text)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Received By")]
        public EmployeeViewModel ReceivedBy { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public CompanyViewModel Customer { get; set; }

        public IEnumerable<AttachmentViewModel> Attachmnets { get; set; }

        public IEnumerable<InfoRequestViewModel> InfoRequests { get; set; }

        public IEnumerable<InquiryEmployeeViewModel> Assignees { get; set; }

        public IEnumerable<OfferViewModel> Offers { get; set; }
    }
}
