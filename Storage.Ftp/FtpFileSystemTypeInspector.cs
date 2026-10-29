using FluentFTP;

using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Ftp;

/// <inheritdoc cref="IFileSystemTypeInspector"/>
public partial class FtpStorage : IFileSystemTypeInspector
{
    /// <inheritdoc/>
    public bool IsDirectory(string path)
    {
        path = this.GetRealPath(path);

        using FtpClient client = this.GetClient();

        return client.DirectoryExists(path);
    }

    /// <inheritdoc/>
    public async Task<bool> IsDirectoryAsync(string path, CancellationToken ct = default)
    {
        path = this.GetRealPath(path);

        await using AsyncFtpClient client = this.GetAsyncClient();

        return await client.DirectoryExists(path, ct);
    }

    /// <inheritdoc/>
    public bool IsFile(string path)
    {
        path = this.GetRealPath(path);

        using FtpClient client = this.GetClient();

        return client.FileExists(path);
    }

    /// <inheritdoc/>
    public async Task<bool> IsFileAsync(string path, CancellationToken ct = default)
    {
        path = this.GetRealPath(path);

        await using AsyncFtpClient client = this.GetAsyncClient();

        return await client.FileExists(path, ct);
    }
}
