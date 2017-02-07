using System.Reflection;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace RamQuest.Security.Data.IdentityServer
{
    public class PersistedGrantDbContextFactory : IDbContextFactory<PersistedGrantDbContext>
    {
        public PersistedGrantDbContext Create(DbContextFactoryOptions options)
        {
            var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
            var builder = new DbContextOptionsBuilder<PersistedGrantDbContext>();
            builder.UseSqlServer(Constants.DevConnectionString, mo => mo.MigrationsAssembly(migrationsAssembly));            
            return new PersistedGrantDbContext(builder.Options, new OperationalStoreOptions { DefaultSchema = "oper" });
        }
    }
}
