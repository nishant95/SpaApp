using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SpaData
{
    internal abstract class EntityTypeConfiguration<TEntity> where TEntity : class
    {
        public abstract void Map(EntityTypeBuilder<TEntity> builder);
    }
}
