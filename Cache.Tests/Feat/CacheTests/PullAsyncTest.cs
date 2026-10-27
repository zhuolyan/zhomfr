using Zhomfr.Cache.Abstractions;
using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Feat.CacheTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class PullAsyncTest(TestTypes type)
{
    [TestCase(StubKeys.NOT_EXISTS)]
    [TestCase(StubKeys.EXISTS_EXPIRED)]
    [CancelAfter(1_000)]
    public async Task Should_ReturnNull_When_NotExist(string key, CancellationToken ct)
    {
        ICache cache = GlobalSetup.GetTested(type);
        Assert.That(await cache.PullAsync<StubCached>(key, ct), Is.Null);
    }

    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    [CancelAfter(1_000)]
    public async Task Should_ReturnValueAndRemoveItem_When_NotExist(string key, CancellationToken ct)
    {
        ICache cache = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await cache.PullAsync<StubCached>(key, ct), Is.Not.Null);
            Assert.That(await cache.GetAsync<StubCached>(key, ct), Is.Null);
        }
    }
}
