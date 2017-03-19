using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wilson.Companies.Core.Entities;
using Wilson.Companies.Data.DataAccess.Repositories;

namespace Wilson.Companies.Data.DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ICompanyWorkData Context;
        private DbSet<TEntity> entities;

        public Repository(ICompanyWorkData context)
        {
            this.Context = context;
            this.entities = this.Context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            this.entities.Add(entity);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.entities.Where(predicate).ToListAsync();
        }

        public void Remove(TEntity entity)
        {
            this.entities.Remove(entity);
        }

        public void Remove(Expression<Func<TEntity, bool>> predicate)
        {
            this.entities.RemoveRange(this.entities.Where(predicate).ToList());
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.entities.SingleOrDefaultAsync(predicate);
        }
    }
}
