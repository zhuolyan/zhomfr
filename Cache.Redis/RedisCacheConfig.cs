using Zhomfr.Cache.Abstractions;
using Zhomfr.Validation.Rules;

namespace Zhomfr.Cache.Redis;

/// <inheritdoc/>

// ReSharper disable once ClassNeverInstantiated.Global
public class RedisCacheConfig : IConfig
{
    /// <summary>Cache prefix, needed then you use more than one app for one provider.</summary>
    [Required]
    [Min("4", ErrorMessage = "The {0} must consist of {1} or more characters.")]
    public string Prefix { get; init; } = string.Empty;

    /// <summary>Default time to life for saved data (in minutes).</summary>
    [Required]
    [Min("1", ErrorMessage = "The {0} must be greater than or equal to {1}.")]
    public int Ttl { get; init; } = 1;

    /// <summary>For more information see "https://seredis.dev/Configuration".</summary>
    [Required]
    [Min("1", ErrorMessage = "The {0} must be greater than or equal to {1}.")]
    public string Connection { get; init; } = string.Empty;
}
