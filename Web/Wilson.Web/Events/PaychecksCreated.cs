using System;
using System.Collections.Generic;
using Wilson.Scheduler.Core.Entities;
using Wilson.Web.Events.Interfaces;

namespace Wilson.Web.Events
{
    public class PaychecksCreated : IDomainEvent
    {
        public PaychecksCreated(IEnumerable<Paycheck> paychecks)
        {
            this.Paychecks = paychecks;
            this.DateOccurred = DateTime.Now;
        }

        public IEnumerable<Paycheck> Paychecks { get; set; }

        public DateTime DateOccurred { get; private set; }
    }
}
