using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RamQuest.IdentityServer.Model;

namespace RamQuest.IdentityServer.Data
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().ForSqlServerToTable("Roles", "dbo");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ForSqlServerToTable("RoleClaims", "dbo");
            modelBuilder.Entity<ApplicationUser>().ForSqlServerToTable("Users", "dbo");
            modelBuilder.Entity<IdentityUserClaim<string>>().ForSqlServerToTable("UserClaims", "dbo");
            modelBuilder.Entity<IdentityUserLogin<string>>().ForSqlServerToTable("UserLogins", "dbo");
            modelBuilder.Entity<IdentityUserRole<string>>().ForSqlServerToTable("UserRoles", "dbo");
            modelBuilder.Entity<IdentityUserToken<string>>().ForSqlServerToTable("UserTokens", "dbo");
        }
    }
}