# Helpers for File Streams

[![NuGet Status](https://img.shields.io/nuget/v/Zhomfr.Helpers.SmartFile.svg)](https://www.nuget.org/packages/Zhomfr.Helpers.SmartFile/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Zhomfr.Helpers.SmartFile.svg)](https://www.nuget.org/packages/Zhomfr.Helpers.SmartFile/)

> ### ⚠️ Warning: Active Development (< 1.0.0)
> Prior to the release of stable version `1.0.0`, this library follows SemVer for initial development. This means
> **breaking changes** may occur even in minor releases (`0.x`).
>
> We strongly recommend pinning the package version in your `.csproj` to avoid unexpected build errors.

Helpers for working with file streams. For detection of mimetype used libmagic.

## 📦 Installation

Install package:

```shell
dotnet add package Zhomfr.Helpers.SmartFile
```

After add namespace in using section:

```csharp
using Zhomfr.Helpers.SmartFile;
```

### If you need automatic bind

```csharp
using Zhomfr.Helpers.SmartFile;

builder.Services.AddControllers(options =>
{
    options.ModelBinderProviders.Add(new SmartFileModelBinderProvider());
});
```

# SmartFile

Namespace: Zhomfr.Helpers.SmartFile

Represents a self-contained file object that manages its own data access. Operates on top of a
[Stream](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream) to provide high-level content manipulation.

```csharp
public class SmartFile : System.IDisposable, System.IAsyncDisposable
```

Inheritance [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object) → [SmartFile](./zhomfr.helpers.smartfile.smartfile.md)<br>
Implements [IDisposable](https://learn.microsoft.com/en-us/dotnet/api/system.idisposable), [IAsyncDisposable](https://learn.microsoft.com/en-us/dotnet/api/system.iasyncdisposable)<br>
Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute), [NullableAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullableattribute)

**Remarks:**

Lifecycle Management: When this object is provided via dependency injection or a Model Binder in
ASP.NET Core, it is typically registered for automatic disposal at the end of the HTTP request. In such cases,
you do not need to call [SmartFile.DisposeAsync ()](./zhomfr.helpers.smartfile.smartfile.md#disposeasync) manually.

Manual Usage: If you instantiate this class manually outside a managed scope (e.g., in a background
task or a console application), you must ensure it is disposed of to release underlying resources and
delete any temporary buffering files.

## Properties

### **FileName**

Gets the name of the file, including the extension.

```csharp
public string FileName { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Extension**

Gets the file extension (e.g., ".png").

**Remarks:**
> **Known Limitation:** `.ico` files may currently be incorrectly identified as generic binary files (`.bin`). This
> issue is planned to be resolved in upcoming releases.

```csharp
public string Extension { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **ContentType**

Gets the MIME type as declared by the source.

```csharp
public string ContentType { get; }
```

#### Property Value

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

### **Length**

Gets the file size in bytes.

```csharp
public long Length { get; }
```

#### Property Value

[Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64)<br>

**Remarks:**

Accessing this property on a non-seekable stream will trigger full buffering to determine the length.

## Constructors

### **SmartFile (Stream, String)**

Initializes a new instance of the [SmartFile](./zhomfr.helpers.smartfile.smartfile.md) class using a data stream.

```csharp
public SmartFile(Stream stream, string fileName)
```

#### Parameters

`stream` [Stream](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream)<br>
The source stream containing file data.

`fileName` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the file including extension.

#### Exceptions

[ArgumentNullException](https://learn.microsoft.com/en-us/dotnet/api/system.argumentnullexception)<br>
Thrown when the stream is null.

### **SmartFile (Byte[], String)**

Initializes a new instance of the [SmartFile](./zhomfr.helpers.smartfile.smartfile.md) class using a byte array.

```csharp
public SmartFile(Byte[] bytes, string fileName)
```

#### Parameters

`bytes` [Byte[]](https://learn.microsoft.com/en-us/dotnet/api/system.byte)<br>
The byte array containing file data.

`fileName` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the file.

### **SmartFile (String, String)**

Initializes a new instance of the [SmartFile](./zhomfr.helpers.smartfile.smartfile.md) class using a string content.

```csharp
public SmartFile(string content, string fileName)
```

#### Parameters

`content` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The string content to be stored as a file (encoded in UTF-8).

`fileName` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the file.

### **SmartFile (Object, String, JsonSerializerOptions)**

Initializes a new instance of the [SmartFile](./zhomfr.helpers.smartfile.smartfile.md) class by serializing an object to
JSON.

```csharp
public SmartFile(object data, string fileName, JsonSerializerOptions? options = null)
```

#### Parameters

`data` [Object](https://learn.microsoft.com/en-us/dotnet/api/system.object)<br>
The object to serialize into the file content.

`fileName` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The name of the file (e.g., "data.json").

`options` [JsonSerializerOptions](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions)?<br>
Optional JSON serializer settings.

## Methods

### **DisposeAsync ()**

```csharp
public async ValueTask DisposeAsync()
```

#### Returns

[ValueTask](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.valuetask)<br>

### **Dispose ()**

```csharp
public void Dispose()
```

### **GetStream ()**

Returns the underlying data stream. Ensures the stream is seekable by transparently buffering it if necessary.

```csharp
public Stream GetStream()
```

#### Returns

[Stream](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream)<br>
A [Stream](https://learn.microsoft.com/en-us/dotnet/api/system.io.stream) positioned at the beginning (0).

#### Exceptions

[ObjectDisposedException](https://learn.microsoft.com/en-us/dotnet/api/system.objectdisposedexception)<br>
If object already disposed.

### **AsString ()**

Reads the entire file content as a UTF-8 string, resetting the stream position to the start.

```csharp
public string AsString()
```

#### Returns

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The file content as a string.

### **AsStringAsync (CancellationToken)**

Asynchronously reads the entire content of the file as a UTF-8 string, resetting the stream position to the
start.

```csharp
public async Task<string> AsStringAsync(CancellationToken ct = null)
```

#### Parameters

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
The cancellation token.

#### Returns

[Task&lt;String&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
A task representing the string content of the file.

### **AsBytes ()**

Reads the entire file content into a byte array, resetting the stream position to the start.

```csharp
public Byte[] AsBytes()
```

#### Returns

[Byte[]](https://learn.microsoft.com/en-us/dotnet/api/system.byte)<br>
A byte array containing the file data.

### **AsBytesAsync (CancellationToken)**

Asynchronously reads the entire content of the file into a byte array, resetting the stream position to the
start.

```csharp
public async Task<Byte[]> AsBytesAsync(CancellationToken ct = null)
```

#### Parameters

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
The cancellation token.

#### Returns

[Task&lt;Byte[]&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
A task representing the byte array of the file data.

### **AsJson&lt;T&gt; (JsonSerializerOptions)**

Deserializes the file content from JSON into the specified type.

```csharp
public T AsJson<T>(JsonSerializerOptions? options = null)
```

#### Type Parameters

`T`<br>
The type to deserialize to.

#### Parameters

`options` [JsonSerializerOptions](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions)?<br>
Optional JSON serializer settings.

#### Returns

T<br>
The deserialized object, or null if deserialization fails.

### **AsJsonAsync&lt;T&gt; (JsonSerializerOptions, CancellationToken)**

Asynchronously deserializes the file content from JSON into the specified type.

```csharp
public async Task<T> AsJsonAsync<T>(JsonSerializerOptions? options = null, CancellationToken ct = null)
```

#### Type Parameters

`T`<br>
The type to deserialize to.

#### Parameters

`options` [JsonSerializerOptions](https://learn.microsoft.com/en-us/dotnet/api/system.text.json.jsonserializeroptions)?<br>
Optional JSON serializer settings.

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
The cancellation token.

#### Returns

Task&lt;T&gt;<br>
A task representing the deserialized object.

### **GetRealMimeType ()**

Performs a deep binary inspection (magic numbers) to determine the actual MIME type.

```csharp
public string GetRealMimeType()
```

#### Returns

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>

**Remarks:**

This method will trigger stream buffering if the underlying stream is non-seekable.

### **GetRealMimeTypeAsync (CancellationToken)**

Asynchronously performs a deep inspection of the file's binary signature.

```csharp
public Task<string> GetRealMimeTypeAsync(CancellationToken ct = null)
```

#### Parameters

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>

#### Returns

[Task&lt;String&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>

### **Hash (HashAlgorithmName)**

Get hash of file.

```csharp
public string Hash(HashAlgorithmName algorithm)
```

#### Parameters

`algorithm` [HashAlgorithmName](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.hashalgorithmname)<br>
The hashing algorithm (e.g., MD5, SHA256).

#### Returns

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The hexadecimal string representation of the file hash.

### **HashAsync (HashAlgorithmName, CancellationToken)**

Asynchronously get hash of a file.

```csharp
public async Task<string> HashAsync(HashAlgorithmName algorithm, CancellationToken ct = null)
```

#### Parameters

`algorithm` [HashAlgorithmName](https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.hashalgorithmname)<br>
The hashing algorithm.

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
The cancellation token.

#### Returns

[Task&lt;String&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
A task representing the hexadecimal file hash.
