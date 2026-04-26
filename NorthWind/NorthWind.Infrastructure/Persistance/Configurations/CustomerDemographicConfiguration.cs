using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWind.Domain.Entities;

namespace NorthWind.Infrastructure.Persistance.Configurations;

public class CustomerDemographicConfiguration : IEntityTypeConfiguration<CustomerDemographic>
{
    public void Configure(EntityTypeBuilder<CustomerDemographic> builder)
    {
        builder.HasKey(e => e.CustomerTypeId).IsClustered(false);

        builder.Property(e => e.CustomerTypeId)
            .HasMaxLength(10)
            .IsFixedLength()
            .HasColumnName("CustomerTypeID");
        builder.Property(e => e.CustomerDesc).HasColumnType("ntext");
    }
}
