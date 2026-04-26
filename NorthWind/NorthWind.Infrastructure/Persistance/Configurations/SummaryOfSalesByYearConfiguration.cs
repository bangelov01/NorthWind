using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWind.Domain.Entities;

namespace NorthWind.Infrastructure.Persistance.Configurations;

public class SummaryOfSalesByYearConfiguration : IEntityTypeConfiguration<SummaryOfSalesByYear>
{
    public void Configure(EntityTypeBuilder<SummaryOfSalesByYear> builder)
    {
        builder
            .HasNoKey()
            .ToView("Summary of Sales by Year");

        builder.Property(e => e.OrderId).HasColumnName("OrderID");
        builder.Property(e => e.ShippedDate).HasColumnType("datetime");
        builder.Property(e => e.Subtotal).HasColumnType("money");
    }
}
