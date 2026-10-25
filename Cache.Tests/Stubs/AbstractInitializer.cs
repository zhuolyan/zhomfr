using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Zhomfr.Cache.Abstractions;

// ReSharper disable VirtualMemberCallInConstructor

namespace Zhomfr.Cache.Tests.Stubs;

public abstract class AbstractInitializer : IDisposable
{
    private static     int             _runCount;
    protected readonly string          Key;
    public readonly    ServiceProvider Provider;

    private readonly List<StubSeedItem> _seedItems =
    [
        new() { Key = StubKeys.EXISTS, Value         = StubValues.Exists },
        new() { Key = StubKeys.EXISTS_ANOTHER, Value = StubValues.ExistsAnother },
        new() { Key = StubKeys.EXISTS_EXPIRED, Value = StubValues.ExistsExpired, Ttl = new TimeSpan(0, 0, -1) },
    ];

    protected AbstractInitializer()
    {
        this.Key = $"Key{AbstractInitializer._runCount++}";

        ServiceCollection services = [];

        Dictionary<string, string?> config = new() { { $"Zhomfr:Cache:{this.Key}:Prefix", this.Key }, { $"Zhomfr:Cache:{this.Key}:Ttl", "1" } };

        this.AddServices(services, new ConfigurationBuilder().AddInMemoryCollection(config).Build());

        this.Provider = services.BuildServiceProvider();

        this.SeedData(this._seedItems);
    }

    public ICache Tested => this.Provider.GetRequiredKeyedService<ICache>(this.Key);

    /// <inheritdoc/>
    public abstract void Dispose();

    protected abstract void SeedData(List<StubSeedItem> data);

    protected abstract void AddServices(IServiceCollection services, IConfiguration configuration);
}
