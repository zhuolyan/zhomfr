using FluentFTP;

using Zhomfr.Helpers.ScalarTypes.Strings;
using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Ftp;

public partial class FtpStorage
{
    private readonly FtpConfig _ftpConfig = new()
    {
        RetryAttempts                = config.RetryAttempts,
        EncryptionMode               = FtpEncryptionMode.Auto,
        ValidateAnyCertificate       = config.ValidateAnyCertificate,
        DataConnectionType           = FtpDataConnectionType.AutoPassive,
        DataConnectionReadTimeout    = config.DataConnectionReadTimeout    * 1_000,
        DataConnectionConnectTimeout = config.DataConnectionConnectTimeout * 1_000,
        ReadTimeout                  = config.ReadTimeout                  * 1_000,
        ConnectTimeout               = config.ConnectTimeout               * 1_000,
        ListingParser                = FtpParser.Auto,
        TimeConversion               = FtpDate.UTC,
    };

    private FtpClient GetClient()
    {
        FtpClient client = new(config.Host, config.Username, config.Password, config.Port, this._ftpConfig);

        client.AutoConnect();

        return client;
    }

    private AsyncFtpClient GetAsyncClient()
    {
        AsyncFtpClient client = new(config.Host, config.Username, config.Password, config.Port, this._ftpConfig);

        client.AutoConnect();

        return client;
    }

    private string GetRealPath(string path)
    {
        return path.StartWith(IStorage.DIRECTORY_SEPARATOR).StartWith(config.RootDir);
    }

    private string GetVirtualPath(string path)
    {
        return path.ChopStart(config.RootDir);
    }

    private bool IsVisibleReal(string path)
    {
        using FtpClient client = this.GetClient();

        if (!client.DirectoryExists(path) || !client.FileExists(path)) {
            return false;
        }

        FtpListItem info = client.GetObjectInfo(path);

        return info.GroupPermissions.HasFlag(FtpPermission.Read) || info.OthersPermissions.HasFlag(FtpPermission.Read);
    }

    private async Task<bool> IsVisibleRealAsync(string path, CancellationToken ct)
    {
        await using AsyncFtpClient client = this.GetAsyncClient();

        if (!await client.DirectoryExists(path, ct) || !await client.FileExists(path, ct)) {
            return false;
        }

        FtpListItem info = await client.GetObjectInfo(path, token: ct);

        return info.GroupPermissions.HasFlag(FtpPermission.Read) || info.OthersPermissions.HasFlag(FtpPermission.Read);
    }

    private bool MakeVisibleReal(string path)
    {
        using FtpClient client = this.GetClient();

        if (client.FileExists(path)) {
            client.Chmod(path, FtpPermission.Read | FtpPermission.Write, FtpPermission.Read, FtpPermission.Read);

            return this.IsVisibleReal(path);
        }

        if (client.DirectoryExists(path)) {
            client.Chmod(path, FtpPermission.Read | FtpPermission.Write | FtpPermission.Execute, FtpPermission.Read, FtpPermission.Read);

            return this.IsVisibleReal(path);
        }

        return false;
    }

    private async Task<bool> MakeVisibleRealAsync(string path, CancellationToken ct)
    {
        await using AsyncFtpClient client = this.GetAsyncClient();

        if (await client.FileExists(path, ct)) {
            await client.Chmod(path, FtpPermission.Read | FtpPermission.Write, FtpPermission.Read, FtpPermission.Read, ct);

            return await this.IsVisibleRealAsync(path, ct);
        }

        if (await client.DirectoryExists(path, ct)) {
            await client.Chmod(path, FtpPermission.Read | FtpPermission.Write | FtpPermission.Execute, FtpPermission.Read, FtpPermission.Read, ct);

            return await this.IsVisibleRealAsync(path, ct);
        }

        return false;
    }

    private bool MakeHiddenReal(string path)
    {
        using FtpClient client = this.GetClient();

        if (client.FileExists(path)) {
            client.Chmod(path, FtpPermission.Read | FtpPermission.Write, FtpPermission.None, FtpPermission.None);

            return !this.IsVisibleReal(path);
        }

        if (client.DirectoryExists(path)) {
            client.Chmod(path, FtpPermission.Read | FtpPermission.Write | FtpPermission.Execute, FtpPermission.None, FtpPermission.None);

            return !this.IsVisibleReal(path);
        }

        return false;
    }

    private async Task<bool> MakeHiddenRealAsync(string path, CancellationToken ct)
    {
        await using AsyncFtpClient client = this.GetAsyncClient();

        if (await client.FileExists(path, ct)) {
            await client.Chmod(path, FtpPermission.Read | FtpPermission.Write, FtpPermission.None, FtpPermission.None, ct);

            return !await this.IsVisibleRealAsync(path, ct);
        }

        if (await client.DirectoryExists(path, ct)) {
            await client.Chmod(path, FtpPermission.Read | FtpPermission.Write | FtpPermission.Execute, FtpPermission.None, FtpPermission.None, ct);

            return !await this.IsVisibleRealAsync(path, ct);
        }

        return false;
    }
}
