using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Wilson.Projects.Data.DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly IProjectsWorkData Context;
        private DbSet<TEntity> entities;

        public Repository(IProjectsWorkData context)
        {
            this.Context = context;
            this.entities = this.Context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await this.entities.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Func<IQueryable<TEntity>, IQueryable<TEntity>> func)
        {
            return await func(this.entities).ToArrayAsync();
        }

        public async Task<TEntity> GetById(string id)
        {
            return await this.entities.FindAsync(id);
        }

        public void Add(TEntity entity)
        {
            this.entities.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            this.entities.AddRange(entities);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.entities.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> func)
        {
            return await func(this.entities.Where(predicate)).ToListAsync();
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

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IQueryable<TEntity>> func)
        {
            return await func(this.entities.Where(predicate)).SingleOrDefaultAsync();
        }
    }
}
