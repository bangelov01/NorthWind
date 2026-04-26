using NorthWind.Infrastructure.Extensions;

namespace NorthWind.Api;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddInfrastructure(builder.Configuration, builder.Environment.IsDevelopment());

        builder.Services.AddOpenApi();

        builder.Services.AddControllers();

        WebApplication app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.MapControllers();

        app.Run();
    }
}