namespace Zhomfr.Cache.Tests.Stubs;

public static class StubFixturesSource
{
    public static readonly List<Type> Tested = [typeof(InMemoryInitializer), typeof(RedisInitializer)];
}
