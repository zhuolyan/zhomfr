using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Local;

/// <inheritdoc cref="IFileSystemTypeInspector"/>
public partial class LocalStorage : IFileSystemTypeInspector
{
    /// <inheritdoc/>
    public bool IsDirectory(string path)
    {
        path = this.GetRealPath(path);

        return Directory.Exists(path);
    }

    /// <inheritdoc/>
    public Task<bool> IsDirectoryAsync(string path, CancellationToken ct = default)
    {
        return Task.FromResult(this.IsDirectory(path));
    }

    /// <inheritdoc/>
    public bool IsFile(string path)
    {
        path = this.GetRealPath(path);

        return File.Exists(path);
    }

    /// <inheritdoc/>
    public Task<bool> IsFileAsync(string path, CancellationToken ct = default)
    {
        return Task.FromResult(this.IsFile(path));
    }
}
