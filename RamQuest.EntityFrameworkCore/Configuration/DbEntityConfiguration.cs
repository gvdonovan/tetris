using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RamQuest.EntityFrameworkCore.Configuration
{
    public abstract class DbEntityConfiguration<TEntity> where TEntity : class
    {
        public abstract void Configure(EntityTypeBuilder<TEntity> entity);
    }
}
