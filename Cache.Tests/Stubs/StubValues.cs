namespace Zhomfr.Cache.Tests.Stubs;

public static class StubValues
{
    public static readonly StubCached Exists        = new() { Value = "exists" };
    public static readonly StubCached ExistsAnother = new() { Value = "exists-another" };
    public static readonly StubCached ExistsExpired = new() { Value = "expired" };
    public static readonly StubCached NotExists     = new() { Value = "new" };
}
