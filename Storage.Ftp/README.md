# Ftp Driver for `Zhomfr.Storage.Abstractions`.

[![NuGet Status](https://img.shields.io/nuget/v/Zhomfr.Storage.Ftp.svg)](https://www.nuget.org/packages/Zhomfr.Storage.Ftp/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Zhomfr.Storage.Ftp.svg)](https://www.nuget.org/packages/Zhomfr.Storage.Ftp/)

> ### ⚠️ Warning: Active Development (< 1.0.0)
> Prior to the release of stable version `1.0.0`, this library follows SemVer for initial development. This means
> **breaking changes** may occur even in minor releases (`0.x`).
>
> We strongly recommend pinning the package version in your `.csproj` to avoid unexpected build errors.

This library provides an Ftp implementation of the `Zhomfr.Storage.Abstractions` storage contract.

### 📦 Installation

Install package:

```shell
dotnet add package Zhomfr.Storage.Ftp
```

### Configuration Convention

All settings are located in `appsettings.json` under the `Zhomfr:Storage` section:

* The **default service** uses the `Zhomfr:Storage:Default` subsection.
* **Keyed services** use subsections where the name matches the service key, resulting in `Zhomfr:Storage:KeyName`.

For instance, given the following registrations:

```csharp
builder.Services.AddDefaultStorage<FtpStorage, FtpStorageConfig>();
builder.Services.AddKeyedStorage<FtpStorage, FtpStorageConfig>("AwesomeKey");
```
