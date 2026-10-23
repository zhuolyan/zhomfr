using Zhomfr.Validation.Rules;

namespace Zhomfr.Hash.Abstractions.Tests.Stubs;

public class StubConfig : AbstractConfig
{
    /// <inheritdoc/>
    [Required]
    [Between("5", "7")]
    public override string Secret { get; set; } = string.Empty;
}
