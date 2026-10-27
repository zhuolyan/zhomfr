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

<table>
<tr valign="top">
<td>

- [FileBetween](#filebetween)
- [FileDimensions](#filedimensions)
- [FileEncoding](#fileencoding)

</td>
<td>

- [FileExtension](#fileextension)
- [FileImage](#fileimage)
- [FileMax](#filemax)

</td>
<td>

- [FileMime](#filemime)
- [FileMin](#filemin)

</td>
</tr>
</table>

## 🛠️ Features and Examples

### FileBetween

Validates that the file size (in bytes) falls within the specified inclusive range.

```csharp
public class Dto
{
    [FileBetween(3, 5)]
    public SmartFile SomeFileInvalid = new ("http://example.com" ,"some_filename.txt"); // ValidationError

    [FileBetween(3, 5)]
    public string SomeFileValid = "test"; // Success
}
```

### FileDimensions

Validates the dimensions (width and height) of an image file. Supports both exact match and range-based
validation.

```csharp
public class Dto
{
    [FileDimensions(128, 128)]
    public SmartFile SomeFile1;

    [FileDimensions(240, 512, 380, 720)]
    public string SomeFile2;
}
```

### FileEncoding

Validates that the file content matches the specified character encoding. Equivalent to Laravel's 'encoding'
rule (uses strict decoder validation).

```csharp
public class Dto
{
    [FileEncoding("ASCII")]
    public SmartFile SomeFile;
}
```

### FileExtension

The file under validation must have a MIME type corresponding to one of the listed extensions.

```csharp
public class Dto
{
    [FileExtension("jpg", "bmp", "png")]
    public SmartFile SomeFile;
}
```

### FileImage

The file under validation must be an image (bmp, gif, ico, jpeg, png, wbmp, webp).

```csharp
public class Dto
{
    [FileImage()]
    public SmartFile SomeImageFile;
            
    [FileImage(true)]
    public SmartFile SomeSvgFile;
}
```

### FileMax

Validates that the file size (in bytes) does not exceed the specified maximum limit.

```csharp
public class Dto
{
    [FileMax(4_194_304)]
    public SmartFile SomeFile;
}
```

### FileMime

The file under validation must match one of the given MIME types.

```csharp
public class Dto
{
    [FileMime("video/avi", "video/mpeg", "video/quicktime")]
    public SmartFile SomeFile;
}
```

### FileMin

Validates that the file size (in bytes) does not below the specified minimum limit.

```csharp
public class Dto
{
    [FileMin(4_194_304)]
    public SmartFile SomeFile;
}
```

