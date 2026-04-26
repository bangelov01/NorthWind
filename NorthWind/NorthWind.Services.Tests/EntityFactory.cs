using NorthWind.Infrastructure.Persistance.Generated;
using NorthWind.Infrastructure.Persistance.Generated.Entities;

namespace NorthWind.Services.Tests;

internal class EntityFactory(NorthWindDbContext dbContext)
{
    public async Task<Infrastructure.Persistance.Generated.Entities.Customer> GetCustomer(string customerId, string companyName)
    {
        Infrastructure.Persistance.Generated.Entities.Customer customer = new()
            {
                CustomerId = customerId,
                CompanyName = companyName,
            };

        await dbContext.Customers.AddAsync(customer);

        return customer;
    }

    public async Task<Order> GetOrder()
    {
        Order order = new();

        await dbContext.Orders.AddAsync(order);

        return order;
    }

    public async Task<OrderDetail> GetOrderDetail(Order order, Product product, decimal unitPrice, short quantity, float discount)
    {
        OrderDetail orderDetail = new()
        {
            Order = order,
            Product = product,
            UnitPrice = unitPrice,
            Quantity = quantity,
            Discount = discount,
        };

        await dbContext.OrderDetails.AddAsync(orderDetail);

        return orderDetail;
    }

    public async Task<Product> GetProduct(string productName, bool discontinued)
    {
        Product product = new()
        {
            ProductName = productName,
            Discontinued = discontinued,
        };

        await dbContext.Products.AddAsync(product);

        return product;
    }

}
