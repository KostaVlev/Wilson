using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wilson.Accounting.Data.DataAccess.Repositories;
using Wilson.Accounting.Core.Entities;

namespace Wilson.Accounting.Data.DataAccess
{
    public interface IAccountingWorkData
    {
        IRepository<Company> Companies { get; }
        IRepository<Invoice> Invoices { get; }
        IRepository<Storehouse> Storehouses { get; }
        IRepository<Bill> Bills { get; }        

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> CompleteAsync();

        int Complete();
    }
}
