using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Unit.CacheTests;

[TestFixtureSource(typeof(StubFixturesSource), nameof(StubFixturesSource.Tested))]
public class ForeverTests<T> where T : AbstractInitializer, new()
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
    public void Should_PutItem_When_AddingNotExist()
    {
        Assert.That(this._initializer, Is.Not.Null);

        this._initializer.Tested.Forever(StubKeys.NOT_EXISTS, StubValues.NotExists);

        Assert.That(this._initializer.Tested.Get<StubCached>(StubKeys.NOT_EXISTS)?.Value, Is.EqualTo(StubValues.NotExists.Value));
    }

    [Test]
    public void Should_PutItem_When_AddingExists()
    {
        Assert.That(this._initializer, Is.Not.Null);

        this._initializer.Tested.ForeverAsync(StubKeys.EXISTS, StubValues.NotExists);

        Assert.That(this._initializer.Tested.Get<StubCached>(StubKeys.EXISTS)?.Value, Is.EqualTo(StubValues.NotExists.Value));
    }
}
