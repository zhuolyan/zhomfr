using Zhomfr.Cache.Abstractions;
using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Unit.CacheTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class ForeverAsyncTests(TestTypes type)
{
    [Test]
    [CancelAfter(1_000)]
    public async Task Should_PutItem_When_AddingNotExist(CancellationToken ct = default)
    {
        ICache cache = GlobalSetup.GetTested(type);

        await cache.ForeverAsync(StubKeys.NOT_EXISTS, StubValues.NotExists, ct);

        Assert.That((await cache.GetAsync<StubCached>(StubKeys.NOT_EXISTS, ct))?.Value, Is.EqualTo(StubValues.NotExists.Value));
    }

    [Test]
    [CancelAfter(1_000)]
    public async Task Should_PutItem_When_AddingExists(CancellationToken ct = default)
    {
        ICache cache = GlobalSetup.GetTested(type);

        await cache.ForeverAsync(StubKeys.EXISTS, StubValues.NotExists, ct);

        Assert.That((await cache.GetAsync<StubCached>(StubKeys.EXISTS, ct))?.Value, Is.EqualTo(StubValues.NotExists.Value));
    }
}
