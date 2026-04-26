using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NorthWind.Infrastructure.Persistance.Entities;

namespace NorthWind.Infrastructure.Persistance.Configurations;

internal class OrderSubtotalConfiguration : IEntityTypeConfiguration<OrderSubtotal>
{
    public void Configure(EntityTypeBuilder<OrderSubtotal> builder)
    {
        builder
            .HasNoKey()
            .ToView("Order Subtotals");

        builder.Property(e => e.OrderId).HasColumnName("OrderID");
        builder.Property(e => e.Subtotal).HasColumnType("money");
    }
}
