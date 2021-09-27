using MartianRobots.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartianRobots.Database.Contexts.Configurations
{
    public class RobotConfiguration : IEntityTypeConfiguration<Robot>
    {
        public void Configure(EntityTypeBuilder<Robot> builder)
        {
            builder.Property(p => p.Path)
                .IsRequired();
            builder.Property(p => p.Lost)
                .IsRequired();
            builder.Property(p => p.NumberOfMoves)
                .IsRequired();

            builder.HasOne<Grid>(r => r.Grid)
                 .WithMany(g => g.Robots)
                 .HasForeignKey(r => r.GridId);

            builder.HasOne<Module>(r => r.LastPosition)
               .WithOne(m => m.Robot)
               .HasForeignKey<Module>(m => m.RobotId);
        }
    }
}
