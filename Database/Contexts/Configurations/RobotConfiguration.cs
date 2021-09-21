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

            builder.HasOne<Module>(m => m.LastPosition)
                .WithOne(r => r.Robot)
                .HasForeignKey<Module>(m => m.RobotId);

        }
    }
}
