using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Wilson.Accounting.Data;
using Wilson.Companies.Data;
using Wilson.Projects.Core.Entities;
using Wilson.Scheduler.Data;
using Wilson.Web.Events.Interfaces;

namespace Wilson.Web.Events.Handlers
{
    public class CteatedProjectHandler : Handler
    {
        public CteatedProjectHandler(IServiceProvider serviceProvider) 
            : base(serviceProvider)
        {
        }

        public override async Task Handle(IDomainEvent args)
        {
            var eventArgs = args as ProjectCreated;
            if (eventArgs == null)
            {
                throw new InvalidCastException();
            }

            var accDbContext = this.ServiceProvider.GetService<AccountingDbContext>();
            var schedulerDbContext = this.ServiceProvider.GetService<SchedulerDbContext>();
            var companyDbContext = this.ServiceProvider.GetService<CompanyDbContext>();

            Mapper.Initialize(cfg =>
            {
                // Companies Maps
                cfg.CreateMap<Project, Companies.Core.Entities.Project>();
                cfg.CreateMap<Company, Companies.Core.Entities.Company>();

                // Accounting Maps
                cfg.CreateMap<Project, Accounting.Core.Entities.Project>();
                cfg.CreateMap<Company, Accounting.Core.Entities.Company>();

                // Scheduler Maps
                cfg.CreateMap<Project, Scheduler.Core.Entities.Project>();
            });            
            
            var companyProject = Mapper.Map<Project, Companies.Core.Entities.Project>(eventArgs.Project);
            var accProject = Mapper.Map<Project, Accounting.Core.Entities.Project>(eventArgs.Project);
            var schedulerProject = Mapper.Map<Project, Scheduler.Core.Entities.Project>(eventArgs.Project);

            await companyDbContext.Set<Companies.Core.Entities.Project>().AddAsync(companyProject);
            await accDbContext.Set<Accounting.Core.Entities.Project>().AddAsync(accProject);
            await schedulerDbContext.Set<Scheduler.Core.Entities.Project>().AddAsync(schedulerProject);

            await accDbContext.SaveChangesAsync();
            await schedulerDbContext.SaveChangesAsync();
            await companyDbContext.SaveChangesAsync();
        }
    }
}
