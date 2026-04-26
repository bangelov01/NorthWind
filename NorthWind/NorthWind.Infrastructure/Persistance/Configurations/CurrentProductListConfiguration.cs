using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWind.Domain.Entities;

namespace NorthWind.Infrastructure.Persistance.Configurations;

public class CurrentProductListConfiguration : IEntityTypeConfiguration<CurrentProductList>
{
    public void Configure(EntityTypeBuilder<CurrentProductList> builder)
    {
        builder
            .HasNoKey()
            .ToView("Current Product List");

        builder.Property(e => e.ProductId)
            .ValueGeneratedOnAdd()
            .HasColumnName("ProductID");
        builder.Property(e => e.ProductName).HasMaxLength(40);
    }
}
