using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wilson.Scheduler.Data.DataAccess.Repositories;
using Wilson.Scheduler.Core.Entities;

namespace Wilson.Scheduler.Data.DataAccess
{
    public class SchedulerWorkData : ISchedulerWorkData
    {
        private readonly DbContext dbContext;

        private readonly IDictionary<Type, object> repositories;

        public SchedulerWorkData(DbContextOptions<SchedulerDbContext> options)
            : this(new SchedulerDbContext(options))
        {
        }

        public SchedulerWorkData(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<Employee> Employees => this.GetRepository<Employee>();

        public IRepository<Schedule> Schedules => this.GetRepository<Schedule>();

        public IRepository<Project> Projects => this.GetRepository<Project>();

        public IRepository<PayRate> PayRates => this.GetRepository<PayRate>();

        public int Complete()
        {
            return this.dbContext.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await this.dbContext.SaveChangesAsync();
        }

        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return this.dbContext.Set<TEntity>();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(Repository<T>);
                this.repositories.Add(
                    typeof(T),
                    Activator.CreateInstance(type, this));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }
    }
}
