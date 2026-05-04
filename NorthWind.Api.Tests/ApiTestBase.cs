using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using NorthWind.Infrastructure.Persistance.Generated;
using NorthWind.Services.Tests;

namespace NorthWind.Api.Tests;

internal abstract class ApiTestBase
{
    protected HttpClient _HttpClient;
    protected NorthWindDbContext _DbContext;
    protected EntityFactory _EntityFactory;

    private WebApplicationFactory<Program> _Factory;
    private IServiceScope _Scope;

    [SetUp]
    public void SetUp()
    {
        _Factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                        {
                            ServiceDescriptor? descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IDbContextOptionsConfiguration<NorthWindDbContext>));

                            if (descriptor != null)
                            {
                                services.Remove(descriptor);
                            }

                            services.AddDbContext<NorthWindDbContext>(options =>
                                options
                                    .UseInMemoryDatabase("NorthWindDbTest")
                                    .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                            );
                        });
                });

        _HttpClient = _Factory.CreateClient();

        _Scope = _Factory.Services.CreateScope();
        _DbContext = _Scope.ServiceProvider.GetRequiredService<NorthWindDbContext>();
        _DbContext.Database.EnsureCreated();

        _EntityFactory = new EntityFactory(_DbContext);
    }

    [TearDown]
    public void TearDown()
    {
        _DbContext.Database.EnsureDeleted();
        _DbContext.Dispose();
        _Scope.Dispose();
        _HttpClient.Dispose();
        _Factory.Dispose();
    }
}
