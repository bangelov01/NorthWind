using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWind.Domain.Entities;

namespace NorthWind.Infrastructure.Persistance.Configurations;

public class ProductsByCategoryConfiguration : IEntityTypeConfiguration<ProductsByCategory>
{
    public void Configure(EntityTypeBuilder<ProductsByCategory> builder)
    {
        builder
            .HasNoKey()
            .ToView("Products by Category");

        builder.Property(e => e.CategoryName).HasMaxLength(15);
        builder.Property(e => e.ProductName).HasMaxLength(40);
        builder.Property(e => e.QuantityPerUnit).HasMaxLength(20);
    }
}
