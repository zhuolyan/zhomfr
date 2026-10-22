# Validation Rules for Database

[![NuGet Status](https://img.shields.io/nuget/v/Zhomfr.Validation.Rules.Database.svg)](https://www.nuget.org/packages/Zhomfr.Validation.Rules.Database/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Zhomfr.Validation.Rules.Database.svg)](https://www.nuget.org/packages/Zhomfr.Validation.Rules.Database/)

A powerful, intuitive collection of custom validation attributes built directly on top of the standard .NET
`System.ComponentModel.DataAnnotations` infrastructure. Designed to eliminate boilerplate code, this library brings a
rich set of production-ready validation rules—inspired by robust ecosystem standards (like Laravel's validation
features)
—allowing you to validate models cleanly using declarative attributes without reinventing the wheel.

## Key Features

* **Standard DataAnnotations Integration:** Seamlessly works with ASP.NET Core, Entity Framework, and standard
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
dotnet add package Zhomfr.Validation.Rules.Database
```

After add namespace in using section:

```csharp
using Zhomfr.Validation.Rules.Database;
```

---

## Available Methods

<table>
<tr valign="top">
<td>

 - [Exist](#exist)
 - [Unique](#unique)

</td>
</tr>
</table>


## 🛠️ Features and Examples

### Exist

Validates that a record with the specified property value exists in the database. Supports any entity type and
property that can be resolved via .


```csharp
public class StubModel
{
    [Exist<StubDbContext,StubEntity>(nameof(StubEntity.Name))]
    public string Name { get; set; } = "user@example.com";
}
```

### Unique

Validates that the value is unique within the specified database table and column. This rule fails if a record
with the same property value already exists.


```csharp
public class StubModel
{
    [Unique<StubDbContext,StubEntity>(nameof(StubEntity.Name))]
    public string Name { get; set; } = "user@example.com";
}
```

