using Microsoft.EntityFrameworkCore;
using RamQuest.EntityFrameworkCore.Configuration;

namespace RamQuest.EntityFrameworkCore.Extensions
{
    static class ModelBuilderExtensions
    {
        public static void AddConfiguration<TEntity>(this ModelBuilder modelBuilder,
            DbEntityConfiguration<TEntity> entityConfiguration) where TEntity : class
        {
            modelBuilder.Entity<TEntity>(entityConfiguration.Configure);
        }
    }
}
