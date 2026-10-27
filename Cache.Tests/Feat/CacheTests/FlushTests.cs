using Zhomfr.Cache.Abstractions;
using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Feat.CacheTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class FlushTests(TestTypes type)
{
    [Test]
    public void Should_FlushCache_When_FlushIsCalled()
    {
        ICache cache = GlobalSetup.GetTested(type);

        cache.Put(StubKeys.EXISTS, StubValues.Exists);
        cache.Put(StubKeys.EXISTS_ANOTHER, StubValues.ExistsAnother);

        using (Assert.EnterMultipleScope()) {
            Assert.That(cache.Get<StubCached>(StubKeys.EXISTS_ANOTHER), Is.Not.Null);
            Assert.That(cache.Get<StubCached>(StubKeys.EXISTS), Is.Not.Null);
        }

        cache.Flush();

        using (Assert.EnterMultipleScope()) {
            Assert.That(cache.Get<StubCached>(StubKeys.EXISTS), Is.Null);
            Assert.That(cache.Get<StubCached>(StubKeys.EXISTS_ANOTHER), Is.Null);
        }
    }
}
