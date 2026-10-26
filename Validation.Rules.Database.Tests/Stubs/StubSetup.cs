using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Moq;

namespace Zhomfr.Validation.Rules.Database.Tests.Stubs;

public class StubSetup : IDisposable
{
    public StubSetup()
    {
        DbContextOptions<StubDbContext> options   = new DbContextOptionsBuilder<StubDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        StubDbContext                   dbContext = new(options);
        Mock<IServiceProvider>          mock      = new();
        StubModel                       model     = new() { Id = 1, Name = "Test" };

        dbContext.Database.EnsureCreated();
        dbContext.StubEntities.Add(model);
        dbContext.SaveChanges();

        mock.Setup(m => m.GetService(typeof(StubDbContext))).Returns(dbContext);
        mock.Setup(m => m.GetService(typeof(StubNotExistDbContext))).Returns(null!);

        this.ValidationContext = new(new(), mock.Object, null) { DisplayName = "MyProp" };
    }

    public ValidationContext ValidationContext { get; }

    /// <inheritdoc/>
    public void Dispose()
    {
        GC.SuppressFinalize(this);

        this.ValidationContext.GetRequiredService<StubDbContext>().Database.EnsureDeleted();
        this.ValidationContext.GetRequiredService<StubDbContext>().Dispose();
    }
}
