using NorthWind.Infrastructure.Persistance.Generated.Entities;
using NorthWind.Services.Customer;
using NorthWind.Services.Customer.Dto;

namespace NorthWind.Services.Tests.Service.CustomerServiceTest;

[TestFixture]
internal class GetCustomers : DatabaseTestBase
{
    private CustomerService _CustomerService;

    [SetUp]
    public void SetUp()
    {
        _CustomerService = new CustomerService(_DbContext);
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("      ")]
    public async Task When_CompanyNameIsNotProvided(string? companynName)
    {
        // Arrange
        Infrastructure.Persistance.Generated.Entities.Customer customer = await _EntityFactory.GetCustomer("AXIVC", "bCompanyName");
        Infrastructure.Persistance.Generated.Entities.Customer secondCustomer = await _EntityFactory.GetCustomer("ABCDE", "aCompanyName");

        Order order = await _EntityFactory.GetOrder();
        order.Customer = secondCustomer;

        await _DbContext.SaveChangesAsync();

        // Act
        IReadOnlyCollection<CustomerOverviewDto> result = await _CustomerService.GetCustomers(companynName);

        // Assert
        Assert.That(result, Has.Count.EqualTo(2));

        CustomerOverviewDto firstInList = result.First();
        Assert.That(firstInList.CustomerId, Is.EqualTo(secondCustomer.CustomerId));
        Assert.That(firstInList.CompanyName, Is.EqualTo(secondCustomer.CompanyName));
        Assert.That(firstInList.OrderCount, Is.EqualTo(secondCustomer.Orders.Count));

        CustomerOverviewDto secondInList = result.Last();
        Assert.That(secondInList.CustomerId, Is.EqualTo(customer.CustomerId));
        Assert.That(secondInList.CompanyName, Is.EqualTo(customer.CompanyName));
        Assert.That(secondInList.OrderCount, Is.EqualTo(customer.Orders.Count));
    }

    [TestCase("testCompanyName")]
    [TestCase("testComp")]
    public async Task When_CompanyNameIsProvided_And_Matches(string companyName)
    {
        // Arrange
        Infrastructure.Persistance.Generated.Entities.Customer customer = await _EntityFactory.GetCustomer("ALFKI", "testCompanyName");

        Infrastructure.Persistance.Generated.Entities.Customer secondCustomer = await _EntityFactory.GetCustomer("AHGTA", "secondTestCompanyName");
        secondCustomer.CompanyName = "differentName";

        Order order = await _EntityFactory.GetOrder();
        order.Customer = customer;

        Order secondOrder = await _EntityFactory.GetOrder();
        secondOrder.Customer = secondCustomer;

        await _DbContext.SaveChangesAsync();

        // Act
        IReadOnlyCollection<CustomerOverviewDto> result = await _CustomerService.GetCustomers(companyName);

        // Assert
        Assert.That(result, Has.Count.EqualTo(1));

        CustomerOverviewDto firstInList = result.First();
        Assert.That(firstInList.CustomerId, Is.EqualTo(customer.CustomerId));
        Assert.That(firstInList.CompanyName, Is.EqualTo(customer.CompanyName));
        Assert.That(firstInList.OrderCount, Is.EqualTo(customer.Orders.Count));
    }

    [Test]
    public async Task When_CompanyNameIsProvided_And_DoesNotMatch()
    {
        // Arrange
        await _EntityFactory.GetCustomer("ALFKI", "testCompanyName");

        await _DbContext.SaveChangesAsync();

        // Act
        IReadOnlyCollection<CustomerOverviewDto> result = await _CustomerService.GetCustomers("differentName");

        // Assert
        Assert.That(result, Is.Empty);
    }
}
