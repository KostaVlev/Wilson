using System;
using Wilson.Scheduler.Core.Entities.ValueObjects;

namespace Wilson.Web.Areas.Scheduler.Models.PayrollViewModels
{
    public class PaycheckViewModel
    {
        public DateTime Date { get; set; }

        public Period Period { get; set; }

        public WorkingHours WorkingHours { get; set; }

        public DaysOff DaysOff { get; set; }

        public SubTotals SubTotals { get; set; }

        public decimal Total { get; set; }
    }
}