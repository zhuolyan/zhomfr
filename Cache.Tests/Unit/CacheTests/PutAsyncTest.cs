using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Unit.CacheTests;

[TestFixtureSource(typeof(StubFixturesSource), nameof(StubFixturesSource.Tested))]
public class PutAsyncTest<T> where T : AbstractInitializer, new()
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
    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    [TestCase(StubKeys.EXISTS_EXPIRED)]
    [CancelAfter(1_000)]
    public async Task Should_SetValue_When_WithoutTtl(string key, CancellationToken ct)
    {
        Assert.That(this._initializer, Is.Not.Null);

        await this._initializer.Tested.PutAsync(key, StubValues.NotExists, ct: ct);

        Assert.That((await this._initializer.Tested?.GetAsync<StubCached>(key, ct)!)?.Value, Is.EqualTo(StubValues.NotExists.Value));
    }

    [TestCase(StubKeys.NOT_EXISTS)]
    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    [TestCase(StubKeys.EXISTS_EXPIRED)]
    [CancelAfter(1_000)]
    public async Task Should_SetValue_When_WithTtl(string key, CancellationToken ct)
    {
        Assert.That(this._initializer, Is.Not.Null);

        await this._initializer.Tested.PutAsync(key, StubValues.NotExists, new TimeSpan(0, 0, 1), ct);

        Assert.That((await this._initializer.Tested?.GetAsync<StubCached>(key, ct)!)?.Value, Is.EqualTo(StubValues.NotExists.Value));
    }
}
