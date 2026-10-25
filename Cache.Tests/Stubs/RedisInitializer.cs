using System.Text.Json;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using StackExchange.Redis;

using Zhomfr.Cache.Abstractions;
using Zhomfr.Cache.Redis;

namespace Zhomfr.Cache.Tests.Stubs;

public class RedisInitializer : AbstractInitializer
{
    /// <inheritdoc/>
    protected override void SeedData(List<StubSeedItem> data)
    {
        JsonSerializerOptions jsonOptions = new() { PropertyNameCaseInsensitive = true };
        ConfigurationOptions  redisConfig = ConfigurationOptions.Parse(GlobalSetup.RedisConnection);

        ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(redisConfig);
        IDatabase             db         = connection.GetDatabase();

        foreach (StubSeedItem item in data.Where(item => item.Key is not StubKeys.EXISTS_EXPIRED)) {
            if (item.Ttl.HasValue) {
                db.StringSet($"{this.Key}:{item.Key}", JsonSerializer.SerializeToUtf8Bytes(item.Value, jsonOptions), item.Ttl.Value);
            } else {
                db.StringSet($"{this.Key}:{item.Key}", JsonSerializer.SerializeToUtf8Bytes(item.Value, jsonOptions));
            }
        }
    }

    /// <inheritdoc/>
    protected override void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddKeyedCache<RedisCache, RedisConfig>(this.Key, configuration);
    }
}
