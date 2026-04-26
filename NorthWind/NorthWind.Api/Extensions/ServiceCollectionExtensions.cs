namespace NorthWind.Api.Extensions;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IConfiguration configuration)
    {
        string[]? allowedOrigins = configuration.GetSection("CorsSettings:AllowedOrigins").Get<string[]>();

        services.AddCors(options =>
                {
                    options.AddPolicy("DefaultPolicy", policy =>
                        {
                            policy.WithOrigins(allowedOrigins!)
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                        });
                }
        );

        return services;
    }
}
