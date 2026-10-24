using Zhomfr.Hash.Abstractions;
using Zhomfr.Validation.Rules;

namespace Zhomfr.Hash.Argon2;

/// <inheritdoc/>
public class Argon2Config : AbstractConfig
{
    /// <inheritdoc/>

    [Required]
    [Size("32")]
    [AlphaNumeric]
    public override string Secret { get; set; } = string.Empty;

    /// <summary>
    ///     Important: For Argon2id, a minimum of 3 iterations is recommended if memory is limited (less than 64 MB). If
    ///     memory is large (1 GB+), 1 iteration can be used.
    /// </summary>
    [Required]
    [Min("1")]
    public int Iterations { get; init; } = 10;

    /// <summary>The main rule: Use as much as your server allows, taking into account peak load.</summary>
    [Required]
    [Min("8")]
    public int MemorySize { get; init; } = 65536;

    /// <summary>
    ///     Do not exceed the number of logical cores of your processor, otherwise performance will drop due to context
    ///     switching.
    /// </summary>
    [Required]
    [Min("4")]
    public int DegreeOfParallelism { get; init; } = 4;

    /// <summary>
    ///     Important: Although Argon2 technically allows 8 bytes, the RFC standard strongly recommends using at least 16
    ///     bytes to ensure uniqueness in all scenarios.
    /// </summary>
    [Required]
    [Min("8")]
    [Max("1024")]
    public int SaltSize { get; init; } = 16;
}
