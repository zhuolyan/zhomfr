# Abstractions for Unified Work with Hashes.

[![NuGet Status](https://img.shields.io/nuget/v/Zhomfr.Hash.Abstractions.svg)](https://www.nuget.org/packages/Zhomfr.Hash.Abstractions/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Zhomfr.Hash.Abstractions.svg)](https://www.nuget.org/packages/Zhomfr.Hash.Abstractions/)

> ### ⚠️ Warning: Active Development (< 1.0.0)
> Prior to the release of stable version `1.0.0`, this library follows SemVer for initial development. This means
> **breaking changes** may occur even in minor releases (`0.x`).
>
> We strongly recommend pinning the package version in your `.csproj` to avoid unexpected build errors.

A lightweight and flexible .NET library providing a unified contract for hash functions. It completely decouples your
business logic from specific hashing algorithms or third-party implementations.

## 💡 Core Concept

Your code interacts exclusively with a standardized interface. The underlying algorithm is determined at the dependency
injection composition root. If you ever need to change the algorithm or provider tomorrow, you won't need to rewrite
your business logic.

## Configuration Convention

All settings are located in `appsettings.json` under the `Zhomfr:Hash` section:

* The **default service** uses the `Zhomfr:Hash:Default` subsection.
* **Keyed services** use subsections where the name matches the service key, resulting in `Zhomfr:Hash:KeyName`.

For instance, given the following registrations:

```csharp
builder.Services.AddDefaultHash<MyAwesomeHash, MyAwesomeHashConfig>();
builder.Services.AddKeyedHash<MyAwesomeHash, MyAwesomeHashConfig>("AwesomeKey");
```

Your `appsettings.json` file should follow this structure:

```json
{
  "Zhomfr": {
    "Hash": {
      "Default": {
        "Secret": "someSecretForDefault"
        // ... other configuration properties
      },
      "AwesomeKey": {
        "Secret": "someSecretForAwesomeKey"
        // ... other configuration properties
      }
    }
  }
}
```

## Usage

```csharp
using Zhomfr.Hash.Abstractions;

public class MyAwesomeHash : IHash 
{
    // some implementation.
}
public class MyAwesomeHashConfig : AbstractConfig 
{
    [Required]
    public string Secret { get; set; } = string.Empty;
}

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDefaultHash<MyAwesomeHash,MyAwesomeHashConfig>();
builder.Services.AddKeyedHash<MyAwesomeHash,MyAwesomeHashConfig>("AwesomeKey");

public class MyAwesomeService(IHash hash){}
public class MyAwesomeService([FromKeyedServices("AwesomeKey")] IHash hash){}
```

# IHash

Namespace: Zhomfr.Hash.Abstractions

Defines methods for secure password hashing and verification.

```csharp
public interface IHash
```

Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute)

## Methods

### **Make (String)**

Creates a secure hash for the specified password.

```csharp
string Make(string password)
```

#### Parameters

`password` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The plain-text password to be hashed.

#### Returns

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
A string containing the hash along with metadata (version, salt, and algorithm parameters) for subsequent
storage in a database.

### **Check (String, String)**

Verifies that the provided password matches the stored hash.

```csharp
bool Check(string password, string storedHash)
```

#### Parameters

`password` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The plain-text password to verify.

`storedHash` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The full hash string previously generated via the [IHash.Make (String)](./zhomfr.hash.abstractions.ihash.md#makestring)
method.

#### Returns

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if the password is correct; otherwise, false.

**Remarks:**

The implementation should use fixed-time comparison to prevent side-channel timing attacks.

### **NeedsRehash (String)**

Determines if the password hash needs to be updated due to changes in security settings.

```csharp
bool NeedsRehash(string storedHash)
```

#### Parameters

`storedHash` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The current hash from the data store.

#### Returns

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if the system's algorithm parameters (e.g., MemorySize or Iterations) differ from those embedded in the
hash.

**Remarks:**

If this method returns true, it is recommended to re-create the hash
using [IHash.Make (String)](./zhomfr.hash.abstractions.ihash.md#makestring) during the
user's next successful authentication.
