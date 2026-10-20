using System.Collections;

namespace Zhomfr.Validation.Rules.Tests.Stubs;

public static class DictionaryStubs
{
    public static readonly Dictionary<string, string> Default     = new() { { "first", "test" }, { "second", "not" }, { "third", "blah" } };
    public static readonly Dictionary<string, string> NotDistinct = new() { { "first", "test" }, { "second", "test" }, { "third", "blah" } };
    public static readonly Dictionary<string, string> Empty       = new();

    public static IDictionary GetDictionary(int count)
    {
        Dictionary<int, int> result = new();

        for (int i = 0; i < count; i++) {
            result[i] = i;
        }

        return result;
    }
}
