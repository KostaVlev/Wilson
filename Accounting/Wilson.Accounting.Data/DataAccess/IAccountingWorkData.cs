using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wilson.Accounting.Data.DataAccess.Repositories;

namespace Wilson.Accounting.Data.DataAccess
{
    public interface IAccountingWorkData
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task CompleteAsync();

        int Complete();
    }
}
