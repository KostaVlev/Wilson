using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wilson.Scheduler.Core.Aggregates;
using Wilson.Scheduler.Data.DataAccess.Repositories;

namespace Wilson.Scheduler.Data.DataAccess
{
    public interface ISchedulerWorkData
    {
        IRepository<EmployeeAggregate> Employees { get; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task CompleteAsync();

        int Complete();
    }
}
