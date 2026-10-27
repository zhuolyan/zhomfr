# Abstractions for Unified Work with Storage.

[![NuGet Status](https://img.shields.io/nuget/v/Zhomfr.Storage.Abstractions.svg)](https://www.nuget.org/packages/Zhomfr.Storage.Abstractions/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Zhomfr.Storage.Abstractions.svg)](https://www.nuget.org/packages/Zhomfr.Storage.Abstractions/)

> ### ⚠️ Warning: Active Development (< 1.0.0)
> Prior to the release of stable version `1.0.0`, this library follows SemVer for initial development. This means
> **breaking changes** may occur even in minor releases (`0.x`).
>
> We strongly recommend pinning the package version in your `.csproj` to avoid unexpected build errors.

A lightweight and flexible .NET library providing a unified contract for storage functions. It completely decouples your
business logic from specific storage driver or third-party implementations.

## 💡 Core Concept

Your code interacts exclusively with a standardized interface. The underlying drivers is determined at the dependency
injection composition root. If you ever need to change the provider tomorrow, you won't need to rewrite
your business logic.

## Available Drivers:

* Local see [GitHub](https://github.com/zhuolyan/zhomfr/tree/master/Storage.Local)
  [NuGet](https://www.nuget.org/packages/Zhomfr.Storage.Local/)

* Ftp see [GitHub](https://github.com/zhuolyan/zhomfr/tree/master/Storage.Ftp)
  [NuGet](https://www.nuget.org/packages/Zhomfr.Storage.Ftp/)

* SFtp see [GitHub](https://github.com/zhuolyan/zhomfr/tree/master/Storage.SFtp)
  [NuGet](https://www.nuget.org/packages/Zhomfr.Storage.SFtp/)

* S3 see [GitHub](https://github.com/zhuolyan/zhomfr/tree/master/Storage.S3)
  [NuGet](https://www.nuget.org/packages/Zhomfr.Storage.S3/)

## Development Custom Driver

### 📦 Installation

Install package:

```shell
dotnet add package Zhomfr.Storage.Abstractions
```

For example, you can use code of available drivers in GitHub.

### Configuration Convention

All settings are located in `appsettings.json` under the `Zhomfr:Storage` section:

* The **default service** uses the `Zhomfr:Storage:Default` subsection.
* **Keyed services** use subsections where the name matches the service key, resulting in `Zhomfr:Storage:KeyName`.

For instance, given the following registrations:

```csharp
builder.Services.AddDefaultStorage<MyAwesomeStorage, MyAwesomeStorageConfig>();
builder.Services.AddKeyedStorage<MyAwesomeStorage, MyAwesomeStorageConfig>("AwesomeKey");
```

Your `appsettings.json` file should follow this structure:

```json
{
  "Zhomfr": {
    "Storage": {
      "Default": {
        "BaseUrl": "http://my-awesome.com/storage",
        "RootDir": "/storage/root/dir",
        // ... other configuration properties
      },
      "AwesomeKey": {
        "BaseUrl": "http://my-awesome.com/storage",
        "RootDir": "/storage/root/dir",
        // ... other configuration properties
      }
    }
  }
}
```

[IConfig](#iconfig)

[IDirectoryManager](#idirectorymanager)

[IDirectoryScanner](#idirectoryscanner)

[IFileManager](#ifilemanager)

[IFileSystemMetadata](#ifilesystemmetadata)

[IFileSystemState](#ifilesystemstate)

[IFileSystemTypeInspector](#ifilesystemtypeinspector)

[IStorage](#istorage)

# IConfig

Namespace: Zhomfr.Storage.Abstractions

Abstract configuration for storage driver.

```csharp
public interface IConfig
```

# IDirectoryManager

Namespace: Zhomfr.Storage.Abstractions

Manages directories with support for synchronous and asynchronous operations.

```csharp
public interface IDirectoryManager
```

Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute)

## Methods

### **MakeDirectory (String, Boolean)**

Create a directory.

```csharp
bool MakeDirectory(string path, bool recursive = true)
```

#### Parameters

`path` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The directory path.

`recursive` [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether to create parent directories.

#### Returns

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True on success, false on failure.

### **MakeDirectoryAsync (String, Boolean, CancellationToken)**

Asynchronously create a directory.

```csharp
Task<bool> MakeDirectoryAsync(string path, bool recursive = true, CancellationToken ct = null)
```

#### Parameters

`path` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The directory path.

`recursive` [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether to create parent directories.

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
The cancellation token.

#### Returns

[Task&lt;Boolean&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
A task representing the operation success.

### **DeleteDirectory (String)**

Recursively delete a directory.

```csharp
bool DeleteDirectory(string directory)
```

#### Parameters

`directory` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The directory path.

#### Returns

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True on success, false on failure.

### **DeleteDirectoryAsync (String, CancellationToken)**

Asynchronously delete a directory.

```csharp
Task<bool> DeleteDirectoryAsync(string directory, CancellationToken ct = null)
```

#### Parameters

`directory` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The directory path.

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
The cancellation token.

#### Returns

[Task&lt;Boolean&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
A task representing the operation success.

### **CleanDirectory (String)**

Empty the specified directory of all files and folders.

```csharp
bool CleanDirectory(string directory)
```

#### Parameters

`directory` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The directory path.

#### Returns

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True on success, false on failure.

### **CleanDirectoryAsync (String, CancellationToken)**

Asynchronously empty the specified directory.

```csharp
Task<bool> CleanDirectoryAsync(string directory, CancellationToken ct = null)
```

#### Parameters

`directory` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The directory path.

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
The cancellation token.

#### Returns

[Task&lt;Boolean&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
A task representing the operation success.

### **IsEmptyDirectory (String)**

Determine if the given path is an empty directory.

```csharp
bool IsEmptyDirectory(string directory)
```

#### Parameters

`directory` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The directory path.

#### Returns

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if the directory is empty, false otherwise.

### **IsEmptyDirectoryAsync (String, CancellationToken)**

Asynchronously determine if the given path is an empty directory.

```csharp
Task<bool> IsEmptyDirectoryAsync(string directory, CancellationToken ct = null)
```

#### Parameters

`directory` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The directory path.

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
The cancellation token.

#### Returns

[Task&lt;Boolean&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
A task representing whether the directory is empty.

# IDirectoryScanner

Namespace: Zhomfr.Storage.Abstractions

Scan directories with support for synchronous and asynchronous operations.

```csharp
public interface IDirectoryScanner
```

Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute)

## Methods

### **Files (String, Boolean)**

Get files in the given directory.

```csharp
IEnumerable<string> Files(string directory, bool hidden = false)
```

#### Parameters

`directory` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The directory path.

`hidden` [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether to include hidden files.

#### Returns

[IEnumerable&lt;String&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
A file paths enumerable.

### **FilesAsync (String, Boolean, CancellationToken)**

Asynchronously get files in the given directory.

```csharp
IAsyncEnumerable<string> FilesAsync(string directory, bool hidden = false, CancellationToken ct = null)
```

#### Parameters

`directory` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The directory path.

`hidden` [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether to include hidden files.

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
The cancellation token.

#### Returns

[IAsyncEnumerable&lt;String&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.iasyncenumerable-1)<br>
An asynchronous enumerable of file paths.

### **AllFiles (String, Boolean)**

Get all files from the given directory recursively.

```csharp
IEnumerable<string> AllFiles(string directory, bool hidden = false)
```

#### Parameters

`directory` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The directory path.

`hidden` [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether to include hidden files.

#### Returns

[IEnumerable&lt;String&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
All recursive file paths enumerable.

### **AllFilesAsync (String, Boolean, CancellationToken)**

Asynchronously get all files from the given directory recursively.

```csharp
IAsyncEnumerable<string> AllFilesAsync(string directory, bool hidden = false, CancellationToken ct = null)
```

#### Parameters

`directory` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The directory path.

`hidden` [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether to include hidden files.

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
The cancellation token.

#### Returns

[IAsyncEnumerable&lt;String&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.iasyncenumerable-1)<br>
An asynchronous enumerable of all recursive file paths.

### **Directories (String, Boolean)**

Get directories within a given directory.

```csharp
IEnumerable<string> Directories(string directory, bool hidden = false)
```

#### Parameters

`directory` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The directory path.

`hidden` [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether to include hidden files.

#### Returns

[IEnumerable&lt;String&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
Directory paths enumerable.

### **DirectoriesAsync (String, Boolean, CancellationToken)**

Asynchronously iterate through directories in the given directory.

```csharp
IAsyncEnumerable<string> DirectoriesAsync(string directory, bool hidden = false, CancellationToken ct = null)
```

#### Parameters

`directory` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The directory path.

`hidden` [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether to include hidden files.

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
The cancellation token.

#### Returns

[IAsyncEnumerable&lt;String&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.iasyncenumerable-1)<br>
An asynchronous enumerable of directory paths.

### **AllDirectories (String, Boolean)**

Get all directories from the given directory recursively.

```csharp
IEnumerable<string> AllDirectories(string directory, bool hidden = false)
```

#### Parameters

`directory` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The directory path.

`hidden` [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether to include hidden files.

#### Returns

[IEnumerable&lt;String&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1)<br>
Directory paths enumerable.

### **AllDirectoriesAsync (String, Boolean, CancellationToken)**

Asynchronously get all directories from the given directory recursively.

```csharp
IAsyncEnumerable<string> AllDirectoriesAsync(string directory, bool hidden = false, CancellationToken ct = null)
```

#### Parameters

`directory` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The directory path.

`hidden` [Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
Whether to include hidden files.

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
The cancellation token.

#### Returns

[IAsyncEnumerable&lt;String&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.iasyncenumerable-1)<br>
An asynchronous enumerable of directory paths.

# IFileManager

Namespace: Zhomfr.Storage.Abstractions

Provides high-level file management operations including moving, copying, and deleting files. All operations
support both synchronous and asynchronous execution for local and remote storage compatibility.

```csharp
public interface IFileManager
```

Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute)

## Methods

### **Get (String)**

Retrieve a file from the storage at the given path.

```csharp
SmartFile? Get(string path)
```

#### Parameters

`path` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The path to the file.

#### Returns

SmartFile?<br>
An `Zhomfr.Helpers.SmartFile.SmartFile` instance for data access.

### **GetAsync (String, CancellationToken)**

Asynchronously retrieve a file from the storage.

```csharp
Task<SmartFile?> GetAsync(string path, CancellationToken ct = null)
```

#### Parameters

`path` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The path to the file.

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
The cancellation token.

#### Returns

[Task&lt;SmartFile&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
A task representing the retrieved `Zhomfr.Helpers.SmartFile.SmartFile`.

### **Put (String, SmartFile)**

Write an `Zhomfr.Helpers.SmartFile.SmartFile` object to the specified path. This method consumes the stream from the
provided file object.

```csharp
bool Put(string path, SmartFile storageFile)
```

#### Parameters

`path` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The destination path.

`storageFile` SmartFile<br>
The file object to persist.

#### Returns

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if the file was stored successfully.

### **PutAsync (String, SmartFile, CancellationToken)**

Asynchronously write an `Zhomfr.Helpers.SmartFile.SmartFile` object to the specified path.

```csharp
Task<bool> PutAsync(string path, SmartFile storageFile, CancellationToken ct = null)
```

#### Parameters

`path` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The destination path.

`storageFile` SmartFile<br>
The file object to persist.

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
The cancellation token.

#### Returns

[Task&lt;Boolean&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
A task representing the operation success.

### **Delete (String)**

Delete the file at the given path.

```csharp
bool Delete(string path)
```

#### Parameters

`path` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The path to the file to be deleted.

#### Returns

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if the file was successfully deleted, false otherwise.

### **DeleteAsync (String, CancellationToken)**

Asynchronously delete the file at the given path.

```csharp
Task<bool> DeleteAsync(string path, CancellationToken ct = null)
```

#### Parameters

`path` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The path to the file to be deleted.

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
The cancellation token to cancel the operation.

#### Returns

[Task&lt;Boolean&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
A task representing the operation success.

### **Move (String, String)**

Move a file to a new location.

```csharp
bool Move(string path, string target)
```

#### Parameters

`path` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The current source path of the file.

`target` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The destination path where the file should be moved.

#### Returns

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if the file was successfully moved, false otherwise.

### **MoveAsync (String, String, CancellationToken)**

Asynchronously move a file to a new location.

```csharp
Task<bool> MoveAsync(string path, string target, CancellationToken ct = null)
```

#### Parameters

`path` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The current source path of the file.

`target` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The destination path where the file should be moved.

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
The cancellation token to cancel the operation.

#### Returns

[Task&lt;Boolean&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
A task representing the operation success.

### **Copy (String, String)**

Copy a file to a new location.

```csharp
bool Copy(string path, string target)
```

#### Parameters

`path` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The source path of the file.

`target` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The destination path where the file should be copied.

#### Returns

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if the file was successfully copied, false otherwise.

### **CopyAsync (String, String, CancellationToken)**

Asynchronously copy a file to a new location.

```csharp
Task<bool> CopyAsync(string path, string target, CancellationToken ct = null)
```

#### Parameters

`path` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The source path of the file.

`target` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The destination path where the file should be copied.

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
The cancellation token to cancel the operation.

#### Returns

[Task&lt;Boolean&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
A task representing the operation success.

# IFileSystemMetadata

Namespace: Zhomfr.Storage.Abstractions

Provides methods for inspecting metadata of files and directories. Supports synchronous and asynchronous
execution for local and remote storage compatibility.

```csharp
public interface IFileSystemMetadata
```

Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute)

## Methods

### **Size (String)**

Gets the total logical size of the file (s) at the specified path in bytes.

```csharp
long Size(string path)
```

#### Parameters

`path` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The path to the file or directory.

#### Returns

[Int64](https://learn.microsoft.com/en-us/dotnet/api/system.int64)<br>
The total size in bytes based on the content length.

**Remarks:**

This returns the actual length of the file content (the sum of raw data bytes). It does NOT reflect the "Size
on disk," as it excludes file system metadata, directory structures (MFT/inodes), and physical allocation overhead
such as cluster padding, disk compression, or sparse file optimization.

### **SizeAsync (String, CancellationToken)**

Asynchronously gets the total logical size of the file (s) at the specified path in bytes.

```csharp
Task<long> SizeAsync(string path, CancellationToken ct = null)
```

#### Parameters

`path` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The path to the file or directory.

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
The cancellation token.

#### Returns

[Task&lt;Int64&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
The total size in bytes based on the content length.

**Remarks:**

This returns the actual length of the file content (the sum of raw data bytes). It does NOT reflect the "Size
on disk," as it excludes file system metadata, directory structures (MFT/inodes), and physical allocation overhead
such as cluster padding, disk compression, or sparse file optimization.

### **CreatedAt (String)**

Get creation time of file or directory.

```csharp
DateTimeOffset CreatedAt(string path)
```

#### Parameters

`path` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The file or directory path.

#### Returns

[DateTimeOffset](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset)<br>
A [DateTime](https://learn.microsoft.com/en-us/dotnet/api/system.datetime) representing created time.

**Remarks:**

⚠️ WARNING: File creation and modification timestamps depend entirely on the underlying storage driver
and server filesystem capabilities (e.g., remote FTP/SFTP often lacks reliable creation time or birthtime
support). Use dedicated metadata storage if strict timestamp consistency is required.

### **CreatedAtAsync (String, CancellationToken)**

Asynchronously get creation time of file or directory.

```csharp
Task<DateTimeOffset> CreatedAtAsync(string path, CancellationToken ct = null)
```

#### Parameters

`path` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The file or directory path.

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
The cancellation token.

#### Returns

[Task&lt;DateTimeOffset&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
A task representing the created time.

**Remarks:**

⚠️ WARNING: File creation and modification timestamps depend entirely on the underlying storage driver
and server filesystem capabilities (e.g., remote FTP/SFTP often lacks reliable creation time or birthtime
support). Use dedicated metadata storage if strict timestamp consistency is required.

### **LastModified (String)**

Get last modification time of file or directory.

```csharp
DateTimeOffset LastModified(string path)
```

#### Parameters

`path` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The file or directory path.

#### Returns

[DateTimeOffset](https://learn.microsoft.com/en-us/dotnet/api/system.datetimeoffset)<br>
A [DateTime](https://learn.microsoft.com/en-us/dotnet/api/system.datetime) representing last modification time.

**Remarks:**

⚠️ WARNING: File creation and modification timestamps depend entirely on the underlying storage driver
and server filesystem capabilities (e.g., remote FTP/SFTP often lacks reliable creation time or birthtime
support). Use dedicated metadata storage if strict timestamp consistency is required.

### **LastModifiedAsync (String, CancellationToken)**

Asynchronously get last modification time of file or directory.

```csharp
Task<DateTimeOffset> LastModifiedAsync(string path, CancellationToken ct = null)
```

#### Parameters

`path` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The file or directory path.

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
The cancellation token.

#### Returns

[Task&lt;DateTimeOffset&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
A task representing the last modification time.

**Remarks:**

⚠️ WARNING: File creation and modification timestamps depend entirely on the underlying storage driver
and server filesystem capabilities (e.g., remote FTP/SFTP often lacks reliable creation time or birthtime
support). Use dedicated metadata storage if strict timestamp consistency is required.

# IFileSystemState

Namespace: Zhomfr.Storage.Abstractions

Provides methods for inspecting state of files and directories. Supports synchronous and asynchronous
execution for local and remote storage compatibility.

```csharp
public interface IFileSystemState
```

Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute)

## Methods

### **Exists (String)**

Determine if file or directory exists at the given path.

```csharp
bool Exists(string path)
```

#### Parameters

`path` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The path to check.

#### Returns

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if path exists, false otherwise.

### **ExistsAsync (String, CancellationToken)**

Asynchronously determine if file or directory exists at the given path.

```csharp
Task<bool> ExistsAsync(string path, CancellationToken ct = null)
```

#### Parameters

`path` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The path to check.

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
The cancellation token.

#### Returns

[Task&lt;Boolean&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
A task representing whether path exists.

### **Missing (String)**

Determine if file or directory is missing at the given path.

```csharp
bool Missing(string path)
```

#### Parameters

`path` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The path to check.

#### Returns

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if path does not exist, false otherwise.

### **MissingAsync (String, CancellationToken)**

Asynchronously determine if file or directory is missing at the given path.

```csharp
Task<bool> MissingAsync(string path, CancellationToken ct = null)
```

#### Parameters

`path` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The path to check.

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
The cancellation token.

#### Returns

[Task&lt;Boolean&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
A task representing whether path is missing.

# IFileSystemTypeInspector

Namespace: Zhomfr.Storage.Abstractions

Provides methods for inspecting type of filesystem objects. Supports synchronous and asynchronous execution
for local and remote storage compatibility.

```csharp
public interface IFileSystemTypeInspector
```

Attributes [NullableContextAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.runtime.compilerservices.nullablecontextattribute)

## Methods

### **IsDirectory (String)**

Determine if the given path is a directory.

```csharp
bool IsDirectory(string path)
```

#### Parameters

`path` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The path to check.

#### Returns

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if path is a directory, false otherwise.

### **IsDirectoryAsync (String, CancellationToken)**

Asynchronously determine if the given path is a directory.

```csharp
Task<bool> IsDirectoryAsync(string path, CancellationToken ct = null)
```

#### Parameters

`path` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The path to check.

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
The cancellation token.

#### Returns

[Task&lt;Boolean&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
A task representing whether path is a directory.

### **IsFile (String)**

Determine if the given path is a file.

```csharp
bool IsFile(string path)
```

#### Parameters

`path` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The path to check.

#### Returns

[Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean)<br>
True if path is a file, false otherwise.

### **IsFileAsync (String, CancellationToken)**

Asynchronously determine if the given path is a file.

```csharp
Task<bool> IsFileAsync(string path, CancellationToken ct = null)
```

#### Parameters

`path` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The path to check.

`ct` [CancellationToken](https://learn.microsoft.com/en-us/dotnet/api/system.threading.cancellationtoken)<br>
The cancellation token.

#### Returns

[Task&lt;Boolean&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1)<br>
A task representing whether path is a file.

# IStorage

Namespace: Zhomfr.Storage.Abstractions

A unified filesystem contract that aggregates specific functional interfaces. Provides a Laravel-like API.

```csharp
public interface IStorage : IDirectoryManager, IDirectoryScanner, IFileManager, IFileSystemMetadata, IFileSystemState, IFileSystemTypeInspector
```

Implements [IDirectoryManager](./zhomfr.storage.abstractions.idirectorymanager.md), [IDirectoryScanner](./zhomfr.storage.abstractions.idirectoryscanner.md), [IFileManager](./zhomfr.storage.abstractions.ifilemanager.md), [IFileSystemMetadata](./zhomfr.storage.abstractions.ifilesystemmetadata.md), [IFileSystemState](./zhomfr.storage.abstractions.ifilesystemstate.md), [IFileSystemTypeInspector](./zhomfr.storage.abstractions.ifilesystemtypeinspector.md)

## Fields

### **Default**

Default datetime if object not exists.

```csharp
public static DateTimeOffset Default;
```

### **DIRECTORY_SEPARATOR**

Unified Path Separator: The contract uses a standardized path separator format. The underlying driver
will automatically translate it to match the target filesystem. Using Windows-specific separators (`\`) may
lead to unpredictable behavior.

```csharp
public static string DIRECTORY_SEPARATOR;
```

## Methods

### **GetUrl (String)**

Gets the full public URL for the file at the given path.

```csharp
string? GetUrl(string path)
```

#### Parameters

`path` [String](https://learn.microsoft.com/en-us/dotnet/api/system.string)<br>
The relative path to the file.

#### Returns

[String](https://learn.microsoft.com/en-us/dotnet/api/system.string)?<br>
A string representing the public URL of the file.

