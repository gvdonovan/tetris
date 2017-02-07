using System.Reflection;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace RamQuest.Security.Data
{
    public class IdentityDbContextFactory : IDbContextFactory<IdentityContext>
    {
        public IdentityContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<IdentityContext>();
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RQ-Security3;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new IdentityContext(builder.Options);
        }
    }

    public class PersistedGrantDbContextFactory : IDbContextFactory<PersistedGrantDbContext>
    {
        public PersistedGrantDbContext Create(DbContextFactoryOptions options)
        {
            var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
            var builder = new DbContextOptionsBuilder<PersistedGrantDbContext>();

            builder
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RQ-Security3;Trusted_Connection=True;MultipleActiveResultSets=true",
                mo => mo.MigrationsAssembly(migrationsAssembly));            
                
            return new PersistedGrantDbContext(builder.Options, new OperationalStoreOptions {DefaultSchema = "oper"});
        }
    }

    public class ConfigurationDbContextFactory : IDbContextFactory<ConfigurationDbContext>
    {
        public ConfigurationDbContext Create(DbContextFactoryOptions options)
        {
            var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
            var builder = new DbContextOptionsBuilder<ConfigurationDbContext>();

            builder
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RQ-Security3;Trusted_Connection=True;MultipleActiveResultSets=true",
                mo => mo.MigrationsAssembly(migrationsAssembly));

            return new ConfigurationDbContext(builder.Options, new ConfigurationStoreOptions { DefaultSchema = "config" });
        }
    }
}
