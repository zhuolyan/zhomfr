using Zhomfr.Helpers.ScalarTypes.Strings;
using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Ftp;

/// <inheritdoc cref="IStorage"/>
public partial class FtpStorage(FtpStorageConfig config) : IStorage
{
    /// <inheritdoc/>
    public string? GetUrl(string path)
    {
        return this.IsFile(path) ? path.StartWith(IStorage.DIRECTORY_SEPARATOR).StartWith(config.BaseUrl) : null;
    }
}
