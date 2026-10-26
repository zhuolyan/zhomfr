using Zhomfr.Cache.Abstractions;
using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Unit.CacheTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class ForeverTests(TestTypes type)
{
    [Test]
    public void Should_PutItem_When_AddingNotExist()
    {
        ICache cache = GlobalSetup.GetTested(type);

        cache.Forever(StubKeys.NOT_EXISTS, StubValues.NotExists);

        Assert.That(cache.Get<StubCached>(StubKeys.NOT_EXISTS)?.Value, Is.EqualTo(StubValues.NotExists.Value));
    }

    [Test]
    public void Should_PutItem_When_AddingExists()
    {
        ICache cache = GlobalSetup.GetTested(type);

        cache.Forever(StubKeys.EXISTS, StubValues.NotExists);

        Assert.That(cache.Get<StubCached>(StubKeys.EXISTS)?.Value, Is.EqualTo(StubValues.NotExists.Value));
    }
}
