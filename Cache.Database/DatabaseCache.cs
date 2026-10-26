using System.Text.Json;

using Microsoft.EntityFrameworkCore;

using Zhomfr.Cache.Abstractions;

// ReSharper disable ClassNeverInstantiated.Global

namespace Zhomfr.Cache.Database;

/// <summary>
///     Provides a database implementation of the <see cref="ICache"/> interface using <see cref="DbContext"/>. This
///     class mimics the Laravel Cache facade behavior, supporting fluent data retrieval, storage, and dependency injection
///     within callbacks.
/// </summary>
/// <param name="dbContextFactory">Factory of db context.</param>
/// <param name="serviceProvider">DI container.</param>
/// <param name="databaseCacheConfig">Configuration for database cache service.</param>
/// <typeparam name="TDbContext">Db context.</typeparam>
public class DatabaseCache<TDbContext>(IDbContextFactory<TDbContext> dbContextFactory, IServiceProvider serviceProvider, DatabaseCacheConfig databaseCacheConfig)
    : ICache where TDbContext : DbContext
{
    private readonly TimeSpan              _defaultTtl  = TimeSpan.FromSeconds(databaseCacheConfig.Ttl);
    private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

#region Iterface implementation

    /// <inheritdoc/>
    public T? Get<T>(string key)
    {
        using TDbContext context = this.GetContext();

        key = this.PrepareKey(key);

        CacheItem? result = context.Set<CacheItem>().FirstOrDefault(e => e.Key == key && (e.ExpiredAt == null || e.ExpiredAt > DateTimeOffset.UtcNow));

        return result is null ? default : JsonSerializer.Deserialize<T>(result.Value, this._jsonOptions);
    }

    /// <inheritdoc/>
    public async Task<T?> GetAsync<T>(string key, CancellationToken ct = default)
    {
        await using TDbContext context = await this.GetContextAsync(ct);

        key = this.PrepareKey(key);

        CacheItem? result = await context.Set<CacheItem>()
                                         .FirstOrDefaultAsync(e => e.Key == key && (e.ExpiredAt == null || e.ExpiredAt > DateTimeOffset.UtcNow), ct);

        return result is null ? default : JsonSerializer.Deserialize<T>(result.Value, this._jsonOptions);
    }

    /// <inheritdoc/>
    public T? Get<T>(string key, T? defaultValue)
    {
        return this.Get<T>(key) ?? defaultValue;
    }

    /// <inheritdoc/>
    public async Task<T?> GetAsync<T>(string key, T? defaultValue, CancellationToken ct = default)
    {
        return await this.GetAsync<T>(key, ct) ?? defaultValue;
    }

    /// <inheritdoc/>
    public void Put<T>(string key, T value, TimeSpan? ttl = null)
    {
        this.Set(key, value, ttl ?? this._defaultTtl);
    }

    /// <inheritdoc/>
    public Task PutAsync<T>(string key, T value, TimeSpan? ttl = null, CancellationToken ct = default)
    {
        return this.SetAsync(key, value, ttl ?? this._defaultTtl, ct);
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
    public async Task<bool> AddAsync<T>(string key, T value, TimeSpan? ttl = null, CancellationToken ct = default)
    {
        if (await this.HasAsync(key, ct)) {
            return false;
        }

        await this.SetAsync(key, value, ttl ?? this._defaultTtl, ct);

        return true;
    }

    /// <inheritdoc/>
    public void Forever<T>(string key, T value)
    {
        this.Set(key, value);
    }

    /// <inheritdoc/>
    public Task ForeverAsync<T>(string key, T value, CancellationToken ct = default)
    {
        return this.SetAsync(key, value, null, ct);
    }

    /// <inheritdoc/>
    public bool Has(string key)
    {
        using TDbContext context = this.GetContext();

        key = this.PrepareKey(key);

        return context.Set<CacheItem>().Any(e => e.Key == key && (!e.ExpiredAt.HasValue || e.ExpiredAt.Value > DateTimeOffset.UtcNow));
    }

    /// <inheritdoc/>
    public async Task<bool> HasAsync(string key, CancellationToken ct = default)
    {
        await using TDbContext context = await this.GetContextAsync(ct);

        key = this.PrepareKey(key);

        return await context.Set<CacheItem>().AnyAsync(e => e.Key == key && (!e.ExpiredAt.HasValue || e.ExpiredAt.Value > DateTimeOffset.UtcNow), ct);
    }

    /// <inheritdoc/>
    /// `
    public bool Forget(string key)
    {
        using TDbContext context = this.GetContext();

        key = this.PrepareKey(key);

        return context.Set<CacheItem>().Where(e => e.Key == key && (e.ExpiredAt == null || e.ExpiredAt > DateTimeOffset.UtcNow)).ExecuteDelete() > 0;
    }

    /// <inheritdoc/>
    public async Task<bool> ForgetAsync(string key, CancellationToken ct = default)
    {
        await using TDbContext context = await this.GetContextAsync(ct);

        key = this.PrepareKey(key);

        return await context.Set<CacheItem>().Where(e => e.Key == key && (e.ExpiredAt == null || e.ExpiredAt > DateTimeOffset.UtcNow)).ExecuteDeleteAsync(ct) > 0;
    }

    /// <inheritdoc/>
    public void Flush()
    {
        using TDbContext context = this.GetContext();

        string key = this.PrepareKey(string.Empty);

        context.Set<CacheItem>().Where(e => e.Key.StartsWith(key)).ExecuteDelete();
    }

    /// <inheritdoc/>
    public async Task FlushAsync(CancellationToken ct = default)
    {
        await using TDbContext context = await this.GetContextAsync(ct);

        string key = this.PrepareKey(string.Empty);

        await context.Set<CacheItem>().Where(e => e.Key.StartsWith(key)).ExecuteDeleteAsync(ct);
    }

    /// <inheritdoc/>
    public T Remember<T>(string key, TimeSpan? ttl, Func<IServiceProvider, T> callback)
    {
        T? value = this.Get<T>(key);

        if (value != null) {
            return value;
        }

        T newValue = callback(serviceProvider);

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

        T newValue = await callback(serviceProvider, ct);

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

        T newValue = callback(serviceProvider);

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

        T newValue = await callback(serviceProvider, ct);

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

#endregion

#region Helpers

    /// <summary>Sets a value in the cache with the specified key, handling prefixing and expiration tokens.</summary>
    private async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken ct = default)
    {
        await using TDbContext context = await this.GetContextAsync(ct);

        key = this.PrepareKey(key);

        CacheItem item = new()
        {
            Key       = key,
            ExpiredAt = expiration.HasValue ? DateTimeOffset.UtcNow.Add(expiration.Value) : null,
            Value     = JsonSerializer.Serialize(value, this._jsonOptions),
        };

        await context.Set<CacheItem>().Upsert(item).RunAsync(ct);
    }

    /// <summary>Synchronously sets a value in the cache with the specified key, handling prefixing and expiration tokens.</summary>
    private void Set<T>(string key, T value, TimeSpan? expiration = null)
    {
        using TDbContext context = this.GetContext();

        key = this.PrepareKey(key);

        CacheItem item = new()
        {
            Key       = key,
            ExpiredAt = expiration.HasValue ? DateTimeOffset.UtcNow.Add(expiration.Value) : null,
            Value     = JsonSerializer.Serialize(value, this._jsonOptions),
        };

        context.Set<CacheItem>().Upsert(item).Run();
    }

    private string PrepareKey(string key)
    {
        return $"{databaseCacheConfig.Prefix}{ICache.KEY_SEPARATOR}{key}";
    }

    /// <summary>Створює та повертає новий контекст БД. Використовуйте 'using var' для автоматичного звільнення ресурсів.</summary>
    private TDbContext GetContext()
    {
        return dbContextFactory.CreateDbContext();
    }

    /// <summary>
    ///     Асинхронно створює та повертає новий контекст БД. Використовуйте 'await using var' для автоматичного
    ///     звільнення ресурсів.
    /// </summary>
    private Task<TDbContext> GetContextAsync(CancellationToken ct)
    {
        return dbContextFactory.CreateDbContextAsync(ct);
    }

#endregion
}
