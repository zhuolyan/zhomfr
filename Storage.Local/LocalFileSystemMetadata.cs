using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Local;

/// <inheritdoc cref="IFileSystemMetadata"/>
public partial class LocalStorage : IFileSystemMetadata
{
    /// <inheritdoc/>
    public long Size(string path)
    {
        path = this.GetRealPath(path);

        if (File.Exists(path)) {
            return new FileInfo(path).Length;
        }

        return Directory.Exists(path) ? LocalStorage.GetAllItemsForRealPath(path, true).Select(i => i is FileInfo info ? info.Length : 0).Sum() : 0;
    }

    /// <inheritdoc/>
    public Task<long> SizeAsync(string path, CancellationToken ct = default)
    {
        return Task.FromResult(this.Size(path));
    }

    /// <inheritdoc/>
    public DateTimeOffset CreatedAt(string path)
    {
        path = this.GetRealPath(path);

        return File.Exists(path) ? File.GetCreationTimeUtc(path) : Directory.GetCreationTimeUtc(path);
    }

    /// <inheritdoc/>
    public Task<DateTimeOffset> CreatedAtAsync(string path, CancellationToken ct = default)
    {
        return Task.FromResult(this.CreatedAt(path));
    }

    /// <inheritdoc/>
    public DateTimeOffset LastModified(string path)
    {
        path = this.GetRealPath(path);

        return File.Exists(path) ? File.GetLastWriteTimeUtc(path) : Directory.GetLastWriteTimeUtc(path);
    }

    /// <inheritdoc/>
    public Task<DateTimeOffset> LastModifiedAsync(string path, CancellationToken ct = default)
    {
        return Task.FromResult(this.LastModified(path));
    }
}
