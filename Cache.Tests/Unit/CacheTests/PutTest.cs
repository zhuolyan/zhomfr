using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Unit.CacheTests;

[TestFixtureSource(typeof(StubFixturesSource), nameof(StubFixturesSource.Tested))]
public class PutTest<T> where T : AbstractInitializer, new()
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
    public void Should_SetValue_When_WithoutTtl(string key)
    {
        Assert.That(this._initializer, Is.Not.Null);

        this._initializer.Tested.Put(key, StubValues.NotExists);

        Assert.That(this._initializer.Tested?.Get<StubCached>(key)?.Value, Is.EqualTo(StubValues.NotExists.Value));
    }

    [TestCase(StubKeys.NOT_EXISTS)]
    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    [TestCase(StubKeys.EXISTS_EXPIRED)]
    public void Should_SetValue_When_WithTtl(string key)
    {
        Assert.That(this._initializer, Is.Not.Null);

        this._initializer.Tested.Put(key, StubValues.NotExists, new TimeSpan(0, 0, 1));

        Assert.That(this._initializer.Tested?.Get<StubCached>(key)?.Value, Is.EqualTo(StubValues.NotExists.Value));
    }
}
