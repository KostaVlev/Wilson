using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wilson.Scheduler.Core.Aggregates;
using Wilson.Scheduler.Data.DataAccess.Repositories;

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

        public IRepository<EmployeeAggregate> Employees => this.GetRepository<EmployeeAggregate>();

        public int Complete()
        {
            return this.dbContext.SaveChanges();
        }

        public async Task CompleteAsync()
        {
            await this.dbContext.SaveChangesAsync();
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
