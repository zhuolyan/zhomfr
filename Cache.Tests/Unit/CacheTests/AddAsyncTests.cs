using Zhomfr.Cache.Abstractions;
using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Unit.CacheTests;

[TestFixtureSource(typeof(GlobalSetup), nameof(GlobalSetup.Tested))]
public class AddAsyncTests(TestTypes type)
{
    [TestCase(StubKeys.NOT_EXISTS)]
    [TestCase(StubKeys.EXISTS_EXPIRED)]
    [CancelAfter(1_000)]
    public async Task Should_ReturnTrue_When_AddingNotExistsOrExpiredWithoutTtl(string key, CancellationToken ct)
    {
        ICache cache = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await cache.AddAsync(key, StubValues.NotExists, ct: ct), Is.True);
            Assert.That((await cache.GetAsync<StubCached>(key, ct))?.Value, Is.EqualTo(StubValues.NotExists.Value));
        }
    }

    [TestCase(StubKeys.NOT_EXISTS)]
    [TestCase(StubKeys.EXISTS_EXPIRED)]
    [CancelAfter(1_000)]
    public async Task Should_ReturnTrue_When_AddingNotExistsOrExpiredWithTtl(string key, CancellationToken ct)
    {
        ICache cache = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await cache.AddAsync(key, StubValues.NotExists, new TimeSpan(0, 0, 1), ct), Is.True);
            Assert.That((await cache.GetAsync<StubCached>(key, ct))?.Value, Is.EqualTo(StubValues.NotExists.Value));
        }
    }

    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    [CancelAfter(1_000)]
    public async Task Should_ReturnFalse_When_AddingExistsWithoutTtl(string key, CancellationToken ct)
    {
        ICache cache = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await cache.AddAsync(key, StubValues.NotExists, ct: ct), Is.False);
            Assert.That((await cache.GetAsync<StubCached>(key, ct))?.Value, Is.Not.EqualTo(StubValues.NotExists.Value));
        }
    }

    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    [CancelAfter(1_000)]
    public async Task Should_ReturnFalse_When_AddingExistsWithTtl(string key, CancellationToken ct)
    {
        ICache cache = GlobalSetup.GetTested(type);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await cache.AddAsync(key, StubValues.NotExists, new TimeSpan(0, 0, 1), ct), Is.False);
            Assert.That((await cache.GetAsync<StubCached>(key, ct))?.Value, Is.Not.EqualTo(StubValues.NotExists.Value));
        }
    }
}
