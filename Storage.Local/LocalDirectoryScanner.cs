using System.Runtime.CompilerServices;

using Zhomfr.Storage.Abstractions;

// ReSharper disable AsyncMethodWithoutAwait

namespace Zhomfr.Storage.Local;

/// <inheritdoc cref="IDirectoryManager"/>
public partial class LocalStorage : IDirectoryScanner
{
    /// <inheritdoc/>
    public IEnumerable<string> Files(string directory, bool hidden = false)
    {
        directory = this.GetRealPath(directory);

        if (!Directory.Exists(directory)) {
            yield break;
        }

        foreach (string variable in Directory.GetFiles(directory)) {
            if (hidden || LocalStorage.IsVisibleRealPath(variable)) {
                yield return this.GetVirtualPath(variable);
            }
        }
    }

    /// <inheritdoc/>
    public async IAsyncEnumerable<string> FilesAsync(string directory, bool hidden = false, [EnumeratorCancellation] CancellationToken ct = default)
    {
        directory = this.GetRealPath(directory);

        if (!Directory.Exists(directory)) {
            yield break;
        }

        foreach (string variable in Directory.GetFiles(directory)) {
            ct.ThrowIfCancellationRequested();

            if (hidden || LocalStorage.IsVisibleRealPath(variable)) {
                yield return this.GetVirtualPath(variable);
            }
        }
    }

    /// <inheritdoc/>
    public IEnumerable<string> AllFiles(string directory, bool hidden = false)
    {
        directory = this.GetRealPath(directory);

        if (!Directory.Exists(directory)) {
            yield break;
        }

        foreach (FileSystemInfo item in LocalStorage.GetAllItemsForRealPath(directory, hidden)) {
            if (item is FileInfo) {
                yield return this.GetVirtualPath(item.FullName);
            }
        }
    }

    /// <inheritdoc/>
    public async IAsyncEnumerable<string> AllFilesAsync(string directory, bool hidden = false, [EnumeratorCancellation] CancellationToken ct = default)
    {
        directory = this.GetRealPath(directory);

        if (!Directory.Exists(directory)) {
            yield break;
        }

        foreach (FileSystemInfo item in LocalStorage.GetAllItemsForRealPath(directory, hidden)) {
            ct.ThrowIfCancellationRequested();

            if (item is FileInfo) {
                yield return this.GetVirtualPath(item.FullName);
            }
        }
    }

    /// <inheritdoc/>
    public IEnumerable<string> Directories(string directory, bool hidden = false)
    {
        directory = this.GetRealPath(directory);

        if (!Directory.Exists(directory)) {
            yield break;
        }

        foreach (string variable in Directory.GetDirectories(directory)) {
            if (hidden || LocalStorage.IsVisibleRealPath(variable)) {
                yield return this.GetVirtualPath(variable);
            }
        }
    }

    /// <inheritdoc/>
    public async IAsyncEnumerable<string> DirectoriesAsync(string directory, bool hidden = false, [EnumeratorCancellation] CancellationToken ct = default)
    {
        directory = this.GetRealPath(directory);

        if (!Directory.Exists(directory)) {
            yield break;
        }

        foreach (string variable in Directory.GetDirectories(directory)) {
            ct.ThrowIfCancellationRequested();

            if (hidden || LocalStorage.IsVisibleRealPath(variable)) {
                yield return this.GetVirtualPath(variable);
            }
        }
    }

    /// <inheritdoc/>
    public IEnumerable<string> AllDirectories(string directory, bool hidden = false)
    {
        directory = this.GetRealPath(directory);

        if (!Directory.Exists(directory)) {
            yield break;
        }

        foreach (FileSystemInfo item in LocalStorage.GetAllItemsForRealPath(directory, hidden)) {
            if (item is DirectoryInfo) {
                yield return this.GetVirtualPath(item.FullName);
            }
        }
    }

    /// <inheritdoc/>
    public async IAsyncEnumerable<string> AllDirectoriesAsync(string directory, bool hidden = false, [EnumeratorCancellation] CancellationToken ct = default)
    {
        directory = this.GetRealPath(directory);

        if (!Directory.Exists(directory)) {
            yield break;
        }

        foreach (FileSystemInfo item in LocalStorage.GetAllItemsForRealPath(directory, hidden)) {
            ct.ThrowIfCancellationRequested();

            if (item is DirectoryInfo) {
                yield return this.GetVirtualPath(item.FullName);
            }
        }
    }
}
