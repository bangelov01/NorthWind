using NorthWind.Api.Extensions;
using NorthWind.Api.Middleware;
using NorthWind.Infrastructure.Extensions;
using NorthWind.Services.Extensions;

namespace NorthWind.Api;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddInfrastructure(builder.Configuration, builder.Environment.IsDevelopment());
        builder.Services.AddServices();

        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();
        builder.Services.AddOpenApi();
        builder.Services.AddCorsPolicy(builder.Configuration);
        builder.Services.AddControllers();

        WebApplication app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwaggerUI();
        }

        app.UseExceptionHandler();
        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseCors("DefaultPolicy");

        app.MapControllers();

        app.Run();
    }
}