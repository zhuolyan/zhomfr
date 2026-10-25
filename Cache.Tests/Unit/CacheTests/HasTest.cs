using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Unit.CacheTests;

[TestFixtureSource(typeof(StubFixturesSource), nameof(StubFixturesSource.Tested))]
public class HasTest<T> where T : AbstractInitializer, new()
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
    public void Should_ReturnFalse_When_NotExist(string key)
    {
        Assert.That(this._initializer, Is.Not.Null);

        Assert.That(this._initializer?.Tested.Has(key), Is.False);
    }

    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    [CancelAfter(1_000)]
    public void Should_ReturnTrue_When_Exists(string key)
    {
        Assert.That(this._initializer, Is.Not.Null);

        Assert.That(this._initializer?.Tested.Has(key), Is.True);
    }
}
