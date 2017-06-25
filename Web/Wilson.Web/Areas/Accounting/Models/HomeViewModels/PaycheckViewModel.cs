using System;
using System.Collections.Generic;
using System.Linq;
using Wilson.Accounting.Core.Entities.ValueObjects;
using Wilson.Web.Areas.Accounting.Models.SharedViewModels;

namespace Wilson.Web.Areas.Accounting.Models.HomeViewModels
{
    public class PaycheckViewModel
    {
        public string Id { get; set; }

        public DateTime Date { get; set; }

        public Period Period { get; set; }

        public decimal Total { get; set; }

        public decimal PayedAmount { get => this.Payments.Sum(p => p.Amount); }

        public decimal Diffrence { get => this.Total - this.PayedAmount; }

        public bool IsPaied { get; set; }

        public IEnumerable<PaymentViewModel> Payments { get; set; }
    }
}
