using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWind.Infrastructure.Persistance.Entities;

namespace NorthWind.Infrastructure.Persistance.Configurations;

internal class CustomerAndSuppliersByCityConfiguration : IEntityTypeConfiguration<CustomerAndSuppliersByCity>
{
    public void Configure(EntityTypeBuilder<CustomerAndSuppliersByCity> builder)
    {
        builder
            .HasNoKey()
            .ToView("Customer and Suppliers by City");

        builder.Property(e => e.City).HasMaxLength(15);
        builder.Property(e => e.CompanyName).HasMaxLength(40);
        builder.Property(e => e.ContactName).HasMaxLength(30);
        builder.Property(e => e.Relationship)
            .HasMaxLength(9)
            .IsUnicode(false);
    }
}
