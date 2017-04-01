using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wilson.Companies.Core.Entities;
using Wilson.Companies.Data.DataAccess.Repositories;

namespace Wilson.Companies.Data.DataAccess
{
    public interface ICompanyWorkData
    {
        IRepository<Inquiry> Inquiries { get; }
        IRepository<InquiryEmployee> InquiryEmployee { get; }
        IRepository<User> Users { get; }
        IRepository<Settings> Settings { get; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> CompleteAsync();

        int Complete();
    }
}
