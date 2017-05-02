using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wilson.Projects.Data.DataAccess.Repositories;

namespace Wilson.Projects.Data.DataAccess
{
    public class ProjectsWorkData : IProjectsWorkData
    {
        private readonly DbContext dbContext;

        private readonly IDictionary<Type, object> repositories;

        public ProjectsWorkData(DbContextOptions<ProjectsDbContext> options)
            : this(new ProjectsDbContext(options))
        {
        }

        public ProjectsWorkData(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.repositories = new Dictionary<Type, object>();
        }
        
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
