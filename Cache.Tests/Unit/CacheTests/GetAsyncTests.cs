using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Unit.CacheTests;

[TestFixtureSource(typeof(StubFixturesSource), nameof(StubFixturesSource.Tested))]
public class GetAsyncTests<T> where T : AbstractInitializer, new()
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
    public async Task Should_ReturnNull_When_NotExist(string key, CancellationToken ct)
    {
        using (Assert.EnterMultipleScope()) {
            Assert.That(this._initializer, Is.Not.Null);
            Assert.That(await this._initializer?.Tested.GetAsync<StubCached>(key, ct)!, Is.Null);
        }
    }

    [TestCase(StubKeys.NOT_EXISTS)]
    [TestCase(StubKeys.EXISTS_EXPIRED)]
    [CancelAfter(1_000)]
    public async Task Should_ReturnDefaultValue_When_NotExistWithDefaultValue(string key, CancellationToken ct = default)
    {
        using (Assert.EnterMultipleScope()) {
            Assert.That(this._initializer, Is.Not.Null);
            Assert.That((await this._initializer?.Tested.GetAsync(key, StubValues.NotExists, ct)!)?.Value, Is.EqualTo(StubValues.NotExists.Value));
        }
    }

    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    [CancelAfter(1_000)]
    public async Task Should_ReturnValue_When_AddingExists(string key, CancellationToken ct)
    {
        using (Assert.EnterMultipleScope()) {
            Assert.That(this._initializer, Is.Not.Null);
            StubCached? test = await this._initializer?.Tested.GetAsync<StubCached>(key, ct)!;
            Assert.That(test, Is.Not.Null);
        }
    }

    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    [CancelAfter(1_000)]
    public async Task Should_ReturnValue_When_ExistWithDefaultValue(string key, CancellationToken ct = default)
    {
        using (Assert.EnterMultipleScope()) {
            Assert.That(this._initializer, Is.Not.Null);
            Assert.That((await this._initializer?.Tested.GetAsync(key, StubValues.NotExists, ct)!)?.Value, Is.Not.EqualTo(StubValues.NotExists.Value));
        }
    }
}
