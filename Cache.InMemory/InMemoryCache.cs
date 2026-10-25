using System.Collections.Concurrent;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

using Zhomfr.Cache.Abstractions;

namespace Zhomfr.Cache.InMemory;

/// <summary>
///     Provides an in-memory implementation of the <see cref="ICache"/> interface using <see cref="IMemoryCache"/>.
///     This class mimics the Laravel Cache facade behavior, supporting fluent data retrieval, storage, and dependency
///     injection within callbacks.
/// </summary>
/// <remarks>
///     This implementation is suitable for single-server applications. For distributed environments, consider using a
///     Redis-based implementation.
/// </remarks>
/// <param name="memoryCache">The underlying .NET memory cache instance.</param>
/// <param name="serviceProvider">The service provider used to resolve dependencies inside "Remember" callbacks.</param>
/// <param name="config">The configuration settings for this cache instance.</param>
public class InMemoryCache(IMemoryCache memoryCache, IServiceProvider serviceProvider, InMemoryConfig config) : ICache
{
    private static readonly ConcurrentDictionary<string, CancellationTokenSource> _prefixTokens = new();
    private readonly        TimeSpan                                              _defaultTtl   = TimeSpan.FromSeconds(config.Ttl);

    /// <inheritdoc/>
    public T? Get<T>(string key)
    {
        return memoryCache.Get<T>(this.PrepareKey(key));
    }

    /// <inheritdoc/>
    public Task<T?> GetAsync<T>(string key, CancellationToken ct = default)
    {
        return Task.FromResult(this.Get<T>(key));
    }

    /// <inheritdoc/>
    public T? Get<T>(string key, T? defaultValue)
    {
        return memoryCache.TryGetValue(this.PrepareKey(key), out T? value) ? value : defaultValue;
    }

    /// <inheritdoc/>
    public Task<T?> GetAsync<T>(string key, T? defaultValue, CancellationToken ct = default)
    {
        return Task.FromResult(this.Get(key, defaultValue));
    }

    /// <inheritdoc/>
    public void Put<T>(string key, T value, TimeSpan? ttl = null)
    {
        this.Set(key, value, ttl ?? this._defaultTtl);
    }

