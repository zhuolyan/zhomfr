using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Unit.CacheTests;

[TestFixtureSource(typeof(StubFixturesSource), nameof(StubFixturesSource.Tested))]
public class ForgetAsyncTests<T> where T : AbstractInitializer, new()
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

    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    [CancelAfter(1_000)]
    public async Task Should_ReturnTrue_When_Exists(string key, CancellationToken ct)
    {
        using (Assert.EnterMultipleScope()) {
            Assert.That(this._initializer, Is.Not.Null);
            Assert.That(await this._initializer?.Tested.ForgetAsync(key, ct)!, Is.True);
            Assert.That(await this._initializer?.Tested.GetAsync<StubCached>(key, ct)!, Is.Null);
        }
    }

    [TestCase(StubKeys.NOT_EXISTS)]
    [TestCase(StubKeys.EXISTS_EXPIRED)]
    [CancelAfter(1_000)]
    public async Task Should_ReturnFalse_When_NotExist(string key, CancellationToken ct)
    {
        using (Assert.EnterMultipleScope()) {
            Assert.That(this._initializer, Is.Not.Null);
            Assert.That(await this._initializer?.Tested.ForgetAsync(key, ct)!, Is.False);
            Assert.That(await this._initializer?.Tested.GetAsync<StubCached>(key, ct)!, Is.Null);
        }
    }
}
