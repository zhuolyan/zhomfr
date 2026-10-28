using Zhomfr.Helpers.SmartFile;
using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Tests.Stubs;

public class StubStorage(StubConfig config) : IStorage
{
    public StubConfig Config => config;

    /// <inheritdoc/>
    public string? GetUrl(string path)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public bool MakeDirectory(string path, bool recursive = true)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<bool> MakeDirectoryAsync(string path, bool recursive = true, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public bool DeleteDirectory(string directory)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<bool> DeleteDirectoryAsync(string directory, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public bool CleanDirectory(string directory)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<bool> CleanDirectoryAsync(string directory, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public bool IsEmptyDirectory(string directory)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<bool> IsEmptyDirectoryAsync(string directory, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public IEnumerable<string> Files(string directory, bool hidden = false)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<string> FilesAsync(string directory, bool hidden = false, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public IEnumerable<string> AllFiles(string directory, bool hidden = false)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<string> AllFilesAsync(string directory, bool hidden = false, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public IEnumerable<string> Directories(string directory, bool hidden = false)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<string> DirectoriesAsync(string directory, bool hidden = false, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public IEnumerable<string> AllDirectories(string directory, bool hidden = false)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public IAsyncEnumerable<string> AllDirectoriesAsync(string directory, bool hidden = false, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public SmartFile? Get(string path)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<SmartFile?> GetAsync(string path, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public bool Put(string path, SmartFile storageFile)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<bool> PutAsync(string path, SmartFile storageFile, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public bool Delete(string path)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<bool> DeleteAsync(string path, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public bool Move(string path, string target)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<bool> MoveAsync(string path, string target, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public bool Copy(string path, string target)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<bool> CopyAsync(string path, string target, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public long Size(string path)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<long> SizeAsync(string path, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public DateTimeOffset CreatedAt(string path)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<DateTimeOffset> CreatedAtAsync(string path, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public DateTimeOffset LastModified(string path)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<DateTimeOffset> LastModifiedAsync(string path, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public bool Exists(string path)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<bool> ExistsAsync(string path, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public bool Missing(string path)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<bool> MissingAsync(string path, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public bool IsHidden(string path)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<bool> IsHiddenAsync(string path, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public bool IsVisible(string path)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<bool> IsVisibleAsync(string path, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public bool MakeHidden(string path)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<bool> MakeHiddenAsync(string path, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public bool MakeVisible(string path)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<bool> MakeVisibleAsync(string path, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public bool IsDirectory(string path)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<bool> IsDirectoryAsync(string path, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public bool IsFile(string path)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public Task<bool> IsFileAsync(string path, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
    }
}
