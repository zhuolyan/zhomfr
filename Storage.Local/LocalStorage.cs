using Zhomfr.Helpers.ScalarTypes.Strings;
using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Local;

/// <inheritdoc cref="IStorage"/>
public partial class LocalStorage(LocalStorageConfig config) : IStorage
{
    private const string URL_SEPARATOR = "/";

    /// <inheritdoc/>
    public string? GetUrl(string path)
    {
        return this.IsFile(path) ? path.StartWith(LocalStorage.URL_SEPARATOR).StartWith(config.BaseUrl) : null;
    }

    private string GetRealPath(string path)
    {
        return path.StartWith(Path.DirectorySeparatorChar.ToString()).StartWith(config.RootDir).Replace(IStorage.DIRECTORY_SEPARATOR, Path.DirectorySeparatorChar);
    }

    private string GetVirtualPath(string path)
    {
        return path.Replace(Path.DirectorySeparatorChar, IStorage.DIRECTORY_SEPARATOR).ChopStart(config.RootDir);
    }
}
