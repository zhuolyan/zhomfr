# Validation Rules for Files

[![NuGet Status](https://img.shields.io/nuget/v/Zhomfr.Validation.Rules.Files.svg)](https://www.nuget.org/packages/Zhomfr.Validation.Rules.Files/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Zhomfr.Validation.Rules.Files.svg)](https://www.nuget.org/packages/Zhomfr.Validation.Rules.Files/)

> ### ⚠️ Warning: Active Development (< 1.0.0)
> Prior to the release of stable version `1.0.0`, this library follows SemVer for initial development. This means
> **breaking changes** may occur even in minor releases (`0.x`).
>
> We strongly recommend pinning the package version in your `.csproj` to avoid unexpected build errors.

A powerful, intuitive collection of custom validation attributes built directly on top of the standard .NET
`System.ComponentModel.DataAnnotations` infrastructure. Designed to eliminate boilerplate code, this library brings a
rich set of production-ready validation rules—inspired by robust ecosystem standards (like Laravel's validation
features)
— allowing you to validate models with SmartFiles cleanly using declarative attributes without reinventing the wheel.

## Key Features

* **Standard DataAnnotations Integration:** Seamlessly works with ASP.NET Core and standard
  `Validator` methods out of the box.
* **Rich Set of Built-in Rules:** Covers frequent validation scenarios that are missing from standard .NET, preventing
  you from writing repetitive custom validators over and over again.
* **Intuitive Declarative Syntax:** Clean and expressive attributes to keep your domain models and DTOs readable and
  maintainable.
* **Customizable Error Messages:** Fully supports localized and custom error message templates via standard
  `ErrorMessage` properties.

## 📦 Installation

Install package:

```shell
dotnet add package Zhomfr.Validation.Rules.Files
```

After add namespace in using section:

```csharp
using Zhomfr.Validation.Rules.Files;
```

---

## Available Methods

{{TABLE}}

## 🛠️ Features and Examples

{{FEATURES}}