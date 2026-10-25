using Zhomfr.Cache.Abstractions;
using Zhomfr.Validation.Rules;

namespace Zhomfr.Cache.InMemory;

/// <inheritdoc/>

// ReSharper disable once ClassNeverInstantiated.Global
public class InMemoryConfig : AbstractConfig
{
    /// <inheritdoc/>
    public override string Prefix { get; init; } = string.Empty;

    /// <inheritdoc/>
    [Required]
    [Min("1")]
    public override int Ttl { get; init; } = 1;
}
