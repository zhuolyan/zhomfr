using Zhomfr.Cache.Abstractions;
using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Unit.CacheTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class PutAsyncTest(TestTypes type)
{
    [TestCase(StubKeys.NOT_EXISTS)]
    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    [TestCase(StubKeys.EXISTS_EXPIRED)]
    [CancelAfter(1_000)]
    public async Task Should_SetValue_When_WithoutTtl(string key, CancellationToken ct)
    {
        ICache cache = GlobalSetup.GetTested(type);

        await cache.PutAsync(key, StubValues.NotExists, ct: ct);

        Assert.That((await cache.GetAsync<StubCached>(key, ct))?.Value, Is.EqualTo(StubValues.NotExists.Value));
    }

    [TestCase(StubKeys.NOT_EXISTS)]
    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    [TestCase(StubKeys.EXISTS_EXPIRED)]
    [CancelAfter(1_000)]
    public async Task Should_SetValue_When_WithTtl(string key, CancellationToken ct)
    {
        ICache cache = GlobalSetup.GetTested(type);

        await cache.PutAsync(key, StubValues.NotExists, new TimeSpan(0, 0, 1), ct);

        Assert.That((await cache.GetAsync<StubCached>(key, ct))?.Value, Is.EqualTo(StubValues.NotExists.Value));
    }
}
