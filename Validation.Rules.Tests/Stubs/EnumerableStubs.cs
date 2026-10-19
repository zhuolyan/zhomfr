namespace Zhomfr.Validation.Rules.Tests.Stubs;

public static class EnumerableStubs
{
    public static readonly IEnumerable<string> ContainsEnumeration = ["test", "not", "blah"];

    public static readonly List<object> Between =
    [
        EnumerableStubs.GetEnumerable(3).ToList(),
        EnumerableStubs.GetEnumerable(6).ToList(),
        EnumerableStubs.GetEnumerable(3),
        EnumerableStubs.GetEnumerable(6),
    ];

    public static IEnumerable<int> GetEnumerable(int count)
    {
        for (int i = 0; i < count; i++) {
            yield return i;
        }
    }

    public static IEnumerable<string> GetNotDistinctEnumerable(int count)
    {
        for (int i = 0; i < count; i++) {
            yield return "test";
        }
    }
}
