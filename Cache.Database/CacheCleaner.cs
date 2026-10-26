using System.Runtime.CompilerServices;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

[assembly: InternalsVisibleTo("Zhomfr.Cache.Database.Tests")]

namespace Zhomfr.Cache.Database;

internal class CacheCleaner<TDbContext>(IDbContextFactory<TDbContext> dbContextFactory, ILogger<CacheCleaner<TDbContext>> logger) : BackgroundService
    where TDbContext : DbContext
{
    private readonly TimeSpan _interval = TimeSpan.FromMinutes(1);

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        using PeriodicTimer timer = new(this._interval);

        do {
            try {
                await using TDbContext context = await dbContextFactory.CreateDbContextAsync(ct);

                while (await context.Set<CacheItem>().Where(e => e.ExpiredAt != null && e.ExpiredAt < DateTimeOffset.UtcNow).ExecuteDeleteAsync(ct) > 0) {
                }
            } catch (Exception e) {
                logger.LogError(e, "Failed to cleanup expired cache items.");
            }
        } while (await timer.WaitForNextTickAsync(ct));
    }
}
