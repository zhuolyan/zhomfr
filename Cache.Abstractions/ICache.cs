namespace Zhomfr.Cache.Abstractions;

/// <summary>
///     Defines a unified interface for cache operations, inspired by the Laravel Cache facade. Provides both simple
///     key-value storage and advanced "remember" patterns.
/// </summary>
public interface ICache
{
    /// <summary>The cache key separator used to format the full key with a driver prefix.</summary>
    const string KEY_SEPARATOR = ":";

    /// <summary>Retrieve an item from the cache by its unique key.</summary>
    /// <typeparam name="T">The type of the item to retrieve.</typeparam>
    /// <param name="key">The unique cache key.</param>
    /// <returns>The cached item or null if it does not exist.</returns>
    T? Get<T>(string key);

    /// <summary>Asynchronously retrieve an item from the cache by its unique key.</summary>
    /// <typeparam name="T">The type of the item to retrieve.</typeparam>
    /// <param name="key">The unique cache key.</param>
    /// <param name="ct">A token to cancel the asynchronous operation.</param>
    /// <returns>A task representing the asynchronous operation, containing the cached item or null.</returns>
    Task<T?> GetAsync<T>(string key, CancellationToken ct = default);

    /// <summary>Retrieve an item from the cache, or return a default value if the item is missing.</summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="key">The unique cache key.</param>
    /// <param name="defaultValue">The value to return if the key is not found.</param>
    /// <returns>The cached item or the provided default value.</returns>
    T? Get<T>(string key, T? defaultValue);

    /// <summary>Asynchronously retrieve an item from the cache, or return a default value if the item is missing.</summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="key">The unique cache key.</param>
    /// <param name="defaultValue">The value to return if the key is not found.</param>
    /// <param name="ct">A token to cancel the asynchronous operation.</param>
    /// <returns>A task containing the cached item or the provided default value.</returns>
    Task<T?> GetAsync<T>(string key, T? defaultValue, CancellationToken ct = default);

    /// <summary>Store an item in the cache for a specific duration.</summary>
    /// <typeparam name="T">The type of the item to store.</typeparam>
    /// <param name="key">The unique cache key.</param>
    /// <param name="value">The object to be cached.</param>
    /// <param name="ttl">Time-to-live. If null, a default system duration is used.</param>
    void Put<T>(string key, T value, TimeSpan? ttl = null);

    /// <summary>Asynchronously store an item in the cache for a specific duration.</summary>
    /// <typeparam name="T">The type of the item to store.</typeparam>
    /// <param name="key">The unique cache key.</param>
    /// <param name="value">The object to be cached.</param>
    /// <param name="ttl">Time-to-live. If null, a default system duration is used.</param>
    /// <param name="ct">A token to cancel the asynchronous operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task PutAsync<T>(string key, T value, TimeSpan? ttl = null, CancellationToken ct = default);

    /// <summary>Store an item in the cache only if it does not already exist.</summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="key">The unique cache key.</param>
    /// <param name="value">The object to be cached.</param>
    /// <param name="ttl">Time-to-live. If null, a default system duration is used.</param>
    /// <returns>True if the item was added; false if it already existed.</returns>
    bool Add<T>(string key, T value, TimeSpan? ttl = null);

    /// <summary>Asynchronously store an item in the cache only if it does not already exist.</summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="key">The unique cache key.</param>
    /// <param name="value">The object to be cached.</param>
    /// <param name="ttl">Time-to-live. If null, a default system duration is used.</param>
    /// <param name="ct">A token to cancel the asynchronous operation.</param>
    /// <returns>A task containing true if the item was added; false if it already existed.</returns>
    Task<bool> AddAsync<T>(string key, T value, TimeSpan? ttl = null, CancellationToken ct = default);

    /// <summary>Store an item in the cache indefinitely.</summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="key">The unique cache key.</param>
    /// <param name="value">The object to be cached.</param>
    void Forever<T>(string key, T value);

    /// <summary>Asynchronously store an item in the cache indefinitely.</summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="key">The unique cache key.</param>
    /// <param name="value">The object to be cached.</param>
    /// <param name="ct">A token to cancel the asynchronous operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task ForeverAsync<T>(string key, T value, CancellationToken ct = default);

