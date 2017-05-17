using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Wilson.Accounting.Data;
using Wilson.Companies.Core.Entities;
using Wilson.Projects.Data;
using Wilson.Scheduler.Data;
using Wilson.Web.Events.Interfaces;

namespace Wilson.Web.Events.Handlers
{
    public class EmployeeCreatedHandler : Handler
    {
        public EmployeeCreatedHandler(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
        }

        public async override Task Handle(IDomainEvent args)
        {
            var eventArgs = args as EmployeeCreated;
            if (eventArgs == null)
            {
                throw new InvalidCastException();
            }

            var accountingDbContext = this.ServiceProvider.GetService<AccountingDbContext>();
            var projectDbContext = this.ServiceProvider.GetService<ProjectsDbContext>();
            var schedulerDbContext = this.ServiceProvider.GetService<SchedulerDbContext>();
            var payRate = schedulerDbContext.PayRates.FirstOrDefault();

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Employee, Accounting.Core.Entities.Employee>()
                    .ForMember(x => x.Paycheks, opt => opt.Ignore())
                    .ForMember(x => x.Company, opt => opt.Ignore())
                    .ForSourceMember(x => x.Company, opt => opt.Ignore())
                    .ForSourceMember(x => x.InfoRequests, opt => opt.Ignore());

                cfg.CreateMap<Employee, Projects.Core.Entities.Employee>()
                    .ForMember(x => x.Projects, opt => opt.Ignore())
                    .ForSourceMember(x => x.Company, opt => opt.Ignore())
                    .ForSourceMember(x => x.InfoRequests, opt => opt.Ignore());

                cfg.CreateMap<Employee, Scheduler.Core.Entities.Employee>()
                    .ForMember(x => x.Schedules, opt => opt.Ignore())
                    .ForMember(x => x.Paychecks, opt => opt.Ignore())
                    .ForMember(x => x.PayRate, opt => opt.Ignore())
                    .ForSourceMember(x => x.Company, opt => opt.Ignore())
                    .ForSourceMember(x => x.InfoRequests, opt => opt.Ignore());
            });

            if (eventArgs.Employees != null)
            {
                var accountingEmployees = Mapper.Map<IEnumerable<Employee>, IEnumerable<Accounting.Core.Entities.Employee>>(eventArgs.Employees);
                var projectsEmployees = Mapper.Map<IEnumerable<Employee>, IEnumerable<Projects.Core.Entities.Employee>>(eventArgs.Employees);
                var schedulerEmployees = Mapper.Map<IEnumerable<Employee>, IEnumerable<Scheduler.Core.Entities.Employee>>(eventArgs.Employees);
                foreach (var employee in schedulerEmployees)
                {
                    employee.ApplayPayRate(payRate);
                }

                await accountingDbContext.Set<Accounting.Core.Entities.Employee>().AddRangeAsync(accountingEmployees);
                await projectDbContext.Set<Projects.Core.Entities.Employee>().AddRangeAsync(projectsEmployees);
                await schedulerDbContext.Set<Scheduler.Core.Entities.Employee>().AddRangeAsync(schedulerEmployees);
            }

            if (eventArgs.Employee != null)
            {
                var accountingEmployee = Mapper.Map<Employee, Accounting.Core.Entities.Employee>(eventArgs.Employee);
                var projectsEmployee = Mapper.Map<Employee, Projects.Core.Entities.Employee>(eventArgs.Employee);
                var schedulerEmployee = Mapper.Map<Employee, Scheduler.Core.Entities.Employee>(eventArgs.Employee);
                schedulerEmployee.ApplayPayRate(payRate);

                await accountingDbContext.Set<Accounting.Core.Entities.Employee>().AddAsync(accountingEmployee);
                await projectDbContext.Set<Projects.Core.Entities.Employee>().AddAsync(projectsEmployee);
                await schedulerDbContext.Set<Scheduler.Core.Entities.Employee>().AddAsync(schedulerEmployee);
            }

            await accountingDbContext.SaveChangesAsync();
            await projectDbContext.SaveChangesAsync();
            await schedulerDbContext.SaveChangesAsync();
        }
    }
}
