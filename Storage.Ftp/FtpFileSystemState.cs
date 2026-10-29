using FluentFTP;

using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Ftp;

/// <inheritdoc cref="IFileSystemState"/>
public partial class FtpStorage : IFileSystemState
{
    /// <inheritdoc/>
    public bool Exists(string path)
    {
        path = this.GetRealPath(path);

        using FtpClient client = this.GetClient();

        return client.FileExists(path) || client.DirectoryExists(path);
    }

    /// <inheritdoc/>
    public async Task<bool> ExistsAsync(string path, CancellationToken ct = default)
    {
        path = this.GetRealPath(path);

        await using AsyncFtpClient client = this.GetAsyncClient();

        return await client.FileExists(path, ct) || await client.DirectoryExists(path, ct);
    }

    /// <inheritdoc/>
    public bool Missing(string path)
    {
        return !this.Exists(path);
    }

    /// <inheritdoc/>
    public async Task<bool> MissingAsync(string path, CancellationToken ct = default)
    {
        return !await this.ExistsAsync(path, ct);
    }

    /// <inheritdoc/>
    public bool IsHidden(string path)
    {
        path = this.GetRealPath(path);

        return !this.IsVisibleReal(path);
    }

    /// <inheritdoc/>
    public async Task<bool> IsHiddenAsync(string path, CancellationToken ct = default)
    {
        path = this.GetRealPath(path);

        return !await this.IsVisibleRealAsync(path, ct);
    }

    /// <inheritdoc/>
    public bool IsVisible(string path)
    {
        path = this.GetRealPath(path);

        return this.IsVisibleReal(path);
    }

    /// <inheritdoc/>
    public async Task<bool> IsVisibleAsync(string path, CancellationToken ct = default)
    {
        path = this.GetRealPath(path);

        return await this.IsVisibleRealAsync(path, ct);
    }

    /// <inheritdoc/>
    public bool MakeHidden(string path)
    {
        path = this.GetRealPath(path);

        return this.MakeHiddenReal(path);
    }

    /// <inheritdoc/>
    public async Task<bool> MakeHiddenAsync(string path, CancellationToken ct = default)
    {
        path = this.GetRealPath(path);

        return await this.MakeHiddenRealAsync(path, ct);
    }

    /// <inheritdoc/>
    public bool MakeVisible(string path)
    {
        path = this.GetRealPath(path);

        return this.MakeVisibleReal(path);
    }

    /// <inheritdoc/>
    public async Task<bool> MakeVisibleAsync(string path, CancellationToken ct = default)
    {
        path = this.GetRealPath(path);

        return await this.MakeVisibleRealAsync(path, ct);
    }
}
