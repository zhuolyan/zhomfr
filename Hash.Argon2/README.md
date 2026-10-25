# Argon 2 Driver for `Zhomfr.Hash.Abstractions`

[![NuGet Status](https://img.shields.io/nuget/v/Zhomfr.Hash.Argon2.svg)](https://www.nuget.org/packages/Zhomfr.Hash.Argon2/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Zhomfr.Hash.Argon2.svg)](https://www.nuget.org/packages/Zhomfr.Hash.Argon2/)

> ### ⚠️ Warning: Active Development (< 1.0.0)
> Prior to the release of stable version `1.0.0`, this library follows SemVer for initial development. This means
> **breaking changes** may occur even in minor releases (`0.x`).
>
> We strongly recommend pinning the package version in your `.csproj` to avoid unexpected build errors.

This library provides an Argon2 implementation of the `Zhomfr.Hash.Abstractions` hashing contract.

## 📦 Installation

Install package:

```shell
dotnet add package Zhomfr.Hash.Argon2
```

After register service in DI:

```csharp
using Zhomfr.Hash.Abstractions;

builder.Services.AddDefaultHash<Argon2Hash, Argon2Config>();
builder.Services.AddKeyedHash<Argon2Hash, Argon2Config>("AwesomeKey");
```

## Configuration Convention

All settings are located in `appsettings.json` under the `Zhomfr:Hash` section:

* The **default service** uses the `Zhomfr:Hash:Default` subsection.
* **Keyed services** use subsections where the name matches the service key, resulting in `Zhomfr:Hash:KeyName`.

Your `appsettings.json` file should follow this structure:

```json
{
  "Zhomfr": {
    "Hash": {
      "Default": {
        "Secret": "some32CharacterAlphaNumericString",
        "Iterations": 10,
        "MemorySize": 65536,
        "DegreeOfParallelism": 4,
        "SaltSize": 16
      },
      "AwesomeKey": {
        "Secret": "some32CharacterAlphaNumericString",
        "Iterations": 10,
        "MemorySize": 65536,
        "DegreeOfParallelism": 4,
        "SaltSize": 16
      }
    }
  }
}
```

And use in your own code:

```csharp
using Zhomfr.Hash.Abstractions;

public class MyAwesomeClass(IHash cache){}
public class MyAwesomeClass([FromKeyedServices("AwesomeKey")]IHash cache){}
```

> For more details, see the documentation
> for [Nuget](https://www.nuget.org/packages/Zhomfr.Hash.Abstractions/#ihash)
> or [GitHub](https://github.com/zhuolyan/zhomfr/tree/master/Hash.Abstractions/#ihash).