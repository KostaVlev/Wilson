using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wilson.Projects.Core.Aggregates;
using Wilson.Projects.Data.DataAccess.Repositories;

namespace Wilson.Projects.Data.DataAccess
{
    public interface IProjectsWorkData
    {
        IRepository<ProjectAggregate> Projects { get; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task CompleteAsync();

        int Complete();
    }
}
