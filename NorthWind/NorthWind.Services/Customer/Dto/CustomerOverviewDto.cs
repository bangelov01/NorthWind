namespace NorthWind.Services.Customer.Dto;

public record CustomerOverviewDto
{
    public required string CustomerId { get; init; }

    public string? CompanyName { get; init; }

    public int OrderCount { get; init; }
}
