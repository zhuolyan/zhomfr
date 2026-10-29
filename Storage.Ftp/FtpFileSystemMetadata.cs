using FluentFTP;

using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Ftp;

/// <inheritdoc cref="IFileSystemMetadata"/>
public partial class FtpStorage : IFileSystemMetadata
{
    /// <inheritdoc/>
    public long Size(string path)
    {
        path = this.GetRealPath(path);

        using FtpClient client = this.GetClient();

        if (client.FileExists(path)) {
            return client.GetFileSize(path);
        }

        if (!client.DirectoryExists(path)) {
            return 0;
        }

        return client.GetListing(path, FtpListOption.Recursive | FtpListOption.AllFiles).Where(i => i.Type == FtpObjectType.File).Sum(file => file.Size);
    }

    /// <inheritdoc/>
    public async Task<long> SizeAsync(string path, CancellationToken ct = default)
    {
        path = this.GetRealPath(path);

        await using AsyncFtpClient client = this.GetAsyncClient();

        if (await client.FileExists(path, ct)) {
            return await client.GetFileSize(path, token: ct);
        }

        if (!await client.DirectoryExists(path, ct)) {
            return 0;
        }

        return (await client.GetListing(path, FtpListOption.Recursive | FtpListOption.AllFiles, ct)).Where(i => i.Type == FtpObjectType.File)
                                                                                                    .Sum(file => file.Size);
    }

    /// <inheritdoc/>
    public DateTimeOffset CreatedAt(string path)
    {
        path = this.GetRealPath(path);

        using FtpClient client = this.GetClient();

        if (client.FileExists(path) || client.DirectoryExists(path)) {
            return client.GetObjectInfo(path).Created;
        }

        return IStorage.DefaultDateTime;
    }

    /// <inheritdoc/>
    public async Task<DateTimeOffset> CreatedAtAsync(string path, CancellationToken ct = default)
    {
        path = this.GetRealPath(path);

        await using AsyncFtpClient client = this.GetAsyncClient();

        if (await client.FileExists(path, ct) || await client.DirectoryExists(path, ct)) {
            return (await client.GetObjectInfo(path, token: ct)).Modified;
        }

        return IStorage.DefaultDateTime;
    }

    /// <inheritdoc/>
    public DateTimeOffset LastModified(string path)
    {
        path = this.GetRealPath(path);

        using FtpClient client = this.GetClient();

        if (client.FileExists(path) || client.DirectoryExists(path)) {
            return client.GetObjectInfo(path).Modified;
        }

        return IStorage.DefaultDateTime;
    }

    /// <inheritdoc/>
    public async Task<DateTimeOffset> LastModifiedAsync(string path, CancellationToken ct = default)
    {
        path = this.GetRealPath(path);

        await using AsyncFtpClient client = this.GetAsyncClient();

        if (await client.FileExists(path, ct) || await client.DirectoryExists(path, ct)) {
            return (await client.GetObjectInfo(path, token: ct)).Modified;
        }

        return IStorage.DefaultDateTime;
    }
}
