using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using NorthWind.Api.Rest.Customer.Models;
using NorthWind.Infrastructure.Persistance.Generated.Entities;
using NorthWind.Services.Customer.Dto;

namespace NorthWind.Api.Tests.Rest.CustomersControllerTest;

[TestFixture]
internal class GetCustomers : ApiTestBase
{
    [TestCase(null)]
    [TestCase("      ")]
    public async Task When_CalledWithNoCompanyNameFilter(string? filter)
    {
        // Arrange
        Customer customer = await _EntityFactory.GetCustomer("ALFKI", "testCompany");

        Customer secondCustomer = await _EntityFactory.GetCustomer("AJHTZ", "secondTestCompany");
        secondCustomer.CompanyName = "secondTestCustomer";

        await _DbContext.SaveChangesAsync();

        // Act
        HttpResponseMessage response = await _HttpClient.GetAsync($"/api/customers{(filter == null ? string.Empty : $"?companyName={filter}")}");
        IList<CustomerOverviewDto>? result = await response.Content.ReadFromJsonAsync<IList<CustomerOverviewDto>>();

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response.Content.Headers.ContentType?.MediaType, Is.EqualTo("application/json"));

        Assert.That(result, Is.Not.Null);

        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(result[0].CustomerId, Is.EqualTo(customer.CustomerId));
        Assert.That(result[0].CompanyName, Is.EqualTo(customer.CompanyName));
        Assert.That(result[0].OrderCount, Is.EqualTo(customer.Orders.Count));

        Assert.That(result[1].CustomerId, Is.EqualTo(secondCustomer.CustomerId));
        Assert.That(result[1].CompanyName, Is.EqualTo(secondCustomer.CompanyName));
        Assert.That(result[1].OrderCount, Is.EqualTo(secondCustomer.Orders.Count));
    }

    [Test]
    public async Task When_CalledWithCompanyNameFilter()
    {
        // Arrange
        Customer customer = await _EntityFactory.GetCustomer("ALFKI", "testCompany");

        Customer secondCustomer = await _EntityFactory.GetCustomer("AJHTZ", "secondTestCompany");
        secondCustomer.CompanyName = "secondTestCustomer";

        await _DbContext.SaveChangesAsync();

        // Act
        HttpResponseMessage response = await _HttpClient.GetAsync($"/api/customers?companyName={customer.CompanyName}");
        IList<CustomerOverviewDto>? result = await response.Content.ReadFromJsonAsync<IList<CustomerOverviewDto>>();

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        Assert.That(response.Content.Headers.ContentType?.MediaType, Is.EqualTo("application/json"));

        Assert.That(result, Is.Not.Null);

        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].CustomerId, Is.EqualTo(customer.CustomerId));
        Assert.That(result[0].CompanyName, Is.EqualTo(customer.CompanyName));
        Assert.That(result[0].OrderCount, Is.EqualTo(customer.Orders.Count));
    }

    [Test]
    public async Task When_CalledWithCompanyNameFilter_And_LengthExceedsMaxLength()
    {
        // Arrange
        await _EntityFactory.GetCustomer("ALFKI", "testCompany");

        await _DbContext.SaveChangesAsync();

        // Act
        HttpResponseMessage response = await _HttpClient.GetAsync($"/api/customers?companyName={new string('a', 31)}");
        ValidationProblemDetails? result = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

        // Assert
        Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        Assert.That(response.Content.Headers.ContentType?.MediaType, Is.EqualTo("application/problem+json"));

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Errors, Has.Count.EqualTo(1));
        Assert.That(result.Errors.TryGetValue(nameof(CustomerSearchCriteria.CompanyName), out string[]? errorMessages), Is.True);

        Assert.That(errorMessages, Is.Not.Null);
        Assert.That(errorMessages, Has.Length.EqualTo(1));
        Assert.That(errorMessages[0], Is.EqualTo("Company name cannot be longer than 30 characters."));
    }
}
