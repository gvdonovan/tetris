using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RamQuest.Security.Model;

namespace RamQuest.Security.Data.Identity
{
    public class IdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
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
