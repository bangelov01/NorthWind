using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWind.Infrastructure.Persistance.Entities;

namespace NorthWind.Infrastructure.Persistance.Configurations;

internal class SummaryOfSalesByQuarterConfiguration : IEntityTypeConfiguration<SummaryOfSalesByQuarter>
{
    public void Configure(EntityTypeBuilder<SummaryOfSalesByQuarter> builder)
    {
        builder
            .HasNoKey()
            .ToView("Summary of Sales by Quarter");

        builder.Property(e => e.OrderId).HasColumnName("OrderID");
        builder.Property(e => e.ShippedDate).HasColumnType("datetime");
        builder.Property(e => e.Subtotal).HasColumnType("money");
    }
}
