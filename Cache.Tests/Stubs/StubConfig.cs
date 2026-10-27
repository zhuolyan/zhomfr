using Zhomfr.Cache.Abstractions;
using Zhomfr.Validation.Rules;

namespace Zhomfr.Cache.Tests.Stubs;

public class StubConfig : IConfig
{
    [Between("5", "7")]
    public string Prefix { get; init; } = string.Empty;
}
