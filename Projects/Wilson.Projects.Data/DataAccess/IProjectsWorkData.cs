using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wilson.Projects.Data.DataAccess.Repositories;
using Wilson.Projects.Core.Entities;

namespace Wilson.Projects.Data.DataAccess
{
    public interface IProjectsWorkData
    {
        IRepository<Employee> Employees { get; }
        IRepository<Company> Companies { get; }
        IRepository<Project> Projects { get; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> CompleteAsync();

        int Complete();
    }
}
