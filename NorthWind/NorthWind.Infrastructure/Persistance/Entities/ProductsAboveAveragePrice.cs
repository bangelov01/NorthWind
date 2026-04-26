namespace NorthWind.Infrastructure.Persistance.Entities;

public class ProductsAboveAveragePrice
{
    public string ProductName { get; set; } = null!;

    public decimal? UnitPrice { get; set; }
}
