using Microsoft.EntityFrameworkCore;
using NorthWind.Infrastructure.Persistance.Generated;
using NorthWind.Services.Customer.Dto;

namespace NorthWind.Services.Customer;

public class CustomerService(NorthWindDbContext dbContext) : ICustomerService
{
    public async Task<IList<CustomerOverviewDto>> GetCustomers(string? customerName)
    {
        IQueryable<Infrastructure.Persistance.Generated.Entities.Customer> customersQuery = dbContext.Customers.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(customerName))
        {
            customersQuery = customersQuery.Where(customer => customer.ContactName.Contains(customerName));
        }

        return await customersQuery.Select(customer => new CustomerOverviewDto
            {
                CustomerId = customer.CustomerId,
                ContactName = customer.ContactName,
                OrderCount = customer.Orders.Count
            }).ToListAsync();
    }

    public async Task<CustomerDetailsDto?> GetCustomerDetails(string id)
    {
        return await dbContext.Customers
                   .AsNoTracking()
                   .Where(customer => customer.CustomerId == id)
                   .Select(customer => new CustomerDetailsDto
                       {
                           CustomerId = customer.CustomerId,
                           CompanyName = customer.CompanyName,
                           ContactName = customer.ContactName,
                           ContactTitle = customer.ContactTitle,
                           Address = customer.Address,
                           City = customer.City,
                           Region = customer.Region,
                           PostalCode = customer.PostalCode,
                           Country = customer.Country,
                           Phone = customer.Phone,
                           Fax = customer.Fax,
                           Orders = customer.Orders
                               .Select(order => new OrderSummaryDto
                                   {
                                       OrderId = order.OrderId,
                                       TotalValue = Math.Round(
                                           order.OrderDetails.Sum(orderDetail =>
                                               orderDetail.UnitPrice * orderDetail.Quantity * (decimal)(1 - orderDetail.Discount)),
                                           2),
                                       ProductCount = order.OrderDetails.Count
                                   })
                               .ToList()
                       })
                   .FirstOrDefaultAsync();
    }
}
