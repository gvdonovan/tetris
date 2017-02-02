using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace RamQuest.Tetris.Data
{
    public class TetrisDbContextFactory : IDbContextFactory<TetrisContext>
    {
        public TetrisContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<TetrisContext>();
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=RQ-Tetris;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new TetrisContext(builder.Options);
        }
    }
}