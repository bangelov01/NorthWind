using Microsoft.Extensions.DependencyInjection;
using NorthWind.Services.Customer;

namespace NorthWind.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomerService, CustomerService>();

        return services;
    }
}
