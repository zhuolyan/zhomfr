using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Unit.CacheTests;

[TestFixtureSource(typeof(StubFixturesSource), nameof(StubFixturesSource.Tested))]
public class RememberTest<T> where T : AbstractInitializer, new()
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
    public void Should_SetAndReturnValueWithRunFunction_When_NotExistWithoutTtl(string key)
    {
        using (Assert.EnterMultipleScope()) {
            Assert.That(this._initializer, Is.Not.Null);

            Assert.That(this._initializer?.Tested.Remember<StubCached>(key, null, _ => StubValues.NotExists).Value, Is.EqualTo(StubValues.NotExists.Value));

            Assert.That(this._initializer?.Tested.Get<StubCached>(key)!?.Value, Is.EqualTo(StubValues.NotExists.Value));
        }
    }

    [TestCase(StubKeys.NOT_EXISTS)]
    [TestCase(StubKeys.EXISTS_EXPIRED)]
    public void Should_SetAndReturnValueWithRunFunction_When_NotExistWithoTtl(string key)
    {
        using (Assert.EnterMultipleScope()) {
            Assert.That(this._initializer, Is.Not.Null);

            Assert.That(this._initializer?.Tested.Remember<StubCached>(key, new TimeSpan(0, 0, 1), _ => StubValues.NotExists).Value,
                        Is.EqualTo(StubValues.NotExists.Value));

            Assert.That(this._initializer?.Tested.Get<StubCached>(key)!?.Value, Is.EqualTo(StubValues.NotExists.Value));
        }
    }

    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    public void Should_ReturnValueWithoutRunClosure_When_ExistWithoutTtl(string key)
    {
        using (Assert.EnterMultipleScope()) {
            Assert.That(this._initializer, Is.Not.Null);

            Assert.That(this._initializer?.Tested.Remember<StubCached>(key, null, _ => StubValues.NotExists).Value, Is.Not.EqualTo(StubValues.NotExists.Value));

            Assert.That(this._initializer?.Tested.Get<StubCached>(key)!?.Value, Is.Not.EqualTo(StubValues.NotExists.Value));
        }
    }

    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    public void Should_ReturnValueWithoutRunClosure_When_ExistWithTtl(string key)
    {
        using (Assert.EnterMultipleScope()) {
            Assert.That(this._initializer, Is.Not.Null);

            Assert.That(this._initializer?.Tested.Remember<StubCached>(key, new TimeSpan(0, 0, 1), _ => StubValues.NotExists).Value,
                        Is.Not.EqualTo(StubValues.NotExists.Value));

            Assert.That(this._initializer?.Tested.Get<StubCached>(key)!?.Value, Is.Not.EqualTo(StubValues.NotExists.Value));
        }
    }
}
