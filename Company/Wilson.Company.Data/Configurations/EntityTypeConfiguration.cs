using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wilson.Company.Data.Configurations
{
    public abstract class EntityTypeConfiguration<TEntity> : IEntityTypeConfiguration where TEntity : class
    {
        public abstract void Map(EntityTypeBuilder<TEntity> builder);
    }
}
