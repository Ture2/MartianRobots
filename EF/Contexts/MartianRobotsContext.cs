using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MartianRobots.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace MartianRobots.EF.Contexts
{
    public class MartianRobotsContext : DbContext
    {

        public MartianRobotsContext(DbContextOptions<MartianRobotsContext> options) : base(options)
        {
           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=test;Trusted_Connection=True;");
        }

        //entities
        public DbSet<Robot> Robots;
        public DbSet<Module> Modules;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}
