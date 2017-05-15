using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Wilson.Accounting.Core.Entities;
using Wilson.Companies.Data;
using Wilson.Projects.Data;
using Wilson.Web.Events.Interfaces;

namespace Wilson.Web.Events.Handlers
{
    public class CreatedCompanyHandler : Handler
    {
        public CreatedCompanyHandler(IServiceProvider serviceProvider) 
            : base(serviceProvider)
        {
        }

        public async override Task Handle(IDomainEvent args)
        {
            var eventArgs = args as CompanyCreated;
            if (eventArgs == null)
            {
                throw new InvalidCastException();
            }
            
            var companyDbContext = this.ServiceProvider.GetService<CompanyDbContext>();
            var projectDbContext = this.ServiceProvider.GetService<ProjectsDbContext>();

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Company, Companies.Core.Entities.Company>();
                cfg.CreateMap<Company, Projects.Core.Entities.Company>();                
            });

            var companyCompany = Mapper.Map<Company, Companies.Core.Entities.Company>(eventArgs.Company);
            companyCompany.ChangeShippingAddress(companyCompany.GetAddress());

            var projectsCompany = Mapper.Map<Company, Projects.Core.Entities.Company>(eventArgs.Company);

            await companyDbContext.Set<Companies.Core.Entities.Company>().AddAsync(companyCompany);
            await projectDbContext.Set<Projects.Core.Entities.Company>().AddAsync(projectsCompany);

            await companyDbContext.SaveChangesAsync();
            await projectDbContext.SaveChangesAsync();
        }
    }
}
