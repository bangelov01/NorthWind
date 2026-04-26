namespace NorthWind.Infrastructure.Persistance.Entities;

public class OrderSubtotal
{
    public int OrderId { get; set; }

    public decimal? Subtotal { get; set; }
}
