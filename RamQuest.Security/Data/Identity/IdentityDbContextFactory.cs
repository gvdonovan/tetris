using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace RamQuest.Security.Data.Identity
{
    public class IdentityDbContextFactory : IDbContextFactory<IdentityDbContext>
    {
        public IdentityDbContext Create(DbContextFactoryOptions options)
        {            
            var builder = new DbContextOptionsBuilder<IdentityDbContext>();
            builder.UseSqlServer(Constants.DevConnectionString);
            return new IdentityDbContext(builder.Options);
        }
    }
}
