using NorthWind.Services.Customer.Dto;

namespace NorthWind.Services.Customer;

public interface ICustomerService
{
    Task<IList<CustomerOverviewDto>> GetCustomers(string? contactName);

    Task<CustomerDetailsDto?> GetCustomerDetails(string id);
}
