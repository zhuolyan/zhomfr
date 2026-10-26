using Microsoft.EntityFrameworkCore;

using Zhomfr.Cache.Database;

namespace Zhomfr.Cache.Tests.Stubs;

public class MsSqlDbContext(DbContextOptions<MsSqlDbContext> options) : DbContext(options)
{
    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Assert.That(modelBuilder.ApplyCacheConfiguration(), Is.InstanceOf<ModelBuilder>());
    }
}
