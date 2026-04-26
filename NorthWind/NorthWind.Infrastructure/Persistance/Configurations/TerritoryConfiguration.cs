using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWind.Infrastructure.Persistance.Entities;

namespace NorthWind.Infrastructure.Persistance.Configurations;

internal class TerritoryConfiguration : IEntityTypeConfiguration<Territory>
{
    public void Configure(EntityTypeBuilder<Territory> builder)
    {
        builder.HasKey(e => e.TerritoryId).IsClustered(false);

        builder.Property(e => e.TerritoryId)
            .HasMaxLength(20)
            .HasColumnName("TerritoryID");
        builder.Property(e => e.RegionId).HasColumnName("RegionID");
        builder.Property(e => e.TerritoryDescription)
            .HasMaxLength(50)
            .IsFixedLength();

        builder.HasOne(d => d.Region).WithMany(p => p.Territories)
            .HasForeignKey(d => d.RegionId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Territories_Region");
    }
}
