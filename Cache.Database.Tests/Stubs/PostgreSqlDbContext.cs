using Microsoft.EntityFrameworkCore;

namespace Zhomfr.Cache.Database.Tests.Stubs;

public class PostgreSqlDbContext(DbContextOptions<PostgreSqlDbContext> options) : DbContext(options)
{
    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Assert.That(modelBuilder.ApplyCacheConfiguration(), Is.InstanceOf<ModelBuilder>());
    }
}
