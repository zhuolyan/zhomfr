using FluentFTP;

using Zhomfr.Helpers.SmartFile;
using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Ftp;

/// <inheritdoc cref="IFileManager"/>
public partial class FtpStorage : IFileManager
{
    /// <inheritdoc/>
    public SmartFile? Get(string path)
    {
        path = this.GetRealPath(path);

        using FtpClient client = this.GetClient();

        if (!client.FileExists(path)) {
            return null;
        }

        return new(client.OpenRead(path), Path.GetFileName(path));
    }

    /// <inheritdoc/>
    public async Task<SmartFile?> GetAsync(string path, CancellationToken ct = default)
    {
        path = this.GetRealPath(path);

        await using AsyncFtpClient client = this.GetAsyncClient();

        if (!await client.FileExists(path, ct)) {
            return null;
        }

        return new(await client.OpenRead(path, token: ct), Path.GetFileName(path));
    }

    /// <inheritdoc/>
    public bool Put(string path, SmartFile storageFile)
    {
        path = this.GetRealPath(path);

        using FtpClient client = this.GetClient();

        if (client.DirectoryExists(path)) {
            return false;
        }

        using Stream output = client.OpenWrite(path);

        storageFile.GetStream().CopyTo(output);

        return this.MakeVisibleReal(path);
    }

    /// <inheritdoc/>
    public async Task<bool> PutAsync(string path, SmartFile storageFile, CancellationToken ct = default)
    {
        path = this.GetRealPath(path);

        await using AsyncFtpClient client = this.GetAsyncClient();

        if (await client.DirectoryExists(path, ct)) {
            return false;
        }

        await using Stream output = await client.OpenWrite(path, token: ct);

        await storageFile.GetStream().CopyToAsync(output, ct);

        return await this.MakeVisibleRealAsync(path, ct);
    }

    /// <inheritdoc/>
    public bool Delete(string path)
    {
        path = this.GetRealPath(path);

        using FtpClient client = this.GetClient();

        if (!client.FileExists(path)) {
            return false;
        }

        client.DeleteFile(path);

        return !client.FileExists(path);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(string path, CancellationToken ct = default)
    {
        path = this.GetRealPath(path);

        await using AsyncFtpClient client = this.GetAsyncClient();

        if (!await client.FileExists(path, ct)) {
            return false;
        }

        await client.DeleteFile(path, ct);

        return !await client.FileExists(path, ct);
    }

    /// <inheritdoc/>
    public bool Move(string path, string target)
    {
        path   = this.GetRealPath(path);
        target = this.GetRealPath(target);

        using FtpClient client = this.GetClient();

        if (!client.FileExists(path) || client.FileExists(target)) {
            return false;
        }

        return client.MoveFile(path, target) && this.MakeVisibleReal(target);
    }

    /// <inheritdoc/>
    public async Task<bool> MoveAsync(string path, string target, CancellationToken ct = default)
    {
        path   = this.GetRealPath(path);
        target = this.GetRealPath(target);

        await using AsyncFtpClient client = this.GetAsyncClient();

        if (!await client.FileExists(path, ct) || await client.FileExists(target, ct)) {
            return false;
        }

        return await client.MoveFile(path, target, token: ct) && await this.MakeVisibleRealAsync(target, ct);
    }

    /// <inheritdoc/>
    public bool Copy(string path, string target)
    {
        path   = this.GetRealPath(path);
        target = this.GetRealPath(target);

        using FtpClient client = this.GetClient();

        if (!client.FileExists(path) || client.FileExists(target)) {
            return false;
        }

        using Stream input  = client.OpenRead(path);
        using Stream output = client.OpenWrite(target);

        input.CopyTo(output);

        return client.FileExists(path) && client.FileExists(target) && this.MakeVisibleReal(target);
    }

    /// <inheritdoc/>
    public async Task<bool> CopyAsync(string path, string target, CancellationToken ct = default)
    {
        path   = this.GetRealPath(path);
        target = this.GetRealPath(target);

        await using AsyncFtpClient client = this.GetAsyncClient();

        if (!await client.FileExists(path, ct) || await client.FileExists(target, ct)) {
            return false;
        }

        await using Stream input  = await client.OpenRead(path, token: ct);
        await using Stream output = await client.OpenWrite(target, token: ct);

        await input.CopyToAsync(output, ct);

        return await client.FileExists(path, ct) && await client.FileExists(target, ct) && await this.MakeVisibleRealAsync(target, ct);
    }
}
