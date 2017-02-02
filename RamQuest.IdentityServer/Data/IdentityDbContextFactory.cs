using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace RamQuest.IdentityServer.Data
{
    public class IdentityDbContextFactory : IDbContextFactory<IdentityContext>
    {
        public IdentityContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<IdentityContext>();
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RQ-Security;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new IdentityContext(builder.Options);
        }
    }
}
