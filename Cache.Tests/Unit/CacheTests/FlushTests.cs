using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Unit.CacheTests;

[TestFixtureSource(typeof(StubFixturesSource), nameof(StubFixturesSource.Tested))]
public class FlushTests<T> where T : AbstractInitializer, new()
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
    public void Should_FlushCache_When_FlushIsCalled()
    {
        Assert.That(this._initializer, Is.Not.Null);

        this._initializer.Tested.Put(StubKeys.EXISTS, StubValues.Exists);
        this._initializer.Tested.Put(StubKeys.EXISTS_ANOTHER, StubValues.ExistsAnother);

        using (Assert.EnterMultipleScope()) {
            Assert.That(this._initializer.Tested.Get<StubCached>(StubKeys.EXISTS_ANOTHER), Is.Not.Null);
            Assert.That(this._initializer.Tested.Get<StubCached>(StubKeys.EXISTS), Is.Not.Null);
        }

        this._initializer.Tested.Flush();

        using (Assert.EnterMultipleScope()) {
            Assert.That(this._initializer.Tested.Get<StubCached>(StubKeys.EXISTS), Is.Null);
            Assert.That(this._initializer.Tested.Get<StubCached>(StubKeys.EXISTS_ANOTHER), Is.Null);
        }
    }
}
