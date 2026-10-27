using Zhomfr.Cache.Abstractions;
using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Feat.CacheTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class FlushAsyncTests(TestTypes type)
{
    [Test]
    [CancelAfter(1_000)]
    public async Task Should_FlushCache_When_FlushAsyncIsCalled(CancellationToken ct = default)
    {
        ICache cache = GlobalSetup.GetTested(type);

        await cache.PutAsync(StubKeys.EXISTS, StubValues.Exists, ct: ct);
        await cache.PutAsync(StubKeys.EXISTS_ANOTHER, StubValues.ExistsAnother, ct: ct);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await cache.GetAsync<StubCached>(StubKeys.EXISTS_ANOTHER, ct), Is.Not.Null);
            Assert.That(await cache.GetAsync<StubCached>(StubKeys.EXISTS, ct), Is.Not.Null);
        }

        await cache.FlushAsync(ct);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await cache.GetAsync<StubCached>(StubKeys.EXISTS_ANOTHER, ct), Is.Null);
            Assert.That(await cache.GetAsync<StubCached>(StubKeys.EXISTS, ct), Is.Null);
        }
    }
}
