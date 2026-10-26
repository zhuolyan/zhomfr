using Microsoft.EntityFrameworkCore;

namespace Zhomfr.Cache.Database.Tests.Stubs;

public class MySqlDbContext(DbContextOptions<MySqlDbContext> options) : DbContext(options)
{
    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Assert.That(modelBuilder.ApplyCacheConfiguration(), Is.InstanceOf<ModelBuilder>());
    }
}
