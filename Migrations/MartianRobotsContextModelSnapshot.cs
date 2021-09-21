﻿// <auto-generated />
using System;
using MartianRobots.EF.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MartianRobots.Migrations
{
    [DbContext(typeof(MartianRobotsContext))]
    partial class MartianRobotsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MartianRobots.EF.Entities.Grid", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Planet")
                        .HasColumnType("int");

                    b.Property<int>("XAxisLength")
                        .HasColumnType("int");

                    b.Property<int>("YAxisLength")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Grids");
                });

            modelBuilder.Entity("MartianRobots.EF.Entities.Module", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GridId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RobotId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<int>("X")
                        .HasColumnType("int");

                    b.Property<int>("Y")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GridId");

                    b.HasIndex("RobotId")
                        .IsUnique();

                    b.ToTable("Modules");
                });

            modelBuilder.Entity("MartianRobots.EF.Entities.Robot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Lost")
                        .HasColumnType("bit");

                    b.Property<int>("NumberOfMoves")
                        .HasColumnType("int");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Robots");
                });

            modelBuilder.Entity("MartianRobots.EF.Entities.Module", b =>
                {
                    b.HasOne("MartianRobots.EF.Entities.Grid", "Grid")
                        .WithMany("Modules")
                        .HasForeignKey("GridId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MartianRobots.EF.Entities.Robot", "Robot")
                        .WithOne("LastPosition")
                        .HasForeignKey("MartianRobots.EF.Entities.Module", "RobotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Grid");

                    b.Navigation("Robot");
                });

            modelBuilder.Entity("MartianRobots.EF.Entities.Grid", b =>
                {
                    b.Navigation("Modules");
                });

            modelBuilder.Entity("MartianRobots.EF.Entities.Robot", b =>
                {
                    b.Navigation("LastPosition");
                });
#pragma warning restore 612, 618
        }
    }
}
