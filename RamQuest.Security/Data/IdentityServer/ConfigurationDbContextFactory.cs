using System.Reflection;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace RamQuest.Security.Data.IdentityServer
{
    public class ConfigurationDbContextFactory : IDbContextFactory<ConfigurationDbContext>
    {
        public ConfigurationDbContext Create(DbContextFactoryOptions options)
        {
            var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
            var builder = new DbContextOptionsBuilder<ConfigurationDbContext>();
            builder.UseSqlServer(Constants.DevConnectionString, mo => mo.MigrationsAssembly(migrationsAssembly));
            return new ConfigurationDbContext(builder.Options, new ConfigurationStoreOptions { DefaultSchema = "config" });
        }
    }
}
