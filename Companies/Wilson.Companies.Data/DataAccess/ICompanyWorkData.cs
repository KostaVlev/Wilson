using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wilson.Companies.Core.Aggregates;
using Wilson.Companies.Data.DataAccess.Repositories;
using Wilson.Companies.Core.Entities;

namespace Wilson.Companies.Data.DataAccess
{
    public interface ICompanyWorkData
    {
        IRepository<InquiryAggregate> Inquiries { get; }
        IRepository<User> Users { get; }
        IRepository<Settings> Settings { get; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> CompleteAsync();

        int Complete();
    }
}
