using Microsoft.EntityFrameworkCore;
using RamQuest.Tetris.Data.Configuration;
using RamQuest.Tetris.Model;
using RamQuest.EntityFrameworkCore.Extensions;

namespace RamQuest.Tetris.Data
{
    public class TetrisContext : DbContext
    {
        public TetrisContext(DbContextOptions<TetrisContext> options) : base(options) { }

        #region DbSet Properties
        public DbSet<Module> Modules { get; set; }

        public DbSet<Command> Commands { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.AddConfiguration(new CommandConfiguration());
            modelBuilder.AddConfiguration(new ModuleConfiguration());
        }
    }
}
