using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using NorthWind.Infrastructure.Persistance.Generated.Entities;
using NorthWind.Services.Customer.Dto;

namespace NorthWind.Api.Tests.Rest.CustomersControllerTest;

[TestFixture]
internal class GetCustomerDetails : ApiTestBase
{
    [TestCase("AL")]
    [TestCase("ALERAR")]
    public async Task When_CalledWithInvalidId(string customerId)
    {
        // Arrange
        await _EntityFactory.GetCustomer("ALFKI", "testCompany");

        await _DbContext.SaveChangesAsync();

        // Act
        HttpResponseMessage response = await _HttpClient.GetAsync($"/api/customers/{customerId}");

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
    }

    [Test]
    public async Task When_CalledWithValidId()
    {
        // Arrange
        Customer customer = await _EntityFactory.GetCustomer("ALFKI", "testCompany");

        Order order = await _EntityFactory.GetOrder();
        order.Customer = customer;
        await _EntityFactory.GetOrderDetail(order, await _EntityFactory.GetProduct("firstTestProductName", true), 200m, 2, 0.2f);

        await _DbContext.SaveChangesAsync();

        // Act
        HttpResponseMessage response = await _HttpClient.GetAsync($"/api/customers/{customer.CustomerId}");
        CustomerDetailsDto? result = await response.Content.ReadFromJsonAsync<CustomerDetailsDto>();

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response.Content.Headers.ContentType?.MediaType, Is.EqualTo("application/json"));

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

        Assert.That(result.Orders, Has.Count.EqualTo(1));
        Assert.That(result.Orders.First().OrderId, Is.EqualTo(order.OrderId));
        Assert.That(result.Orders.First().TotalValue, Is.EqualTo(320.0m));
        Assert.That(result.Orders.First().ProductCount, Is.EqualTo(order.OrderDetails.Count));
    }

    [Test]
    public async Task When_CalledWithInvalidId_And_CustomerDoesNotExist()
    {
        // Arrange
        await _EntityFactory.GetCustomer("ALFKI", "testCompany");

        await _DbContext.SaveChangesAsync();

        // Act
        HttpResponseMessage response = await _HttpClient.GetAsync("/api/customers/XXXXX");
        NotFoundResult? result = await response.Content.ReadFromJsonAsync<NotFoundResult>();

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));

        Assert.That(result, Is.Not.Null);
        Assert.That(result.StatusCode, Is.EqualTo(404));
    }
}
