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
    public async Task When_ContactNameIsNotProvided(string? contactName)
    {
        // Arrange
        Infrastructure.Persistance.Generated.Entities.Customer customer = await _EntityFactory.GetCustomer("ABCDE", "testCompanyName");
        customer.ContactName = "testContactName";

        Order order = await _EntityFactory.GetOrder();
        order.Customer = customer;

        await _DbContext.SaveChangesAsync();

        // Act
        IList<CustomerOverviewDto> result = await _CustomerService.GetCustomers(contactName);

        // Assert
        Assert.That(result, Has.Count.EqualTo(1));

        Assert.That(result[0].CustomerId, Is.EqualTo(customer.CustomerId));
        Assert.That(result[0].ContactName, Is.EqualTo(customer.ContactName));
        Assert.That(result[0].OrderCount, Is.EqualTo(customer.Orders.Count));
    }

    [TestCase("testContactName")]
    [TestCase("testCont")]
    public async Task When_ContactNameIsProvided_And_Matches(string contactName)
    {
        // Arrange
        Infrastructure.Persistance.Generated.Entities.Customer customer = await _EntityFactory.GetCustomer("ALFKI", "testCompanyName");
        customer.ContactName = "testContactName";

        Infrastructure.Persistance.Generated.Entities.Customer secondCustomer = await _EntityFactory.GetCustomer("AHGTA", "secondTestCompanyName");
        secondCustomer.ContactName = "differentName";

        Order order = await _EntityFactory.GetOrder();
        order.Customer = customer;

        Order secondOrder = await _EntityFactory.GetOrder();
        secondOrder.Customer = secondCustomer;

        await _DbContext.SaveChangesAsync();

        // Act
        IList<CustomerOverviewDto> result = await _CustomerService.GetCustomers(contactName);

        // Assert
        Assert.That(result, Has.Count.EqualTo(1));

        Assert.That(result[0].CustomerId, Is.EqualTo(customer.CustomerId));
        Assert.That(result[0].ContactName, Is.EqualTo(customer.ContactName));
        Assert.That(result[0].OrderCount, Is.EqualTo(customer.Orders.Count));
    }

    [Test]
    public async Task When_ContactNameIsProvided_And_DoesNotMatch()
    {
        // Arrange
        Infrastructure.Persistance.Generated.Entities.Customer customer = await _EntityFactory.GetCustomer("ALFKI", "testCompanyName");
        customer.ContactName = "testContactName";

        await _DbContext.SaveChangesAsync();

        // Act
        IList<CustomerOverviewDto> result = await _CustomerService.GetCustomers("differentName");

        // Assert
        Assert.That(result, Is.Empty);
    }
}
