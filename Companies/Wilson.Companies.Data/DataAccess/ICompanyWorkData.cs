using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wilson.Companies.Core.Entities;
using Wilson.Companies.Data.DataAccess.Repositories;

namespace Wilson.Companies.Data.DataAccess
{
    public interface ICompanyWorkData
    {        
        IRepository<ApplicationUser> Users { get; }        
        IRepository<Company> Companies { get; }
        IRepository<Employee> Employees { get; }
        IRepository<Inquiry> Inquiries { get; }
        IRepository<Settings> Settings { get; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> CompleteAsync();

        int Complete();
    }
}
