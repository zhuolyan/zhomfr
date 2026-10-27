using Zhomfr.Cache.Abstractions;
using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Unit.CacheTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class PutTest(TestTypes type)
{
    [TestCase(StubKeys.NOT_EXISTS)]
    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    [TestCase(StubKeys.EXISTS_EXPIRED)]
    public void Should_SetValue_When_WithoutTtl(string key)
    {
        ICache cache = GlobalSetup.GetTested(type);

        cache.Put(key, StubValues.NotExists);

        Assert.That(cache?.Get<StubCached>(key)?.Value, Is.EqualTo(StubValues.NotExists.Value));
    }

    [TestCase(StubKeys.NOT_EXISTS)]
    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    [TestCase(StubKeys.EXISTS_EXPIRED)]
    public void Should_SetValue_When_WithTtl(string key)
    {
        ICache cache = GlobalSetup.GetTested(type);

        cache.Put(key, StubValues.NotExists, new TimeSpan(0, 0, 1));

        Assert.That(cache?.Get<StubCached>(key)?.Value, Is.EqualTo(StubValues.NotExists.Value));
    }
}
