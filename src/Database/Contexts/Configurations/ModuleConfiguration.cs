using MartianRobots.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MartianRobots.Database.Contexts.Configurations
{
    public class ModuleConfiguration : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.Property(p => p.State)
                .IsRequired();
            builder.Property(p => p.X)
                .IsRequired(); 
            builder.Property(p => p.Y)
                .IsRequired();

            builder.HasOne<Grid>(m => m.Grid)
                .WithMany(g => g.Modules)
                .HasForeignKey(m => m.GridId)
                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
