namespace NorthWind.Services.Customer.Dto;

public class CustomerOverviewDto
{
    public required string CustomerId { get; init; }

    public string? ContactName { get; init; }

    public int OrderCount { get; init; }
}
