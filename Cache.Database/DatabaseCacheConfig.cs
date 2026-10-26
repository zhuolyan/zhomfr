using Zhomfr.Cache.Abstractions;
using Zhomfr.Validation.Rules;

namespace Zhomfr.Cache.Database;

/// <inheritdoc/>

// ReSharper disable once ClassNeverInstantiated.Global
public class DatabaseCacheConfig : AbstractConfig
{
    /// <inheritdoc/>
    [Required]
    [Min("4", ErrorMessage = "The {0} must consist of {1} or more characters.")]
    public override string Prefix { get; init; } = string.Empty;

    /// <inheritdoc/>
    [Required]
    [Min("1", ErrorMessage = "The {0} must be greater than or equal to {1}.")]
    public override int Ttl { get; init; } = 1;
}
