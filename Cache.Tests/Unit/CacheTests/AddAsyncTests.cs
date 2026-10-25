using Zhomfr.Cache.Abstractions;
using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Unit.CacheTests;

[TestFixtureSource(typeof(StubFixturesSource), nameof(StubFixturesSource.Tested))]
public class AddAsyncTests<T> where T : AbstractInitializer, new()
{
    [SetUp]
    public void SetUp()
    {
        this._initializer = new T();
    }

    [TearDown]
    public void TearDown()
    {
        this._initializer?.Dispose();
        this._initializer = null;
    }

    private AbstractInitializer? _initializer;

    [TestCase(StubKeys.NOT_EXISTS)]
    [TestCase(StubKeys.EXISTS_EXPIRED)]
    [CancelAfter(1_000)]
    public async Task Should_ReturnTrue_When_AddingNotExistsOrExpiredWithoutTtl(string key, CancellationToken ct)
    {
        Assert.That(this._initializer, Is.Not.Null);

        ICache cache = this._initializer.Tested;

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
        Assert.That(this._initializer, Is.Not.Null);

        ICache cache = this._initializer.Tested;

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
        Assert.That(this._initializer, Is.Not.Null);

        ICache cache = this._initializer.Tested;

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
        Assert.That(this._initializer, Is.Not.Null);

        ICache cache = this._initializer.Tested;

        using (Assert.EnterMultipleScope()) {
            Assert.That(await cache.AddAsync(key, StubValues.NotExists, new TimeSpan(0, 0, 1), ct), Is.False);
            Assert.That((await cache.GetAsync<StubCached>(key, ct))?.Value, Is.Not.EqualTo(StubValues.NotExists.Value));
        }
    }
}
