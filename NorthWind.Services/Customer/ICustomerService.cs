using NorthWind.Services.Customer.Dto;

namespace NorthWind.Services.Customer;

public interface ICustomerService
{
    Task<IReadOnlyCollection<CustomerOverviewDto>> GetCustomers(string? companyName);

    Task<CustomerDetailsDto?> GetCustomerDetails(string id);
}
