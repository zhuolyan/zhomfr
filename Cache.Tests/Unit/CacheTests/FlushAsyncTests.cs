using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Unit.CacheTests;

[TestFixtureSource(typeof(StubFixturesSource), nameof(StubFixturesSource.Tested))]
public class FlushAsyncTests<T> where T : AbstractInitializer, new()
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
    public async Task Should_FlushCache_When_FlushAsyncIsCalled(CancellationToken ct = default)
    {
        Assert.That(this._initializer, Is.Not.Null);

        await this._initializer.Tested.PutAsync(StubKeys.EXISTS, StubValues.Exists, ct: ct);
        await this._initializer.Tested.PutAsync(StubKeys.EXISTS_ANOTHER, StubValues.ExistsAnother, ct: ct);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await this._initializer.Tested.GetAsync<StubCached>(StubKeys.EXISTS_ANOTHER, ct), Is.Not.Null);
            Assert.That(await this._initializer.Tested.GetAsync<StubCached>(StubKeys.EXISTS, ct), Is.Not.Null);
        }

        await this._initializer.Tested.FlushAsync(ct);

        using (Assert.EnterMultipleScope()) {
            Assert.That(await this._initializer.Tested.GetAsync<StubCached>(StubKeys.EXISTS_ANOTHER, ct), Is.Null);
            Assert.That(await this._initializer.Tested.GetAsync<StubCached>(StubKeys.EXISTS, ct), Is.Null);
        }
    }
}
