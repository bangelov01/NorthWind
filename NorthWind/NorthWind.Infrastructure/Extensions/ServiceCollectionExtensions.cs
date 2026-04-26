using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NorthWind.Infrastructure.Persistance;

namespace NorthWind.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, bool isDevelopment)
    {
        services.AddDbContext<NorthWindDbContext>((serviceProvider, options) =>
            {
                if (isDevelopment)
                {
                    options.EnableDetailedErrors();
                    options.EnableSensitiveDataLogging();
                }

                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions =>
                        {
                            sqlOptions.CommandTimeout(30);
                            sqlOptions.EnableRetryOnFailure();
                        });
            });

        return services;
    }
}
