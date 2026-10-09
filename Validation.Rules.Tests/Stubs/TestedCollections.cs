namespace Zhomfr.Validation.Rules.Tests.Stubs;

public static class TestedCollections
{
    public static readonly IEnumerable<string>        NotDistinctEnumeration = TestedCollections.GetNotDistinctEnumeration();
    public static readonly Dictionary<string, string> Dictionary             = new() { { "first", "test" }, { "second", "not" }, { "third", "blah" } };
    public static readonly string[]                   Collection             = ["test", "not", "blah"];

    public static readonly List<List<int>> BetweenCorrectValues    = [[1, 2], [1, 2, 3], [1, 2, 3, 4]];
    public static readonly List<List<int>> BetweenNotCorrectValues = [[1], [1, 2, 3, 4, 5]];

    public static readonly List<IEnumerable<int>> BetweenCorrectEnumerationValues
        = [TestedCollections.GetEnumerable(2), TestedCollections.GetEnumerable(3), TestedCollections.GetEnumerable(4)];

    public static readonly List<IEnumerable<int>> BetweenNotCorrectEnumerationValues = [TestedCollections.GetEnumerable(1), TestedCollections.GetEnumerable(5)];
    public static readonly string[]               NotDistinctCollection              = ["test", "test", "blah"];

    public static IEnumerable<int> GetEnumerable(int count)
    {
        for (int i = 0; i < count; i++) {
            yield return i;
        }
    }

    private static IEnumerable<string> GetEnumeration()
    {
        yield return "first";
        yield return "second";
        yield return "third";
    }

    private static IEnumerable<string> GetNotDistinctEnumeration()
    {
        yield return "first";
        yield return "second";
        yield return "second";
        yield return "third";
    }
}
