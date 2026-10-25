using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Unit.CacheTests;

[TestFixtureSource(typeof(StubFixturesSource), nameof(StubFixturesSource.Tested))]
public class HasAsyncTest<T> where T : AbstractInitializer, new()
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
    public async Task Should_ReturnFalse_When_NotExist(string key, CancellationToken ct)
    {
        Assert.That(this._initializer, Is.Not.Null);

        Assert.That(await this._initializer?.Tested.HasAsync(key, ct)!, Is.False);
    }

    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    [CancelAfter(1_000)]
    public async Task Should_ReturnTrue_When_Exists(string key, CancellationToken ct)
    {
        Assert.That(this._initializer, Is.Not.Null);

        Assert.That(await this._initializer?.Tested.HasAsync(key, ct)!, Is.True);
    }
}
