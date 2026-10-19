namespace Zhomfr.Validation.Rules.Tests.Stubs;

public static class DictionaryStubs
{
    public static readonly Dictionary<string, string> Default     = new() { { "first", "test" }, { "second", "not" }, { "third", "blah" } };
    public static readonly Dictionary<string, string> NotDistinct = new() { { "first", "test" }, { "second", "test" }, { "third", "blah" } };
}
