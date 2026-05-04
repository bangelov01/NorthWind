using NorthWind.Infrastructure.Persistance.Generated.Entities;
using NorthWind.Services.Customer;
using NorthWind.Services.Customer.Dto;

namespace NorthWind.Services.Tests.Service.CustomerServiceTest;

[TestFixture]
internal class GetCustomerDetails : DatabaseTestBase
{
    private CustomerService _CustomerService;

    [SetUp]
    public void SetUp()
    {
        _CustomerService = new CustomerService(_DbContext);
    }

    [Test]
    public async Task When_CustomerDoesNotExist()
    {
        // Arrange
        await _EntityFactory.GetCustomer("AAAAA", "testCompanyName");

        await _DbContext.SaveChangesAsync();

        // Act
        CustomerDetailsDto? result = await _CustomerService.GetCustomerDetails("ALFKI");

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task When_CustomerExists()
    {
        // Arrange
        Infrastructure.Persistance.Generated.Entities.Customer customer = await _EntityFactory.GetCustomer("AAAAA", "testCompanyName");

        Order order = await _EntityFactory.GetOrder();
        order.Customer = customer;
        await _EntityFactory.GetOrderDetail(order, await _EntityFactory.GetProduct("firstTestProductName", true), 200m, 2, 0.2f);
        await _EntityFactory.GetOrderDetail(order, await _EntityFactory.GetProduct("secondTestProductName", true), 300m, 5, 0.1f);

        Order secondOrder = await _EntityFactory.GetOrder();
        secondOrder.Customer = customer;
        await _EntityFactory.GetOrderDetail(secondOrder, await _EntityFactory.GetProduct("otherProduct", true), 350m, 2, 0.2f);
        await _EntityFactory.GetOrderDetail(secondOrder, await _EntityFactory.GetProduct("secondOtherProduct", true), 400m, 5, 0.2f);

        await _DbContext.SaveChangesAsync();

        // Act
        CustomerDetailsDto? result = await _CustomerService.GetCustomerDetails(customer.CustomerId);

        // Assert
        Assert.That(result, Is.Not.Null);

        Assert.That(result.CustomerId, Is.EqualTo(customer.CustomerId));
        Assert.That(result.CompanyName, Is.EqualTo(customer.CompanyName));
        Assert.That(result.ContactName, Is.EqualTo(customer.ContactName));
        Assert.That(result.ContactTitle, Is.EqualTo(customer.ContactTitle));
        Assert.That(result.Address, Is.EqualTo(customer.Address));
        Assert.That(result.City, Is.EqualTo(customer.City));
        Assert.That(result.Region, Is.EqualTo(customer.Region));
        Assert.That(result.PostalCode, Is.EqualTo(customer.PostalCode));
        Assert.That(result.Country, Is.EqualTo(customer.Country));
        Assert.That(result.Phone, Is.EqualTo(customer.Phone));
        Assert.That(result.Fax, Is.EqualTo(customer.Fax));

        Assert.That(result.Orders, Has.Count.EqualTo(2));

        OrderSummaryDto firstInList = result.Orders.First();
        Assert.That(firstInList.OrderId, Is.EqualTo(secondOrder.OrderId));
        Assert.That(firstInList.TotalValue, Is.EqualTo(2160.0m));
        Assert.That(firstInList.ProductCount, Is.EqualTo(secondOrder.OrderDetails.Count));

        OrderSummaryDto secondInList = result.Orders.Last();
        Assert.That(secondInList.OrderId, Is.EqualTo(order.OrderId));
        Assert.That(secondInList.TotalValue, Is.EqualTo(1670.0m));
        Assert.That(secondInList.ProductCount, Is.EqualTo(order.OrderDetails.Count));
    }
}
