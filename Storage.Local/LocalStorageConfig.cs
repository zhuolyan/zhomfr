using Zhomfr.Storage.Abstractions;
using Zhomfr.Validation.Rules;

namespace Zhomfr.Storage.Local;

/// <inheritdoc/>
public class LocalStorageConfig : IConfig
{
    /// <summary>Root dir for storage.</summary>
    [Required]
    [Min("1")]
    public string RootDir { get; set; } = "storage";

    /// <summary>Base url for access from web to files in storage.</summary>
    [ActiveUrl]
    [DoesntEndWith("/", "\\")]
    public string BaseUrl { get; set; } = string.Empty;
}
