namespace Zhomfr.Helpers.ScalarTypes.Tests.Stubs;

public class SwapStubValue
{
    public string                     Input        { get; init; } = string.Empty;
    public Dictionary<string, string> Replacements { get; init; } = new();
    public string                     Expected     { get; init; } = string.Empty;
}
