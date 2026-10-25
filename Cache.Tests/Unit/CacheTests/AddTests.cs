using Zhomfr.Cache.Abstractions;
using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Unit.CacheTests;

[TestFixtureSource(typeof(StubFixturesSource), nameof(StubFixturesSource.Tested))]
public class AddTests<T> where T : AbstractInitializer, new()
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
    public void Should_ReturnTrue_When_AddingNotExistsOrExpiredWithoutTtl(string key)
    {
        Assert.That(this._initializer, Is.Not.Null);

        ICache cache = this._initializer.Tested;

        using (Assert.EnterMultipleScope()) {
            Assert.That(cache.Add(key, StubValues.NotExists), Is.True);
            Assert.That(cache.Get<StubCached>(key)?.Value, Is.EqualTo(StubValues.NotExists.Value));
        }
    }

    [TestCase(StubKeys.NOT_EXISTS)]
    [TestCase(StubKeys.EXISTS_EXPIRED)]
    public void Should_ReturnTrue_When_AddingNotExistsOrExpiredWithTtl(string key)
    {
        Assert.That(this._initializer, Is.Not.Null);

        ICache cache = this._initializer.Tested;

        using (Assert.EnterMultipleScope()) {
            Assert.That(cache.Add(key, StubValues.NotExists, new TimeSpan(0, 0, 1)), Is.True);
            Assert.That(cache.Get<StubCached>(key)?.Value, Is.EqualTo(StubValues.NotExists.Value));
        }
    }

    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    public void Should_ReturnFalse_When_AddingExistsWithoutTtl(string key)
    {
        Assert.That(this._initializer, Is.Not.Null);

        ICache cache = this._initializer.Tested;

        using (Assert.EnterMultipleScope()) {
            Assert.That(cache.Add(key, StubValues.NotExists), Is.False);
            Assert.That(cache.Get<StubCached>(key)?.Value, Is.Not.EqualTo(StubValues.NotExists.Value));
        }
    }

    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    public void Should_ReturnFalse_When_AddingExistsWithTtl(string key)
    {
        Assert.That(this._initializer, Is.Not.Null);

        ICache cache = this._initializer.Tested;

        using (Assert.EnterMultipleScope()) {
            Assert.That(cache.Add(key, StubValues.NotExists, new TimeSpan(0, 0, 1)), Is.False);
            Assert.That(cache.Get<StubCached>(key)?.Value, Is.Not.EqualTo(StubValues.NotExists.Value));
        }
    }
}
