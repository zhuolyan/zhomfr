using Zhomfr.Storage.Abstractions;
using Zhomfr.Validation.Rules;

namespace Zhomfr.Storage.Tests.Stubs;

public class StubConfig : IConfig
{
    [Url]
    public string BaseUrl { get; set; } = string.Empty;

    [Required]
    [Min("1")]
    public string RootDir { get; set; } = string.Empty;
}
