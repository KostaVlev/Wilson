using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Wilson.Accounting.Data;
using Wilson.Scheduler.Core.Entities;
using Wilson.Web.Events.Interfaces;
using System.Collections.Generic;

namespace Wilson.Web.Events.Handlers
{
    public class CreatedPaycheksHandler : Handler
    {
        public CreatedPaycheksHandler(IServiceProvider serviceProvider) 
            : base(serviceProvider)
        {
        }

        public override async Task Handle(IDomainEvent args)
        {
            var eventArgs = args as PaychecksCreated;
            if (eventArgs == null)
            {
                throw new InvalidCastException();
            }

            var accDbContext = this.ServiceProvider.GetService<AccountingDbContext>();

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Paycheck, Accounting.Core.Entities.Paycheck>().ForMember(x => x.Employee, opt => opt.Ignore());
            });

            var accPaychecks = Mapper.Map<IEnumerable<Paycheck>, IEnumerable<Accounting.Core.Entities.Paycheck>>(eventArgs.Paychecks);

            await accDbContext.Set<Accounting.Core.Entities.Paycheck>().AddRangeAsync(accPaychecks);
            await accDbContext.SaveChangesAsync();
        }
    }
}
