using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using RamQuest.Tetris.Data;

namespace RamQuest.Tetris.Data.Migrations
{
    [DbContext(typeof(TetrisContext))]
    partial class TetrisContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RamQuest.Tetris.Model.Command", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Assembly");

                    b.Property<string>("ContractType");

                    b.Property<int>("ModuleId");

                    b.Property<string>("Name");

                    b.Property<string>("Namespace");

                    b.Property<string>("Type");

                    b.Property<string>("Version");

                    b.HasKey("Id");

                    b.HasIndex("ModuleId");

                    b.ToTable("Commands");
                });

            modelBuilder.Entity("RamQuest.Tetris.Model.Module", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("RamQuest.Tetris.Model.Command", b =>
                {
                    b.HasOne("RamQuest.Tetris.Model.Module", "Module")
                        .WithMany()
                        .HasForeignKey("ModuleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
