using Zhomfr.Helpers.ScalarTypes.Strings;
using Zhomfr.Storage.Abstractions;

namespace Zhomfr.Storage.Local;

/// <inheritdoc cref="IStorage"/>
public partial class LocalStorage(LocalStorageConfig config) : IStorage
{
    /// <inheritdoc/>
    public string? GetUrl(string path)
    {
        return this.IsFile(path) ? path.StartWith(IStorage.DIRECTORY_SEPARATOR.ToString()).StartWith(config.BaseUrl) : null;
    }
}
