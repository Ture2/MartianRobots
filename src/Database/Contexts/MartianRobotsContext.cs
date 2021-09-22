using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MartianRobots.Database.Contexts.Configurations;
using MartianRobots.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace MartianRobots.Database.Contexts
{
    public class MartianRobotsContext : DbContext
    {
        //entities
        public virtual DbSet<Robot> Robots { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<Grid> Grids { get; set; }

        public MartianRobotsContext(DbContextOptions<MartianRobotsContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ModuleConfiguration());
            modelBuilder.ApplyConfiguration(new RobotConfiguration());

        }

    }
}
