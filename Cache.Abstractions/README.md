# Abstractions for Unified Work with Cache.

[![NuGet Status](https://img.shields.io/nuget/v/Zhomfr.Cache.Abstractions.svg)](https://www.nuget.org/packages/Zhomfr.Cache.Abstractions/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Zhomfr.Cache.Abstractions.svg)](https://www.nuget.org/packages/Zhomfr.Cache.Abstractions/)

> ### ⚠️ Warning: Active Development (< 1.0.0)
> Prior to the release of stable version `1.0.0`, this library follows SemVer for initial development. This means
> **breaking changes** may occur even in minor releases (`0.x`).
>
> We strongly recommend pinning the package version in your `.csproj` to avoid unexpected build errors.

A lightweight and flexible .NET library providing a unified contract for cache functions. It completely decouples your
business logic from specific caching driver or third-party implementations.

## 💡 Core Concept

Your code interacts exclusively with a standardized interface. The underlying drivers is determined at the dependency
injection composition root. If you ever need to change the provider tomorrow, you won't need to rewrite
your business logic.

## Available Drivers:

* InMemory see [GitHub](https://github.com/zhuolyan/zhomfr/tree/master/Cache.InMemory)
[NuGet](https://www.nuget.org/packages/Zhomfr.Cache.InMemory/)

* Redis see [GitHub](https://github.com/zhuolyan/zhomfr/tree/master/Cache.Redis)
  [NuGet](https://www.nuget.org/packages/Zhomfr.Cache.Redis/)

* Database see [GitHub](https://github.com/zhuolyan/zhomfr/tree/master/Cache.Database)
  [NuGet](https://www.nuget.org/packages/Zhomfr.Cache.Database/)

## Development Custom Driver

### 📦 Installation

Install package:

```shell
dotnet add package Zhomfr.Cache.Abstractions
```

For example, you can use code of available drivers in GitHub.

### Configuration Convention

All settings are located in `appsettings.json` under the `Zhomfr:Cache` section:

* The **default service** uses the `Zhomfr:Cache:Default` subsection.
* **Keyed services** use subsections where the name matches the service key, resulting in `Zhomfr:Cache:KeyName`.

For instance, given the following registrations:

```csharp
builder.Services.AddDefaultCache<MyAwesomeCache, MyAwesomeCacheConfig>();
builder.Services.AddKeyedCache<MyAwesomeCache, MyAwesomeCacheConfig>("AwesomeKey");
```

Your `appsettings.json` file should follow this structure:

```json
{
  "Zhomfr": {
    "Cache": {
      "Default": {
        "Prefix": "somePrefixForDefault",
        "Ttl": 10
        // ... other configuration properties
      },
      "AwesomeKey": {
        "Prefix": "somePrefixForAwesomeKey",
        "Ttl": 60
        // ... other configuration properties
      }
    }
  }
}
```

## Abstractions

- [AbstractConfig](#abstractconfig)

- [ICache](#icache)

# AbstractConfig

Namespace: Zhomfr.Cache.Abstractions

Abstract configuration object.

```csharp
public abstract class AbstractConfig
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [AbstractConfig](./zhomfr.cache.abstractions.abstractconfig.md)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

## Properties

### **Prefix**

Cache prefix, needed then you use more than one app for one provider.

```csharp
public abstract string Prefix { get; init; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Ttl**

Default time to life for saved data (in minutes).

```csharp
public abstract int Ttl { get; init; }
```

#### Property Value

[Int32](https://learn.microsoft.com/en-us/dotnet/api/system.int32)<br>

## Constructors

### **AbstractConfig ()**

```csharp
protected AbstractConfig()
```

# ICache

Namespace: Zhomfr.Cache.Abstractions

Defines a unified interface for cache operations, inspired by the Laravel Cache facade. Provides both simple
key-value storage and advanced "remember" patterns.

```csharp
public interface ICache
```

Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute)

## Fields

### **KEY_SEPARATOR**

The cache key separator used to format the full key with a driver prefix.

```csharp
public static string KEY_SEPARATOR;
```

## Methods

### **Get&lt;T&gt; (String)**

Retrieve an item from the cache by its unique key.

```csharp
T Get<T>(string key)
```

#### Type Parameters

`T`<br>
The type of the item to retrieve.

#### Parameters

`key` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The unique cache key.

#### Returns

T<br>
The cached item or null if it does not exist.

### **GetAsync&lt;T&gt; (String, CancellationToken)**

Asynchronously retrieve an item from the cache by its unique key.

```csharp
Task<T> GetAsync<T>(string key, CancellationToken ct = null)
```

#### Type Parameters

`T`<br>
The type of the item to retrieve.

#### Parameters

`key` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The unique cache key.

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
A token to cancel the asynchronous operation.

#### Returns

Task&lt;T&gt;<br>
A task representing the asynchronous operation, containing the cached item or null.

### **Get&lt;T&gt; (String, T)**

Retrieve an item from the cache, or return a default value if the item is missing.

```csharp
T Get<T>(string key, T defaultValue)
```

#### Type Parameters

`T`<br>
The type of the item.

#### Parameters

`key` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The unique cache key.

`defaultValue` T<br>
The value to return if the key is not found.

#### Returns

T<br>
The cached item or the provided default value.

### **GetAsync&lt;T&gt; (String, T, CancellationToken)**

Asynchronously retrieve an item from the cache, or return a default value if the item is missing.

```csharp
Task<T> GetAsync<T>(string key, T defaultValue, CancellationToken ct = null)
```

#### Type Parameters

`T`<br>
The type of the item.

#### Parameters

`key` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The unique cache key.

`defaultValue` T<br>
The value to return if the key is not found.

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
A token to cancel the asynchronous operation.

#### Returns

Task&lt;T&gt;<br>
A task containing the cached item or the provided default value.

### **Put&lt;T&gt; (String, T, TimeSpan?)**

Store an item in the cache for a specific duration.

```csharp
void Put<T>(string key, T value, TimeSpan? ttl = null)
```

#### Type Parameters

`T`<br>
The type of the item to store.

#### Parameters

`key` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The unique cache key.

`value` T<br>
The object to be cached.

`ttl` [TimeSpan?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Time-to-live. If null, a default system duration is used.

### **PutAsync&lt;T&gt; (String, T, TimeSpan?, CancellationToken)**

Asynchronously store an item in the cache for a specific duration.

```csharp
Task PutAsync<T>(string key, T value, TimeSpan? ttl = null, CancellationToken ct = null)
```

#### Type Parameters

`T`<br>
The type of the item to store.

#### Parameters

`key` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The unique cache key.

`value` T<br>
The object to be cached.

`ttl` [TimeSpan?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Time-to-live. If null, a default system duration is used.

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
A token to cancel the asynchronous operation.

#### Returns

[Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>
A task representing the asynchronous operation.

### **Add&lt;T&gt; (String, T, TimeSpan?)**

Store an item in the cache only if it does not already exist.

```csharp
bool Add<T>(string key, T value, TimeSpan? ttl = null)
```

#### Type Parameters

`T`<br>
The type of the item.

#### Parameters

`key` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The unique cache key.

`value` T<br>
The object to be cached.

`ttl` [TimeSpan?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Time-to-live. If null, a default system duration is used.

#### Returns

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if the item was added; false if it already existed.

### **AddAsync&lt;T&gt; (String, T, TimeSpan?, CancellationToken)**

Asynchronously store an item in the cache only if it does not already exist.

```csharp
Task<bool> AddAsync<T>(string key, T value, TimeSpan? ttl = null, CancellationToken ct = null)
```

#### Type Parameters

`T`<br>
The type of the item.

#### Parameters

`key` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The unique cache key.

`value` T<br>
The object to be cached.

`ttl` [TimeSpan?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Time-to-live. If null, a default system duration is used.

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
A token to cancel the asynchronous operation.

#### Returns

[Task&lt;Boolean&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
A task containing true if the item was added; false if it already existed.

### **Forever&lt;T&gt; (String, T)**

Store an item in the cache indefinitely.

```csharp
void Forever<T>(string key, T value)
```

#### Type Parameters

`T`<br>
The type of the item.

#### Parameters

`key` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The unique cache key.

`value` T<br>
The object to be cached.

### **ForeverAsync&lt;T&gt; (String, T, CancellationToken)**

Asynchronously store an item in the cache indefinitely.

```csharp
Task ForeverAsync<T>(string key, T value, CancellationToken ct = null)
```

#### Type Parameters

`T`<br>
The type of the item.

#### Parameters

`key` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The unique cache key.

`value` T<br>
The object to be cached.

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
A token to cancel the asynchronous operation.

#### Returns

[Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>
A task representing the asynchronous operation.

### **Has (String)**

Determine if an item exists in the cache.

```csharp
bool Has(string key)
```

#### Parameters

`key` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The cache key to check.

#### Returns

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if the key exists; otherwise, false.

### **HasAsync (String, CancellationToken)**

Asynchronously determine if an item exists in the cache.

```csharp
Task<bool> HasAsync(string key, CancellationToken ct = null)
```

#### Parameters

`key` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The cache key to check.

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
A token to cancel the asynchronous operation.

#### Returns

[Task&lt;Boolean&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
A task containing true if the key exists; otherwise, false.

### **Forget (String)**

Remove an item from the cache by its key.

```csharp
bool Forget(string key)
```

#### Parameters

`key` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The key to remove.

#### Returns

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if the item was successfully removed.

### **ForgetAsync (String, CancellationToken)**

Asynchronously remove an item from the cache by its key.

```csharp
Task<bool> ForgetAsync(string key, CancellationToken ct = null)
```

#### Parameters

`key` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The key to remove.

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
A token to cancel the asynchronous operation.

#### Returns

[Task&lt;Boolean&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
A task containing true if the item was successfully removed.

### **Flush ()**

Remove all items from the cache.

```csharp
void Flush()
```

### **FlushAsync (CancellationToken)**

Asynchronously remove all items from the cache.

```csharp
Task FlushAsync(CancellationToken ct = null)
```

#### Parameters

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
A token to cancel the asynchronous operation.

#### Returns

[Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task)<br>
A task representing the asynchronous operation.

### **Remember&lt;T&gt; (String, TimeSpan?, Func&lt;IServiceProvider, T&gt;)**

Get an item from the cache, or execute the callback and store the result if the item is missing. Provides
access to IServiceProvider within the callback for dependency resolution.

```csharp
T Remember<T>(string key, TimeSpan? ttl, Func<IServiceProvider, T> callback)
```

#### Type Parameters

`T`<br>
The type of the item.

#### Parameters

`key` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The cache key.

`ttl` [TimeSpan?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Time-to-live for the new cache entry.

`callback` Func&lt;IServiceProvider, T&gt;<br>
A delegate that receives a service provider and returns the data to cache.

#### Returns

T<br>
The cached or freshly generated item.

### **RememberAsync&lt;T&gt; (String, TimeSpan?, Func&lt;IServiceProvider, CancellationToken, Task&lt;T&gt;&gt;,

CancellationToken)**

Asynchronously get an item from the cache, or execute the callback and store the result if the item is
missing.

```csharp
Task<T> RememberAsync<T>(string key, TimeSpan? ttl, Func<IServiceProvider, CancellationToken, Task<T>> callback, CancellationToken ct = null)
```

#### Type Parameters

`T`<br>
The type of the item.

#### Parameters

`key` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The cache key.

`ttl` [TimeSpan?](https://learn.microsoft.com/en-us/dotnet/api/system.nullable-1)<br>
Time-to-live for the new cache entry.

`callback` Func&lt;IServiceProvider, CancellationToken, Task&lt;T&gt;&gt;<br>
An asynchronous delegate that receives a service provider, a cancellation token, and returns the
data to cache.

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
A token to cancel the asynchronous operation.

#### Returns

Task&lt;T&gt;<br>
A task containing the cached or freshly generated item.

### **RememberForever&lt;T&gt; (String, Func&lt;IServiceProvider, T&gt;)**

Get an item from the cache, or store the callback result indefinitely if the item is missing.

```csharp
T RememberForever<T>(string key, Func<IServiceProvider, T> callback)
```

#### Type Parameters

`T`<br>
The type of the item.

#### Parameters

`key` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The cache key.

`callback` Func&lt;IServiceProvider, T&gt;<br>
A delegate that receives a service provider and returns the data to cache.

#### Returns

T<br>
The cached or freshly generated item.

### **RememberForeverAsync&lt;T&gt; (String, Func&lt;IServiceProvider, CancellationToken, Task&lt;T&gt;&gt;,

CancellationToken)**

Asynchronously get an item from the cache, or store the callback result indefinitely if the item is missing.

```csharp
Task<T> RememberForeverAsync<T>(string key, Func<IServiceProvider, CancellationToken, Task<T>> callback, CancellationToken ct = null)
```

#### Type Parameters

`T`<br>
The type of the item.

#### Parameters

`key` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The cache key.

`callback` Func&lt;IServiceProvider, CancellationToken, Task&lt;T&gt;&gt;<br>
An asynchronous delegate that receives a service provider, a cancellation token, and returns the
data to cache.

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
A token to cancel the asynchronous operation.

#### Returns

Task&lt;T&gt;<br>
A task containing the cached or freshly generated item.

### **Pull&lt;T&gt; (String)**

Retrieve an item from the cache and then delete it.

```csharp
T Pull<T>(string key)
```

#### Type Parameters

`T`<br>
The type of the item.

#### Parameters

`key` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The unique cache key.

#### Returns

T<br>
The cached item before its removal, or null.

### **PullAsync&lt;T&gt; (String, CancellationToken)**

Asynchronously retrieve an item from the cache and then delete it.

```csharp
Task<T> PullAsync<T>(string key, CancellationToken ct = null)
```

#### Type Parameters

`T`<br>
The type of the item.

#### Parameters

`key` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The unique cache key.

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
A token to cancel the asynchronous operation.

#### Returns

Task&lt;T&gt;<br>
A task containing the cached item before its removal, or null.
