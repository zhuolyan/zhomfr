using FluentFTP;

using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Ftp;

/// <inheritdoc cref="IDirectoryManager"/>
public partial class FtpStorage : IDirectoryManager
{
    /// <inheritdoc/>
    public bool MakeDirectory(string path, bool recursive = true)
    {
        path = this.GetRealPath(path);

        using FtpClient client = this.GetClient();

        return !client.DirectoryExists(path) && client.CreateDirectory(path, recursive) && this.MakeVisibleReal(path);
    }

    /// <inheritdoc/>
    public async Task<bool> MakeDirectoryAsync(string path, bool recursive = true, CancellationToken ct = default)
    {
        path = this.GetRealPath(path);

        await using AsyncFtpClient client = this.GetAsyncClient();

        return !await client.DirectoryExists(path, ct) && await client.CreateDirectory(path, recursive, ct) && await this.MakeVisibleRealAsync(path, ct);
    }

    /// <inheritdoc/>
    public bool DeleteDirectory(string directory)
    {
        directory = this.GetRealPath(directory);

        using FtpClient client = this.GetClient();

        if (!client.DirectoryExists(directory)) {
            return false;
        }

        client.DeleteDirectory(directory);

        return !client.DirectoryExists(directory);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteDirectoryAsync(string directory, CancellationToken ct = default)
    {
        directory = this.GetRealPath(directory);

        await using AsyncFtpClient client = this.GetAsyncClient();

        if (!await client.DirectoryExists(directory, ct)) {
            return false;
        }

        await client.DeleteDirectory(directory, ct);

        return !await client.DirectoryExists(directory, ct);
    }

    /// <inheritdoc/>
    public bool CleanDirectory(string directory)
    {
        directory = this.GetRealPath(directory);

        using FtpClient client = this.GetClient();

        if (!client.DirectoryExists(directory)) {
            return false;
        }

        client.EmptyDirectory(directory, FtpListOption.AllFiles);

        return client.GetListing().Length == 0;
    }

    /// <inheritdoc/>
    public async Task<bool> CleanDirectoryAsync(string directory, CancellationToken ct = default)
    {
        directory = this.GetRealPath(directory);

        await using AsyncFtpClient client = this.GetAsyncClient();

        if (!await client.DirectoryExists(directory, ct)) {
            return false;
        }

        await client.EmptyDirectory(directory, FtpListOption.AllFiles, ct);

        return (await client.GetListing(ct)).Length == 0;
    }

    /// <inheritdoc/>
    public bool IsEmptyDirectory(string directory)
    {
        directory = this.GetRealPath(directory);

        using FtpClient client = this.GetClient();

        if (!client.DirectoryExists(directory)) {
            return false;
        }

        return client.GetListing().Length == 0;
    }

    /// <inheritdoc/>
    public async Task<bool> IsEmptyDirectoryAsync(string directory, CancellationToken ct = default)
    {
        directory = this.GetRealPath(directory);

        await using AsyncFtpClient client = this.GetAsyncClient();

        if (!await client.DirectoryExists(directory, ct)) {
            return false;
        }

        return (await client.GetListing(ct)).Length == 0;
    }
}
