# Database Driver for `Zhomfr.Cache.Abstractions`.

[![NuGet Status](https://img.shields.io/nuget/v/Zhomfr.Cache.Database.svg)](https://www.nuget.org/packages/Zhomfr.Cache.Database/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Zhomfr.Cache.Database.svg)](https://www.nuget.org/packages/Zhomfr.Cache.Database/)

> ### ⚠️ Warning: Active Development (< 1.0.0)
> Prior to the release of stable version `1.0.0`, this library follows SemVer for initial development. This means
> **breaking changes** may occur even in minor releases (`0.x`).
>
> We strongly recommend pinning the package version in your `.csproj` to avoid unexpected build errors.

This library provides an Database implementation of the `Zhomfr.Cache.Abstractions` cache contract.

> ### ⚠️ Warning: Unsupported SqLite DB driver.

## 📦 Installation

Install package:

```shell
dotnet add package Zhomfr.Cache.Database
```

After register service in DI:

```csharp
using Zhomfr.Cache.Database;

public class MyAwesomeDbContext(DbContextOptions<MyAwesomeDbContext> options) : DbContext(options)
{
    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyCacheConfiguration();
    }
}

// Program.cs


builder.Services.AddDbContextPool<MyAwesomeDbContext>()

builder.Services.AddDefaultCache<MyAwesomeDbContext>();
builder.Services.AddKeyedCache<MyAwesomeDbContext>("AwesomeKey");
```

## Configuration Convention

All settings are located in `appsettings.json` under the `Zhomfr:Cache` section:

* The **default service** uses the `Zhomfr:Cache:Default` subsection.
* **Keyed services** use subsections where the name matches the service key, resulting in `Zhomfr:Cache:KeyName`.

Your `appsettings.json` file should follow this structure:

```json
{
  "Zhomfr": {
    "Cache": {
      "Default": {
        "Prefix": "somePrefixForDefault",
        "Ttl": 10
      },
      "AwesomeKey": {
        "Prefix": "somePrefixForAwesomeKey",
        "Ttl": 60
      }
    }
  }
}
```

And use in your own code:

```csharp
using Zhomfr.Cache.Abstractions;

public class MyAwesomeClass(ICache cache){}
public class MyAwesomeClass([FromKeyedServices("AwesomeKey")]ICache cache){}
```

> For more details, see the documentation
> for [Nuget](https://www.nuget.org/packages/Zhomfr.Cache.Abstractions/#icache)
> or [GitHub](http://github.com/zhuolyan/zhomfr/tree/master/Cache.Abstractions/#icache).
