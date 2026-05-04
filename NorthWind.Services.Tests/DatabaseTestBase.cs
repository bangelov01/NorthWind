using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NorthWind.Infrastructure.Persistance.Generated;

namespace NorthWind.Services.Tests;

internal abstract class DatabaseTestBase
{
    protected NorthWindDbContext _DbContext;
    protected EntityFactory _EntityFactory;

    [SetUp]
    public virtual void SetUpDatabase()
    {
        DbContextOptions<NorthWindDbContext> contextOptions = new DbContextOptionsBuilder<NorthWindDbContext>()
            .UseInMemoryDatabase("NorthWindDbTest")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        _DbContext = new NorthWindDbContext(contextOptions);
        _DbContext.Database.EnsureCreated();

        _EntityFactory = new EntityFactory(_DbContext);
    }

    [TearDown]
    public virtual void TearDownDatabase()
    {
        _DbContext.Database.EnsureDeleted();
        _DbContext.Dispose();
    }
}
