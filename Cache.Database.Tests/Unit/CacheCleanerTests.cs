using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Testing.Platform.Services;

using Zhomfr.Cache.Database.Tests.Stubs;

namespace Zhomfr.Cache.Database.Tests.Unit;

[TestFixture(typeof(MsSqlDbContext))]
[TestFixture(typeof(MySqlDbContext))]
[TestFixture(typeof(PostgreSqlDbContext))]
public class CacheCleanerTests<T> where T : DbContext
{
    [Test]
    [CancelAfter(1_000)]
    public async Task Service_Should_Execute_Work_And_Stop_Gracefully(CancellationToken ct)
    {
        LoggerFactory           loggerFactory = new();
        Logger<CacheCleaner<T>> logger        = new(loggerFactory);

        IDbContextFactory<T> dbContextFactory = GlobalSetup.ServiceProvider!.GetRequiredService<IDbContextFactory<T>>();
        await using T        dbContext        = await dbContextFactory.CreateDbContextAsync(ct);

        Assert.That(dbContext.Set<CacheItem>().Count(), Is.EqualTo(4));

        using CacheCleaner<T>         service   = new(dbContextFactory, logger);
        using CancellationTokenSource cts       = new();
        Task                          startTask = service.StartAsync(cts.Token);

        await Task.Delay(500, cts.Token);

        await cts.CancelAsync();
        await service.StopAsync(ct);
        await startTask;

        Assert.That(dbContext.Set<CacheItem>().Count(), Is.EqualTo(2));
    }
}
