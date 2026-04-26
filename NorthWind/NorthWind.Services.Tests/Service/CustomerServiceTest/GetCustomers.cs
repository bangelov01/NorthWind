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
        Infrastructure.Persistance.Generated.Entities.Customer customer = await _EntityFactory.GetCustomer("ABCDE", "testCompanyName");

        Order order = await _EntityFactory.GetOrder();
        order.Customer = customer;

        await _DbContext.SaveChangesAsync();

        // Act
        IList<CustomerOverviewDto> result = await _CustomerService.GetCustomers(companynName);

        // Assert
        Assert.That(result, Has.Count.EqualTo(1));

        Assert.That(result[0].CustomerId, Is.EqualTo(customer.CustomerId));
        Assert.That(result[0].CompanyName, Is.EqualTo(customer.CompanyName));
        Assert.That(result[0].OrderCount, Is.EqualTo(customer.Orders.Count));
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
        IList<CustomerOverviewDto> result = await _CustomerService.GetCustomers(companyName);

        // Assert
        Assert.That(result, Has.Count.EqualTo(1));

        Assert.That(result[0].CustomerId, Is.EqualTo(customer.CustomerId));
        Assert.That(result[0].CompanyName, Is.EqualTo(customer.CompanyName));
        Assert.That(result[0].OrderCount, Is.EqualTo(customer.Orders.Count));
    }

    [Test]
    public async Task When_CompanyNameIsProvided_And_DoesNotMatch()
    {
        // Arrange
        await _EntityFactory.GetCustomer("ALFKI", "testCompanyName");

        await _DbContext.SaveChangesAsync();

        // Act
        IList<CustomerOverviewDto> result = await _CustomerService.GetCustomers("differentName");

        // Assert
        Assert.That(result, Is.Empty);
    }
}
