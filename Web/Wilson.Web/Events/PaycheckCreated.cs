using System;
using Wilson.Scheduler.Core.Entities;
using Wilson.Web.Events.Interfaces;

namespace Wilson.Web.Events
{
    public class PaycheckCreated : IDomainEvent
    {
        public PaycheckCreated(Paycheck paycheck)
        {
            this.Paycheck = paycheck;
            this.DateOccurred = DateTime.Now;
        }

        public DateTime DateOccurred { get; private set; }

        public Paycheck Paycheck { get; set; }
    }
}
