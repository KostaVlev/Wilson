using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Wilson.Projects.Data.DataAccess.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);

        void Remove(TEntity entity);

        void Remove(Expression<Func<TEntity, bool>> predicate);
    }
}
