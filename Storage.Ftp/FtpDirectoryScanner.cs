using System.Runtime.CompilerServices;

using FluentFTP;

using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Ftp;

/// <inheritdoc cref="IDirectoryScanner"/>
public partial class FtpStorage : IDirectoryScanner
{
    /// <inheritdoc/>
    public IEnumerable<string> Files(string directory, bool hidden = false)
    {
        directory = this.GetRealPath(directory);

        using FtpClient client = this.GetClient();

        if (!client.DirectoryExists(directory)) {
            yield break;
        }

        FtpListItem[] list = client.GetListing(directory, FtpListOption.AllFiles);

        foreach (FtpListItem item in list) {
            if (item.Type == FtpObjectType.File && (hidden || item.GroupPermissions == FtpPermission.Read || item.OthersPermissions == FtpPermission.Read)) {
                yield return this.GetVirtualPath(item.FullName);
            }
        }
    }

    /// <inheritdoc/>
    public async IAsyncEnumerable<string> FilesAsync(string directory, bool hidden = false, [EnumeratorCancellation] CancellationToken ct = default)
    {
        directory = this.GetRealPath(directory);

        await using AsyncFtpClient client = this.GetAsyncClient();

        if (!await client.DirectoryExists(directory, ct)) {
            yield break;
        }

        FtpListItem[] list = await client.GetListing(directory, FtpListOption.AllFiles, ct);

        foreach (FtpListItem item in list) {
            ct.ThrowIfCancellationRequested();

            if (item.Type == FtpObjectType.File && (hidden || item.GroupPermissions == FtpPermission.Read || item.OthersPermissions == FtpPermission.Read)) {
                yield return this.GetVirtualPath(item.FullName);
            }
        }
    }

    /// <inheritdoc/>
    public IEnumerable<string> AllFiles(string directory, bool hidden = false)
    {
        directory = this.GetRealPath(directory);

        using FtpClient client = this.GetClient();

        if (!client.DirectoryExists(directory)) {
            yield break;
        }

        FtpListItem[] list = client.GetListing(directory, FtpListOption.Recursive | FtpListOption.AllFiles);

        foreach (FtpListItem item in list) {
            if (item.Type == FtpObjectType.File && (hidden || item.GroupPermissions == FtpPermission.Read || item.OthersPermissions == FtpPermission.Read)) {
                yield return this.GetVirtualPath(item.FullName);
            }
        }
    }

    /// <inheritdoc/>
    public async IAsyncEnumerable<string> AllFilesAsync(string directory, bool hidden = false, [EnumeratorCancellation] CancellationToken ct = default)
    {
        directory = this.GetRealPath(directory);

        await using AsyncFtpClient client = this.GetAsyncClient();

        if (!await client.DirectoryExists(directory, ct)) {
            yield break;
        }

        FtpListItem[] list = await client.GetListing(directory, FtpListOption.Recursive | FtpListOption.AllFiles, ct);

        foreach (FtpListItem item in list) {
            ct.ThrowIfCancellationRequested();

            if (item.Type == FtpObjectType.File && (hidden || item.GroupPermissions == FtpPermission.Read || item.OthersPermissions == FtpPermission.Read)) {
                yield return this.GetVirtualPath(item.FullName);
            }
        }
    }

    /// <inheritdoc/>
    public IEnumerable<string> Directories(string directory, bool hidden = false)
    {
        directory = this.GetRealPath(directory);

        using FtpClient client = this.GetClient();

        if (!client.DirectoryExists(directory)) {
            yield break;
        }

        FtpListItem[] list = client.GetListing(directory, FtpListOption.AllFiles);

        foreach (FtpListItem item in list) {
            if (item.Type == FtpObjectType.Directory && (hidden || item.GroupPermissions == FtpPermission.Read || item.OthersPermissions == FtpPermission.Read)) {
                yield return this.GetVirtualPath(item.FullName);
            }
        }
    }

    /// <inheritdoc/>
    public async IAsyncEnumerable<string> DirectoriesAsync(string directory, bool hidden = false, [EnumeratorCancellation] CancellationToken ct = default)
    {
        directory = this.GetRealPath(directory);

        await using AsyncFtpClient client = this.GetAsyncClient();

        if (!await client.DirectoryExists(directory, ct)) {
            yield break;
        }

        FtpListItem[] list = await client.GetListing(directory, FtpListOption.AllFiles, ct);

        foreach (FtpListItem item in list) {
            ct.ThrowIfCancellationRequested();

            if (item.Type == FtpObjectType.Directory && (hidden || item.GroupPermissions == FtpPermission.Read || item.OthersPermissions == FtpPermission.Read)) {
                yield return this.GetVirtualPath(item.FullName);
            }
        }
    }

    /// <inheritdoc/>
    public IEnumerable<string> AllDirectories(string directory, bool hidden = false)
    {
        directory = this.GetRealPath(directory);

        using FtpClient client = this.GetClient();

        if (!client.DirectoryExists(directory)) {
            yield break;
        }

        FtpListItem[] list = client.GetListing(directory, FtpListOption.Recursive | FtpListOption.AllFiles);

        foreach (FtpListItem item in list) {
            if (item.Type == FtpObjectType.Directory && (hidden || item.GroupPermissions == FtpPermission.Read || item.OthersPermissions == FtpPermission.Read)) {
                yield return this.GetVirtualPath(item.FullName);
            }
        }
    }

    /// <inheritdoc/>
    public async IAsyncEnumerable<string> AllDirectoriesAsync(string directory, bool hidden = false, [EnumeratorCancellation] CancellationToken ct = default)
    {
        directory = this.GetRealPath(directory);

        await using AsyncFtpClient client = this.GetAsyncClient();

        if (!await client.DirectoryExists(directory, ct)) {
            yield break;
        }

        FtpListItem[] list = await client.GetListing(directory, FtpListOption.Recursive | FtpListOption.AllFiles, ct);

        foreach (FtpListItem item in list) {
            ct.ThrowIfCancellationRequested();

            if (item.Type == FtpObjectType.Directory && (hidden || item.GroupPermissions == FtpPermission.Read || item.OthersPermissions == FtpPermission.Read)) {
                yield return this.GetVirtualPath(item.FullName);
            }
        }
    }
}
