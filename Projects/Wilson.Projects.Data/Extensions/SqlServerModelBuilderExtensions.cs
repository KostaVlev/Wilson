using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wilson.Projects.Data.Extensions
{
    public static class SqlServerModelBuilderExtensions
    {
        public static PropertyBuilder<decimal> HasPrecision(this PropertyBuilder<decimal> builder, int precision, int scale)
        {
            return builder.HasColumnType($"decimal({precision},{scale})");
        }
    }
}
