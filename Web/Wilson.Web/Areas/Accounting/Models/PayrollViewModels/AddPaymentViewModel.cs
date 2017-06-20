using System;
using System.ComponentModel.DataAnnotations;
using Wilson.Web.Areas.Accounting.Models.SharedViewModels;

namespace Wilson.Web.Areas.Accounting.Models.PayrollViewModels
{
    public class AddPaymentViewModel
    {
        public DateTime From { get; set; }
        
        public DateTime To { get; set; }
        
        [StringLength(36)]
        public string EmployeeId { get; set; }
        
        [StringLength(36)]
        public string PaycheckId { get; set; }

        public PaymentViewModel Payment { get; set; }

        public static AddPaymentViewModel Create(DateTime from, DateTime to, string employeeId, string paycheckId)
        {
            return new AddPaymentViewModel()
            {
                From = from,
                To = to,
                EmployeeId = employeeId,
                PaycheckId = paycheckId,
                Payment = PaymentViewModel.Create()
            };
        }
        public static AddPaymentViewModel ReBuild(AddPaymentViewModel model)
        {
            return model;
        }
    }
}
