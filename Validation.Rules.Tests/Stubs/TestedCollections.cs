namespace Zhomfr.Validation.Rules.Tests.Stubs;

public static class TestedCollections
{
    public static readonly Dictionary<string, string> Dictionary = new() { { "first", "test" }, { "second", "not" }, { "third", "blah" } };
    public static readonly string[]                   Collection = ["test", "not", "blah"];
}
