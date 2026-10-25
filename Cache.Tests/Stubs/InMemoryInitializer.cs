using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Zhomfr.Cache.Abstractions;
using Zhomfr.Cache.InMemory;

namespace Zhomfr.Cache.Tests.Stubs;

public class InMemoryInitializer : AbstractInitializer
{
    /// <inheritdoc/>
    public override void Dispose()
    {
        this.Provider.Dispose();

        GC.SuppressFinalize(this);
    }

    /// <inheritdoc/>
    protected override void SeedData(List<StubSeedItem> data)
    {
        IMemoryCache cache = this.Provider.GetRequiredService<IMemoryCache>();

        foreach (StubSeedItem item in data.Where(item => item.Key is not StubKeys.EXISTS_EXPIRED)) {
            if (item.Ttl.HasValue) {
                cache.Set($"{this.Key}:{item.Key}", item.Value, item.Ttl.Value);
            } else {
                cache.Set($"{this.Key}:{item.Key}", item.Value);
            }
        }
    }

    /// <inheritdoc/>
    protected override void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();

        services.AddKeyedCache<InMemoryCache, InMemoryConfig>(this.Key, configuration);
    }
}
