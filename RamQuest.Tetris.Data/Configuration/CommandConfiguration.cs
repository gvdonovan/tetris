using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RamQuest.EntityFrameworkCore.Configuration;
using RamQuest.Tetris.Model;

namespace RamQuest.Tetris.Data.Configuration
{
    public class CommandConfiguration : DbEntityConfiguration<Command>
    {
        public override void Configure(EntityTypeBuilder<Command> entity)
        {            
        }
    }
}
