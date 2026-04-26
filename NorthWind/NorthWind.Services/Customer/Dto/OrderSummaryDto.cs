namespace NorthWind.Services.Customer.Dto;

public class OrderSummaryDto
{
    public required int OrderId { get; init; }

    public decimal TotalValue { get; init; }

    public int ProductCount { get; init; }
}