    /// <summary>Determine if an item exists in the cache.</summary>
    /// <param name="key">The cache key to check.</param>
    /// <returns>True if the key exists; otherwise, false.</returns>
    bool Has(string key);

    /// <summary>Asynchronously determine if an item exists in the cache.</summary>
    /// <param name="key">The cache key to check.</param>
    /// <param name="ct">A token to cancel the asynchronous operation.</param>
    /// <returns>A task containing true if the key exists; otherwise, false.</returns>
    Task<bool> HasAsync(string key, CancellationToken ct = default);

    /// <summary>Remove an item from the cache by its key.</summary>
    /// <param name="key">The key to remove.</param>
    /// <returns>True if the item was successfully removed.</returns>
    bool Forget(string key);

    /// <summary>Asynchronously remove an item from the cache by its key.</summary>
    /// <param name="key">The key to remove.</param>
    /// <param name="ct">A token to cancel the asynchronous operation.</param>
    /// <returns>A task containing true if the item was successfully removed.</returns>
    Task<bool> ForgetAsync(string key, CancellationToken ct = default);

    /// <summary>Remove all items from the cache.</summary>
    void Flush();

    /// <summary>Asynchronously remove all items from the cache.</summary>
    /// <param name="ct">A token to cancel the asynchronous operation.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task FlushAsync(CancellationToken ct = default);

    /// <summary>
    ///     Get an item from the cache, or execute the callback and store the result if the item is missing. Provides
    ///     access to IServiceProvider within the callback for dependency resolution.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="key">The cache key.</param>
    /// <param name="ttl">Time-to-live for the new cache entry.</param>
    /// <param name="callback">A delegate that receives a service provider and returns the data to cache.</param>
    /// <returns>The cached or freshly generated item.</returns>
    T Remember<T>(string key, TimeSpan? ttl, Func<IServiceProvider, T> callback);

    /// <summary>
    ///     Asynchronously get an item from the cache, or execute the callback and store the result if the item is
    ///     missing.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="key">The cache key.</param>
    /// <param name="ttl">Time-to-live for the new cache entry.</param>
    /// <param name="callback">
    ///     An asynchronous delegate that receives a service provider, a cancellation token, and returns the
    ///     data to cache.
    /// </param>
    /// <param name="ct">A token to cancel the asynchronous operation.</param>
    /// <returns>A task containing the cached or freshly generated item.</returns>
    Task<T> RememberAsync<T>(string key, TimeSpan? ttl, Func<IServiceProvider, CancellationToken, Task<T>> callback, CancellationToken ct = default);

    /// <summary>Get an item from the cache, or store the callback result indefinitely if the item is missing.</summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="key">The cache key.</param>
    /// <param name="callback">A delegate that receives a service provider and returns the data to cache.</param>
    /// <returns>The cached or freshly generated item.</returns>
    T RememberForever<T>(string key, Func<IServiceProvider, T> callback);

    /// <summary>Asynchronously get an item from the cache, or store the callback result indefinitely if the item is missing.</summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="key">The cache key.</param>
    /// <param name="callback">
    ///     An asynchronous delegate that receives a service provider, a cancellation token, and returns the
    ///     data to cache.
    /// </param>
    /// <param name="ct">A token to cancel the asynchronous operation.</param>
    /// <returns>A task containing the cached or freshly generated item.</returns>
    Task<T> RememberForeverAsync<T>(string key, Func<IServiceProvider, CancellationToken, Task<T>> callback, CancellationToken ct = default);

    /// <summary>Retrieve an item from the cache and then delete it.</summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="key">The unique cache key.</param>
    /// <returns>The cached item before its removal, or null.</returns>
    T? Pull<T>(string key);

    /// <summary>Asynchronously retrieve an item from the cache and then delete it.</summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    /// <param name="key">The unique cache key.</param>
    /// <param name="ct">A token to cancel the asynchronous operation.</param>
    /// <returns>A task containing the cached item before its removal, or null.</returns>
    Task<T?> PullAsync<T>(string key, CancellationToken ct = default);
}
