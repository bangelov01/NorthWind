namespace NorthWind.Services.Customer.Dto;

public class CustomerDetailsDto
{
    public required string CustomerId { get; init; }

    public required string CompanyName { get; init; }

    public string? ContactName { get; init; }

    public string? ContactTitle { get; init; }

    public string? Address { get; init; }

    public string? City { get; init; }

    public string? Region { get; init; }

    public string? PostalCode { get; init; }

    public string? Country { get; init; }

    public string? Phone { get; init; }

    public string? Fax { get; init; }

    public required IReadOnlyCollection<OrderSummaryDto> Orders { get; init; }
}
