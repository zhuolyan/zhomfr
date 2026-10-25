using Zhomfr.Cache.Abstractions;
using Zhomfr.Validation.Rules;

namespace Zhomfr.Cache.Tests.Stubs;

public class StubConfig : AbstractConfig
{
    /// <inheritdoc/>
    [Between("5", "7")]
    public override string Prefix { get; init; } = string.Empty;

    /// <inheritdoc/>
    public override int Ttl { get; init; }
}
