using System.Collections;

namespace Zhomfr.Helpers.ScalarTypes.Tests.Stubs;

public static class EnumerableStub
{
    public static IEnumerable GetValue()
    {
        yield return 1;
        yield return 2;
        yield return 3;
    }
}
