using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wilson.Scheduler.Data.DataAccess.Repositories;
using Wilson.Scheduler.Core.Entities;

namespace Wilson.Scheduler.Data.DataAccess
{
    public interface ISchedulerWorkData
    {
        IRepository<Employee> Employees { get; }
        IRepository<Schedule> Schedules { get; }
        IRepository<Project> Projects { get; }
        IRepository<PayRate> PayRates { get; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> CompleteAsync();

        int Complete();
    }
}
