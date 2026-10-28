using System.Runtime.Versioning;

using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Local;

/// <inheritdoc cref="IDirectoryManager"/>
public partial class LocalStorage : IDirectoryManager
{
    /// <inheritdoc/>
    public bool MakeDirectory(string path, bool recursive = true)
    {
        path = this.GetRealPath(path);

        if (Directory.Exists(path)) {
            return false;
        }

        DirectoryInfo? parent = Directory.GetParent(path);

        if (!recursive && parent is not { Exists: true }) {
            return false;
        }

        Directory.CreateDirectory(path);

        return LocalStorage.MakeVisibleRealPath(path);
    }

    /// <inheritdoc/>
    [UnsupportedOSPlatform("windows")]
    public Task<bool> MakeDirectoryAsync(string path, bool recursive = true, CancellationToken ct = default)
    {
        return Task.FromResult(this.MakeDirectory(path, recursive));
    }

    /// <inheritdoc/>
    public bool DeleteDirectory(string directory)
    {
        directory = this.GetRealPath(directory);

        if (!Directory.Exists(directory)) {
            return false;
        }

        Directory.Delete(directory, true);

        return !Directory.Exists(directory);
    }

    /// <inheritdoc/>
    public Task<bool> DeleteDirectoryAsync(string directory, CancellationToken ct = default)
    {
        return Task.FromResult(this.DeleteDirectory(directory));
    }

    /// <inheritdoc/>
    public bool CleanDirectory(string directory)
    {
        directory = this.GetRealPath(directory);

        if (!Directory.Exists(directory)) {
            return false;
        }

        foreach (string file in Directory.GetFiles(directory)) {
            File.Delete(file);
        }

        foreach (string dir in Directory.GetDirectories(directory)) {
            Directory.Delete(dir, true);
        }

        return this.IsEmptyDirectory(directory);
    }

    /// <inheritdoc/>
    public Task<bool> CleanDirectoryAsync(string directory, CancellationToken ct = default)
    {
        return Task.FromResult(this.CleanDirectory(directory));
    }

    /// <inheritdoc/>
    public bool IsEmptyDirectory(string directory)
    {
        directory = this.GetRealPath(directory);

        if (!Directory.Exists(directory)) {
            return false;
        }

        DirectoryInfo info = new(directory);

        return info.GetFileSystemInfos().Length == 0;
    }

    /// <inheritdoc/>
    public Task<bool> IsEmptyDirectoryAsync(string directory, CancellationToken ct = default)
    {
        return Task.FromResult(this.IsEmptyDirectory(directory));
    }
}
