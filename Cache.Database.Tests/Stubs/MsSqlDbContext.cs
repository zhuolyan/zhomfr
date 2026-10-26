using Microsoft.EntityFrameworkCore;

namespace Zhomfr.Cache.Database.Tests.Stubs;

public class MsSqlDbContext(DbContextOptions<MsSqlDbContext> options) : DbContext(options)
{
    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Assert.That(modelBuilder.ApplyCacheConfiguration(), Is.InstanceOf<ModelBuilder>());
    }
}
