using Zhomfr.Cache.Tests.Stubs;

namespace Zhomfr.Cache.Tests.Unit.CacheTests;

[TestFixtureSource(typeof(StubFixturesSource), nameof(StubFixturesSource.Tested))]
public class RememberAsyncTest<T> where T : AbstractInitializer, new()
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
    public async Task Should_SetAndReturnValueWithRunFunction_When_NotExistWithoutTtl(string key, CancellationToken ct)
    {
        using (Assert.EnterMultipleScope()) {
            Assert.That(this._initializer, Is.Not.Null);

            Assert.That((await this._initializer?.Tested.RememberAsync<StubCached>(key, null, (_, _) => Task.FromResult(StubValues.NotExists), ct)!).Value,
                        Is.EqualTo(StubValues.NotExists.Value));

            Assert.That((await this._initializer?.Tested.GetAsync<StubCached>(key, ct)!)?.Value, Is.EqualTo(StubValues.NotExists.Value));
        }
    }

    [TestCase(StubKeys.NOT_EXISTS)]
    [TestCase(StubKeys.EXISTS_EXPIRED)]
    [CancelAfter(1_000)]
    public async Task Should_SetAndReturnValueWithRunFunction_When_NotExistWithTtl(string key, CancellationToken ct)
    {
        using (Assert.EnterMultipleScope()) {
            Assert.That(this._initializer, Is.Not.Null);

            Assert.That(
                (await this._initializer?.Tested.RememberAsync<StubCached>(key, new TimeSpan(0, 0, 1), (_, _) => Task.FromResult(StubValues.NotExists), ct)!)
               .Value,
                Is.EqualTo(StubValues.NotExists.Value));

            Assert.That((await this._initializer?.Tested.GetAsync<StubCached>(key, ct)!)?.Value, Is.EqualTo(StubValues.NotExists.Value));
        }
    }

    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    [CancelAfter(1_000)]
    public async Task Should_ReturnValueWithoutRunClosure_When_ExistWithoutTtl(string key, CancellationToken ct)
    {
        using (Assert.EnterMultipleScope()) {
            Assert.That(this._initializer, Is.Not.Null);

            Assert.That((await this._initializer?.Tested.RememberAsync<StubCached>(key, null, (_, _) => Task.FromResult(StubValues.NotExists), ct)!).Value,
                        Is.Not.EqualTo(StubValues.NotExists.Value));

            Assert.That((await this._initializer?.Tested.GetAsync<StubCached>(key, ct)!)?.Value, Is.Not.EqualTo(StubValues.NotExists.Value));
        }
    }

    [TestCase(StubKeys.EXISTS)]
    [TestCase(StubKeys.EXISTS_ANOTHER)]
    [CancelAfter(1_000)]
    public async Task Should_ReturnValueWithoutRunClosure_When_ExistWithTtl(string key, CancellationToken ct)
    {
        using (Assert.EnterMultipleScope()) {
            Assert.That(this._initializer, Is.Not.Null);

            Assert.That(
                (await this._initializer?.Tested.RememberAsync<StubCached>(key, new TimeSpan(0, 0, 1), (_, _) => Task.FromResult(StubValues.NotExists), ct)!)
               .Value,
                Is.Not.EqualTo(StubValues.NotExists.Value));

            Assert.That((await this._initializer?.Tested.GetAsync<StubCached>(key, ct)!)?.Value, Is.Not.EqualTo(StubValues.NotExists.Value));
        }
    }
}
