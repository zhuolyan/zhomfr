using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Unit.CacheTests;

[TestFixtureSource(typeof(StubFixturesSource), nameof(StubFixturesSource.Tested))]
public class GetTests<T> where T : AbstractInitializer, new()
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
    public void Should_ReturnNull_When_NotExist(string key)
    {
        using (Assert.EnterMultipleScope()) {
            Assert.That(this._initializer, Is.Not.Null);
            Assert.That(this._initializer?.Tested.Get<StubCached>(key), Is.Null);
        }
    }

    [TestCase(StubKeys.NOT_EXISTS)]
    [TestCase(StubKeys.EXISTS_EXPIRED)]
    public void Should_ReturnDefaultValue_When_NotExistWithDefaultValue(string key)
    {
        using (Assert.EnterMultipleScope()) {
            Assert.That(this._initializer, Is.Not.Null);
            Assert.That(this._initializer?.Tested.Get(key, StubValues.NotExists)?.Value, Is.EqualTo(StubValues.NotExists.Value));
        }
    }

    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    public void Should_ReturnValue_When_AddingExists(string key)
    {
        using (Assert.EnterMultipleScope()) {
            Assert.That(this._initializer, Is.Not.Null);
            Assert.That(this._initializer?.Tested.Get<StubCached>(key), Is.Not.Null);
        }
    }

    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    public void Should_ReturnValue_When_ExistWithDefaultValue(string key)
    {
        using (Assert.EnterMultipleScope()) {
            Assert.That(this._initializer, Is.Not.Null);
            Assert.That(this._initializer?.Tested.Get(key, StubValues.NotExists)?.Value, Is.Not.EqualTo(StubValues.NotExists.Value));
        }
    }
}
