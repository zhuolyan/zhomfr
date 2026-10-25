using Zhomfr.Cache.Abstractions;

namespace Zhomfr.Cache.Tests.Stubs;

public class StubCache(StubConfig config) : ICache
{
    public StubConfig Config => config;

    /// <inheritdoc/>
    public T? Get<T>(string key)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<T?> GetAsync<T>(string key, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public T? Get<T>(string key, T? defaultValue)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<T?> GetAsync<T>(string key, T? defaultValue, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public void Put<T>(string key, T value, TimeSpan? ttl = null)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task PutAsync<T>(string key, T value, TimeSpan? ttl = null, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public bool Add<T>(string key, T value, TimeSpan? ttl = null)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<bool> AddAsync<T>(string key, T value, TimeSpan? ttl = null, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public void Forever<T>(string key, T value)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task ForeverAsync<T>(string key, T value, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public bool Has(string key)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<bool> HasAsync(string key, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public bool Forget(string key)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<bool> ForgetAsync(string key, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public void Flush()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task FlushAsync(CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public T Remember<T>(string key, TimeSpan? ttl, Func<IServiceProvider, T> callback)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<T> RememberAsync<T>(string key, TimeSpan? ttl, Func<IServiceProvider, CancellationToken, Task<T>> callback, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public T RememberForever<T>(string key, Func<IServiceProvider, T> callback)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<T> RememberForeverAsync<T>(string key, Func<IServiceProvider, CancellationToken, Task<T>> callback, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public T? Pull<T>(string key)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<T?> PullAsync<T>(string key, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
