using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Wilson.Accounting.Core.Entities;
using Wilson.Companies.Data;
using Wilson.Projects.Data;
using Wilson.Web.Events.Interfaces;

namespace Wilson.Web.Events.Handlers
{
    public class CompanyCreatedHandler : Handler
    {
        public CompanyCreatedHandler(IServiceProvider serviceProvider)
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
                cfg.CreateMap<Company, Companies.Core.Entities.Company>()
                    .ForMember(x => x.Employees, opt => opt.Ignore())
                    .ForMember(x => x.Projects, opt => opt.Ignore())
                    .ForSourceMember(x => x.Employees, opt => opt.Ignore())
                    .ForSourceMember(x => x.SaleInvoices, opt => opt.Ignore())
                    .ForSourceMember(x => x.BuyInvoices, opt => opt.Ignore());

                cfg.CreateMap<Company, Projects.Core.Entities.Company>()
                    .ForSourceMember(x => x.Employees, opt => opt.Ignore())
                    .ForSourceMember(x => x.SaleInvoices, opt => opt.Ignore())
                    .ForSourceMember(x => x.BuyInvoices, opt => opt.Ignore());
            });

            if (eventArgs.Companies != null)
            {
                var companyCompanies = Mapper.Map<IEnumerable<Company>, IEnumerable<Companies.Core.Entities.Company>>(eventArgs.Companies);
                foreach (var company in companyCompanies)
                {
                    company.ChangeShippingAddress(company.GetAddress());
                }

                var projectsCompanies = Mapper.Map<IEnumerable<Company>, IEnumerable<Projects.Core.Entities.Company>>(eventArgs.Companies);

                await companyDbContext.Set<Companies.Core.Entities.Company>().AddRangeAsync(companyCompanies);
                await projectDbContext.Set<Projects.Core.Entities.Company>().AddRangeAsync(projectsCompanies);
            }

            if (eventArgs.Company != null)
            {
                var companyCompany = Mapper.Map<Company, Companies.Core.Entities.Company>(eventArgs.Company);
                companyCompany.ChangeShippingAddress(companyCompany.GetAddress());

                var projectsCompany = Mapper.Map<Company, Projects.Core.Entities.Company>(eventArgs.Company);

                await companyDbContext.Set<Companies.Core.Entities.Company>().AddAsync(companyCompany);
                await projectDbContext.Set<Projects.Core.Entities.Company>().AddAsync(projectsCompany);
            }
            
            await companyDbContext.SaveChangesAsync();
            await projectDbContext.SaveChangesAsync();
        }
    }
}
