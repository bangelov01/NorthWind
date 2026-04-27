namespace NorthWind.Services.Customer.Dto;

public record OrderSummaryDto
{
    public required int OrderId { get; init; }

    public decimal TotalValue { get; init; }

    public int ProductCount { get; init; }
}
