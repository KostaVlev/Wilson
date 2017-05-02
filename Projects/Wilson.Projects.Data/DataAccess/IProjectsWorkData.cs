using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Wilson.Projects.Data.DataAccess
{
    public interface IProjectsWorkData
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task CompleteAsync();

        int Complete();
    }
}
