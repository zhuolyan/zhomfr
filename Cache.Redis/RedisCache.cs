using System.Net;
using System.Text.Json;

using StackExchange.Redis;

using Zhomfr.Cache.Abstractions;

namespace Zhomfr.Cache.Redis;

// ReSharper disable once ClassNeverInstantiated.Global
/// <inheritdoc cref="ICache"/>
public class RedisCache : ICache, IDisposable
{
    private static readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };
    private readonly        ConnectionMultiplexer _connection;
    private readonly        IDatabase             _db;
    private readonly        TimeSpan              _defaultTtl;
    private readonly        string                _prefix;
    private readonly        IServiceProvider      _serviceProvider;

    /// <summary>
    ///     Provides an Redis implementation of the <see cref="ICache"/> interface using
    ///     <see cref="ConnectionMultiplexer"/>. This class mimics the Laravel Cache facade behavior, supporting fluent data
    ///     retrieval, storage, and dependency injection within callbacks.
    /// </summary>
    /// <param name="serviceProvider">The service provider used to resolve dependencies inside "Remember" callbacks.</param>
    /// <param name="config">The configuration settings for this cache instance.</param>
    public RedisCache(IServiceProvider serviceProvider, RedisConfig config)
    {
        this._serviceProvider = serviceProvider;

        ConfigurationOptions redisConfig = ConfigurationOptions.Parse(config.Connection);

        this._connection = ConnectionMultiplexer.Connect(redisConfig);
        this._db         = this._connection.GetDatabase();
        this._defaultTtl = TimeSpan.FromMinutes(config.Ttl);
        this._prefix     = config.Prefix;
    }

    /// <inheritdoc/>
    public T? Get<T>(string key)
    {
        RedisValue data = this._db.StringGet(this.PrepareKey(key));

        return data.IsNull ? default : JsonSerializer.Deserialize<T>((byte[])data!, RedisCache._jsonOptions);
    }

    /// <inheritdoc/>
    public async Task<T?> GetAsync<T>(string key, CancellationToken ct = default)
    {
        RedisValue data = await this._db.StringGetAsync(this.PrepareKey(key)).WaitAsync(ct);

        return data.IsNull ? default : JsonSerializer.Deserialize<T>((byte[])data!, RedisCache._jsonOptions);
    }

    /// <inheritdoc/>
    public T? Get<T>(string key, T? defaultValue)
    {
        return this.Get<T>(key) ?? defaultValue;
    }

    /// <inheritdoc/>
    public async Task<T?> GetAsync<T>(string key, T? defaultValue, CancellationToken ct = default)
    {
        T? value = await this.GetAsync<T>(key, ct);

        return value != null ? value : defaultValue;
    }

    /// <inheritdoc/>
    public void Put<T>(string key, T value, TimeSpan? ttl = null)
    {
        byte[] bytes = JsonSerializer.SerializeToUtf8Bytes(value, RedisCache._jsonOptions);
        this._db.StringSet(this.PrepareKey(key), bytes, ttl ?? this._defaultTtl);
    }

    /// <inheritdoc/>
    public async Task PutAsync<T>(string key, T value, TimeSpan? ttl = null, CancellationToken ct = default)
    {
        byte[] bytes = JsonSerializer.SerializeToUtf8Bytes(value, RedisCache._jsonOptions);
        await this._db.StringSetAsync(this.PrepareKey(key), bytes, ttl ?? this._defaultTtl).WaitAsync(ct);
    }

    /// <inheritdoc/>
    public bool Add<T>(string key, T value, TimeSpan? ttl = null)
    {
        byte[] bytes = JsonSerializer.SerializeToUtf8Bytes(value, RedisCache._jsonOptions);

        return this._db.StringSet(this.PrepareKey(key), bytes, ttl ?? this._defaultTtl, When.NotExists);
    }

    /// <inheritdoc/>
    public async Task<bool> AddAsync<T>(string key, T value, TimeSpan? ttl = null, CancellationToken ct = default)
    {
        byte[] bytes = JsonSerializer.SerializeToUtf8Bytes(value, RedisCache._jsonOptions);

        return await this._db.StringSetAsync(this.PrepareKey(key), bytes, ttl ?? this._defaultTtl, When.NotExists).WaitAsync(ct);
    }

    /// <inheritdoc/>
    public void Forever<T>(string key, T value)
    {
        byte[] bytes = JsonSerializer.SerializeToUtf8Bytes(value, RedisCache._jsonOptions);
        this._db.StringSet(this.PrepareKey(key), bytes);
    }

    /// <inheritdoc/>
    public async Task ForeverAsync<T>(string key, T value, CancellationToken ct = default)
    {
        byte[] bytes = JsonSerializer.SerializeToUtf8Bytes(value, RedisCache._jsonOptions);
        await this._db.StringSetAsync(this.PrepareKey(key), bytes).WaitAsync(ct);
    }

    /// <inheritdoc/>
    public bool Has(string key)
    {
        return this._db.KeyExists(this.PrepareKey(key));
    }

    /// <inheritdoc/>
    public async Task<bool> HasAsync(string key, CancellationToken ct = default)
    {
        return await this._db.KeyExistsAsync(this.PrepareKey(key)).WaitAsync(ct);
    }

    /// <inheritdoc/>
    public bool Forget(string key)
    {
        return this._db.KeyDelete(this.PrepareKey(key));
    }

    /// <inheritdoc/>
    public async Task<bool> ForgetAsync(string key, CancellationToken ct = default)
    {
        return await this._db.KeyDeleteAsync(this.PrepareKey(key)).WaitAsync(ct);
    }

    /// <inheritdoc/>
    public void Flush()
    {
        foreach (EndPoint endpoint in this._connection.GetEndPoints()) {
            IServer server = this._connection.GetServer(endpoint);

            foreach (RedisKey k in server.Keys(this._db.Database, this.PrepareKey("*"))) {
                this._db.KeyDelete(k);
            }
        }
    }

    /// <inheritdoc/>
    public async Task FlushAsync(CancellationToken ct = default)
    {
        foreach (EndPoint endpoint in this._connection.GetEndPoints()) {
            IServer server = this._connection.GetServer(endpoint);

            await foreach (RedisKey k in server.KeysAsync(this._db.Database, this.PrepareKey("*")).WithCancellation(ct)) {
                await this._db.KeyDeleteAsync(k).WaitAsync(ct);
            }
        }
    }

    /// <inheritdoc/>
    public T Remember<T>(string key, TimeSpan? ttl, Func<IServiceProvider, T> callback)
    {
        T? value = this.Get<T>(key);

        if (value != null) {
            return value;
        }

        T newValue = callback(this._serviceProvider);
        this.Put(key, newValue, ttl);

        return newValue;
    }

    /// <inheritdoc/>
    public async Task<T> RememberAsync<T>(string key, TimeSpan? ttl, Func<IServiceProvider, CancellationToken, Task<T>> callback, CancellationToken ct = default)
    {
        T? value = await this.GetAsync<T>(key, ct);

        if (value != null) {
            return value;
        }

        T newValue = await callback(this._serviceProvider, ct);
        await this.PutAsync(key, newValue, ttl, ct);

        return newValue;
    }

    /// <inheritdoc/>
    public T RememberForever<T>(string key, Func<IServiceProvider, T> callback)
    {
        T? value = this.Get<T>(key);

        if (value != null) {
            return value;
        }

        T newValue = callback(this._serviceProvider);
        this.Forever(key, newValue);

        return newValue;
    }

    /// <inheritdoc/>
    public async Task<T> RememberForeverAsync<T>(string key, Func<IServiceProvider, CancellationToken, Task<T>> callback, CancellationToken ct = default)
    {
        T? value = await this.GetAsync<T>(key, ct);

        if (value != null) {
            return value;
        }

        T newValue = await callback(this._serviceProvider, ct);
        await this.ForeverAsync(key, newValue, ct);

        return newValue;
    }

    /// <inheritdoc/>
    public T? Pull<T>(string key)
    {
        T? value = this.Get<T>(key);

        if (value != null) {
            this.Forget(key);
        }

        return value;
    }

    /// <inheritdoc/>
    public async Task<T?> PullAsync<T>(string key, CancellationToken ct = default)
    {
        T? value = await this.GetAsync<T>(key, ct);

        if (value != null) {
            await this.ForgetAsync(key, ct);
        }

        return value;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        this._connection.Dispose();

        GC.SuppressFinalize(this);
    }

    private string PrepareKey(string key)
    {
        return $"{this._prefix}{ICache.KEY_SEPARATOR}{key}";
    }
}