    /// <inheritdoc/>
    public Task PutAsync<T>(string key, T value, TimeSpan? ttl = null, CancellationToken ct = default)
    {
        this.Put(key, value, ttl);

        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public bool Add<T>(string key, T value, TimeSpan? ttl = null)
    {
        if (this.Has(key)) {
            return false;
        }

        this.Set(key, value, ttl ?? this._defaultTtl);

        return true;
    }

    /// <inheritdoc/>
    public Task<bool> AddAsync<T>(string key, T value, TimeSpan? ttl = null, CancellationToken ct = default)
    {
        return Task.FromResult(this.Add(key, value, ttl));
    }

    /// <inheritdoc/>
    public void Forever<T>(string key, T value)
    {
        this.Set(key, value);
    }

    /// <inheritdoc/>
    public Task ForeverAsync<T>(string key, T value, CancellationToken ct = default)
    {
        this.Forever(key, value);

        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public bool Has(string key)
    {
        return memoryCache.TryGetValue(this.PrepareKey(key), out object? _);
    }

    /// <inheritdoc/>
    public Task<bool> HasAsync(string key, CancellationToken ct = default)
    {
        return Task.FromResult(this.Has(key));
    }

    /// <inheritdoc/>
    public bool Forget(string key)
    {
        if (!this.Has(key)) {
            return false;
        }

        this.Remove(key);

        return true;
    }

    /// <inheritdoc/>
    public Task<bool> ForgetAsync(string key, CancellationToken ct = default)
    {
        return Task.FromResult(this.Forget(key));
    }

    /// <inheritdoc/>
    public void Flush()
    {
        if (InMemoryCache._prefixTokens.TryRemove(config.Prefix, out CancellationTokenSource? cts)) {
            cts.Cancel();
            cts.Dispose();
        }
    }

    /// <inheritdoc/>
    public Task FlushAsync(CancellationToken ct = default)
    {
        this.Flush();

        return Task.CompletedTask;
    }

    /// <inheritdoc/>
    public T Remember<T>(string key, TimeSpan? ttl, Func<IServiceProvider, T> callback)
    {
        T? value = this.Get<T>(key);

        if (value != null) {
            return value;
        }

        T newValue = callback(serviceProvider);

        this.Set(key, newValue, ttl ?? this._defaultTtl);

        return newValue;
    }

    /// <inheritdoc/>
    public async Task<T> RememberAsync<T>(string key, TimeSpan? ttl, Func<IServiceProvider, CancellationToken, Task<T>> callback, CancellationToken ct = default)
    {
        T? value = this.Get<T>(key);

        if (value != null) {
            return value;
        }

        T newValue = await callback(serviceProvider, ct);
        this.Set(key, newValue, ttl ?? this._defaultTtl);

        return newValue;
    }

    /// <inheritdoc/>
    public T RememberForever<T>(string key, Func<IServiceProvider, T> callback)
    {
        if (memoryCache.TryGetValue(this.PrepareKey(key), out T? value)) {
            return value!;
        }

        T newValue = callback(serviceProvider);
        this.Set(key, newValue);

        return newValue;
    }

    /// <inheritdoc/>
    public async Task<T> RememberForeverAsync<T>(string key, Func<IServiceProvider, CancellationToken, Task<T>> callback, CancellationToken ct = default)
    {
        if (memoryCache.TryGetValue(this.PrepareKey(key), out T? value)) {
            return value!;
        }

        T newValue = await callback(serviceProvider, ct);
        this.Set(key, newValue);

        return newValue;
    }

    /// <inheritdoc/>
    public T? Pull<T>(string key)
    {
        if (!memoryCache.TryGetValue(this.PrepareKey(key), out T? value)) {
            return default;
        }

        this.Remove(key);

        return value;
    }

    /// <inheritdoc/>
    public Task<T?> PullAsync<T>(string key, CancellationToken ct = default)
    {
        return Task.FromResult(this.Pull<T>(key));
    }

    /// <summary>
    ///     Gets or creates a <see cref="CancellationTokenSource"/> for the current prefix. This token is used to
    ///     invalidate all cache entries associated with this specific prefix at once.
    /// </summary>
    /// <returns>A <see cref="CancellationTokenSource"/> instance tied to the configured prefix.</returns>
    private CancellationTokenSource GetTokenSource()
    {
        return InMemoryCache._prefixTokens.GetOrAdd(config.Prefix, _ => new());
    }

    /// <summary>Sets a value in the cache with the specified key, handling prefixing and expiration tokens.</summary>
    /// <typeparam name="T">The type of the item to cache.</typeparam>
    /// <param name="key">The cache key (will be prefixed automatically).</param>
    /// <param name="value">The data to be cached.</param>
    /// <param name="expiration">An optional absolute expiration timespan.</param>
    private void Set<T>(string key, T value, TimeSpan? expiration = null)
    {
        CancellationTokenSource cts = this.GetTokenSource();

        MemoryCacheEntryOptions options = new MemoryCacheEntryOptions().AddExpirationToken(new CancellationChangeToken(cts.Token)).SetSize(1);

        if (expiration.HasValue) {
            options.SetAbsoluteExpiration(expiration.Value);
        }

        memoryCache.Set(this.PrepareKey(key), value, options);
    }

    /// <summary>Prepares a fully qualified cache key by combining the configured prefix with the provided key.</summary>
    /// <param name="key">The original cache key.</param>
    /// <returns>A string representing the prefixed key (e.g., "prefix:key").</returns>
    private string PrepareKey(string key)
    {
        return $"{config.Prefix}{ICache.KEY_SEPARATOR}{key}";
    }

    /// <summary>Removes a specific item from the cache by its key, applying the configured prefix.</summary>
    /// <param name="key">The cache key to remove.</param>
    private void Remove(string key)
    {
        memoryCache.Remove(this.PrepareKey(key));
    }
}
