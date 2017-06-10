using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wilson.Accounting.Data.DataAccess.Repositories;
using Wilson.Accounting.Core.Entities;

namespace Wilson.Accounting.Data.DataAccess
{
    public class AccountingWorkData : IAccountingWorkData
    {
        private readonly DbContext dbContext;

        private readonly IDictionary<Type, object> repositories;

        public AccountingWorkData(DbContextOptions<AccountingDbContext> options)
            : this(new AccountingDbContext(options))
        {
        }

        public AccountingWorkData(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<Employee> Employees => this.GetRepository<Employee>();
        public IRepository<Company> Companies => this.GetRepository<Company>();
        public IRepository<Invoice> Invoices => this.GetRepository<Invoice>();
        public IRepository<Storehouse> Storehouses => this.GetRepository<Storehouse>();
        public IRepository<Bill> Bills => this.GetRepository<Bill>();

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
