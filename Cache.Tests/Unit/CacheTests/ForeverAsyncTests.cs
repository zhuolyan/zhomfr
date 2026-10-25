using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Unit.CacheTests;

[TestFixtureSource(typeof(StubFixturesSource), nameof(StubFixturesSource.Tested))]
public class ForeverAsyncTests<T> where T : AbstractInitializer, new()
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

    [Test]
    [CancelAfter(1_000)]
    public async Task Should_PutItem_When_AddingNotExist(CancellationToken ct = default)
    {
        Assert.That(this._initializer, Is.Not.Null);

        await this._initializer.Tested.ForeverAsync(StubKeys.NOT_EXISTS, StubValues.NotExists, ct);

        Assert.That((await this._initializer.Tested.GetAsync<StubCached>(StubKeys.NOT_EXISTS, ct))?.Value, Is.EqualTo(StubValues.NotExists.Value));
    }

    [Test]
    [CancelAfter(1_000)]
    public async Task Should_PutItem_When_AddingExists(CancellationToken ct = default)
    {
        Assert.That(this._initializer, Is.Not.Null);

        await this._initializer.Tested.ForeverAsync(StubKeys.EXISTS, StubValues.NotExists, ct);

        Assert.That((await this._initializer.Tested.GetAsync<StubCached>(StubKeys.EXISTS, ct))?.Value, Is.EqualTo(StubValues.NotExists.Value));
    }
}
